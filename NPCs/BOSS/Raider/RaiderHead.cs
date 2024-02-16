using Revelation;
using System;
using System.Linq;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.GameContent.ItemDropRules;
using Revelation.Items.生物掉落物.BOSS掉落物.衰竭辐射.袭击者;
using Steamworks;
using Terraria.GameContent;

namespace Revelation.NPCs.BOSS.Raider
{
    [AutoloadBossHead]
    internal class RaiderHead : ModNPC
    {
        public static int Life => Main.masterMode ? 25000 : Main.expertMode ? 22500 : 30000;
        private static int Damage => 80;

        public static int BackgroundMusic => MusicLoader.GetMusicSlot("Revelation/Assets/Music/衰竭辐射boss战1");

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 30;
            NPC.scale = 2.0f;
            NPC.lifeMax = Life;
            NPC.defense = 10;
            NPC.knockBackResist = 0;
            NPC.damage = Damage;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.npcSlots = 6;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.HitSound = new SoundStyle("Revelation/Sound/BOSS音效/袭击者/袭击者受击2");
            NPC.value = Item.buyPrice(0, 4, 50, 0);

            Music = BackgroundMusic;
        }

        private int Stage
        {
            get => (int)NPC.ai[0];
            set { NPC.ai[0] = value; NPC.netUpdate = true; }
        }

        private enum AIStatus
        {
            Spawned,
            Stretching,
            Enclosing,
            Spectating,
            ZMoving
        }

        private struct AIData
        {
            public AIStatus status = AIStatus.Spawned;
            public ulong counter = 0;

            public AIData()
            {
            }

            public void Serialize(BinaryWriter writer)
            {
                writer.Write((int)status);
            }

            public void Serialize(BinaryReader reader)
            {
                status = (AIStatus)reader.ReadInt32();
            }
        }

        private AIData ai;

        public override void SendExtraAI(BinaryWriter writer)
        {
            ai.Serialize(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            ai.Serialize(reader);
        }

        public override void AI()
        {
            if(!NPC.HasValidTarget || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest();
            }

            ++ai.counter;
            switch(ai.status)
            {
                case AIStatus.Spawned:
                    AI_Spawned();
                    break;
                case AIStatus.Stretching:
                    AI_Strecthing();
                    break;
                case AIStatus.Enclosing:
                    AI_Enclosing();
                    break;
                case AIStatus.Spectating:
                    AI_Spectating();
                    break;
                case AIStatus.ZMoving:
                    AI_ZMoving();
                    break;
            }

            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
        }

        private Player TargetPlayer => Main.player[NPC.target];

        private void AI_Spawned()
        {
            Stage = 0;
            SpawnTail();
            ai.status = AIStatus.Stretching;
            TargetPlayer.AddBuff(BuffID.Darkness, 10 * 60);
        }

        private void SpawnTail()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                var last = NPC.whoAmI;
                int segment;
                foreach (int i in Enumerable.Range(0, 39))
                {
                    segment = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<RaiderBody>(),
                            0, NPC.whoAmI, last, 0.0f, 0.0f);
                    Main.npc[segment].realLife = NPC.whoAmI;
                    Main.npc[segment].netUpdate = true;
                    last = segment;
                }
                segment = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<RaiderTail>(),
                            0, NPC.whoAmI, last, 0.0f, 0.0f);
                Main.npc[segment].realLife = NPC.whoAmI;
                Main.npc[segment].netUpdate = true;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, segment);
            }
            Main.npc[NPC.whoAmI].netUpdate = true;
        }

        private void AI_Strecthing()
        {
            var delta = TargetPlayer.Center - NPC.Center;
            var dist = delta.Length();
            if(dist < 700.0f)
            {
                var expectedDirection = -delta.SafeNormalize(Vector2.UnitX);
                var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);
                var maxOmega = 0.0698f;
                var angleTo = direction.AngleTo(expectedDirection);
                var omega = Math.Clamp(angleTo, -maxOmega, maxOmega);
                var speed = 15.0f;
                NPC.velocity = direction.RotatedBy(omega) * speed;
            }
            else
            {
                Stage = 1;
                ai.status = AIStatus.Enclosing;
            }
        }

        private void AI_Enclosing()
        {
            AI_SurroundPlayer(800.0f, 20.0f, 0.0785f);

            if(NPC.life < NPC.lifeMax / 2)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                Stage = 2;
                ai.status = AIStatus.Spectating;
                NPC.Opacity = 0.2f;
                AI_SpawnBrother();
                NPC.netUpdate = true;
            }
        }

        private int crusher = 0, devourer = 0;
        private NPC CrusherNPC => Main.npc[crusher];
        private NPC DevourerNPC => Main.npc[devourer];

        private void AI_SpawnBrother()
        {
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                var pos = NPC.Center;
                crusher = NPC.NewNPC(NPC.GetSource_FromAI(), (int)pos.X, (int)pos.Y, ModContent.NPCType<CrusherHead>());
                Main.npc[crusher].netUpdate = true;
                devourer = NPC.NewNPC(NPC.GetSource_FromAI(), (int)pos.X, (int)pos.Y, ModContent.NPCType<DevourerHead>());
                Main.npc[devourer].netUpdate = true;
            }
        }

        private void AI_Spectating()
        {
            if(crusher == 0 && devourer == 0)
            {
                Stage = 3;
                ai.status = AIStatus.ZMoving;
                NPC.Opacity = 1.0f;
                NPC.netUpdate = true;
            }
            else
            {
                if(!CrusherNPC.active || CrusherNPC.life <= 0)
                {
                    crusher = 0;
                }
                if(!DevourerNPC.active || DevourerNPC.life <= 0)
                {
                    devourer = 0;
                }

                AI_SurroundPlayer(1000.0f, 30.0f, 0.1f);
            }
        }

        private void AI_SurroundPlayer(float r, float speed, float omegaMax)
        {
            var delta = TargetPlayer.Center - NPC.Center;
            var dist = delta.Length();

            var up = -delta.SafeNormalize(Vector2.UnitX);
            var left = new Vector2(-up.Y, up.X);
            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);


            var directionOnCircle = (Vector2.Dot(direction, left) * left).SafeNormalize(Vector2.UnitX);
            var factor = Math.Clamp((r - dist) / r, -1.0f, 1.0f);
            var expectedDirection = factor * up + (1 - Math.Abs(factor)) * directionOnCircle;
            var omega = Math.Clamp(direction.AngleTo(expectedDirection), -omegaMax, omegaMax);

            NPC.velocity = direction.RotatedBy(omega) * speed;
        }

        private void AI_ZMoving()
        {
            var delta = TargetPlayer.Center - NPC.Center;
            var dist = delta.Length();

            var up = -delta.SafeNormalize(Vector2.UnitX);
            var left = new Vector2(-up.Y, up.X);
            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);
            var tangent = (Vector2.Dot(direction, left) * left).SafeNormalize(Vector2.UnitX);

            var orbit = 800.0f;
            var angle = (tangent.Y * up.X - tangent.X * up.Y) * (Math.PI / 2 - 0.02f);
            if(dist < orbit)
            {
                angle = -angle;
            }
            var expectedDirection = tangent.RotatedBy(angle);
            var omegaMax = 0.06f;
            var omega = Math.Clamp(direction.AngleTo(expectedDirection), -omegaMax, omegaMax);
            var speed = 20.0f;

            NPC.velocity = direction.RotatedBy(omega) * speed;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射组织>(), 1, 20, 35));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射心脏>(), 2, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射利齿>(), 1, 1, 3));
            npcLoot.Add(notExpertRule);

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<袭击者令牌>()));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            base.BossLoot(ref name, ref potionType);
            potionType = ItemID.HealingPotion;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if(Stage == 2)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
    }
}
