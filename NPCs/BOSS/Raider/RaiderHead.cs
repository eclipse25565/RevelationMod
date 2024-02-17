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
using Revelation.Buff;
using Terraria.Graphics.Effects;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Revelation.NPCs.BOSS.Raider
{
    [AutoloadBossHead]
    internal class RaiderHead : ModNPC
    {
        public static int Life => Main.masterMode ? 6000 : Main.expertMode ? 6000 : 8000;
        private static int Damage => 36;
        private static int Defense => 5;

        public static int BackgroundMusic => MusicLoader.GetMusicSlot("Revelation/Assets/Music/衰竭辐射boss战1");

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 30;
            NPC.scale = 2.0f;
            NPC.lifeMax = Life;
            NPC.defense = Defense;
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

        private float PortalX
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private float PortalY
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        private float PortalDelta
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        private enum AIState
        {
            Spawned,
            Stretching,
            Enclosing,
            Spectating,
            ZMoving,
            PreparingRaiding,
            Raiding,
            ReturningToZMoving,
            Stage4,
            Dying
        }

        private struct AIData
        {
            public AIState state = AIState.Spawned;
            public ulong counter = 0;
            public bool spawnedObject = false;

            public AIData()
            {
            }

            public void Serialize(BinaryWriter writer)
            {
                writer.Write((int)state);
                writer.Write(counter);
                writer.Write(spawnedObject);
            }

            public void Serialize(BinaryReader reader)
            {
                state = (AIState)reader.ReadInt32();
                counter = reader.ReadUInt64();
                spawnedObject = reader.ReadBoolean();
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
            if (!NPC.HasValidTarget || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest();
            }

            ++ai.counter;
            switch (ai.state)
            {
                case AIState.Spawned:
                    AI_Spawned();
                    break;
                case AIState.Stretching:
                    AI_Strecthing();
                    break;
                case AIState.Enclosing:
                    AI_Enclosing();
                    break;
                case AIState.Spectating:
                    AI_Spectating();
                    break;
                case AIState.ZMoving:
                    AI_ZMoving();
                    break;
                case AIState.PreparingRaiding:
                    AI_PrepareRaiding();
                    break;
                case AIState.Raiding:
                    AI_Raiding();
                    break;
                case AIState.ReturningToZMoving:
                    AI_ReturningToZMoving();
                    break;
                case AIState.Stage4:
                    AI_Stage4();
                    break;
                case AIState.Dying:
                    AI_Dying();
                    break;
            }

            if(Stage == 3 && NPC.life <= 1000)
            {
                Stage = 4;
                ai.state = AIState.Stage4;
                NPC.netUpdate = true;
            }

            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
        }

        private Player TargetPlayer => Main.player[NPC.target];

        private void AI_Spawned()
        {
            Stage = 0;
            SpawnTail();
            ai.state = AIState.Stretching;
            TargetPlayer.AddBuff(BuffID.Darkness, 10 * 60);
            NPC.netUpdate = true;
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
            if (dist < 700.0f)
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
                ai.state = AIState.Enclosing;
                NPC.netUpdate = true;
            }
        }

        private void AI_Enclosing()
        {
            AI_SurroundPlayer(800.0f, 20.0f, 0.0785f);

            if (NPC.life < NPC.lifeMax / 2)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                Stage = 2;
                ai.state = AIState.Spectating;
                NPC.Opacity = 0.2f;
                NPC.dontTakeDamage = true;
                AI_SpawnBrother();
                NPC.netUpdate = true;
            }
        }

        private int crusher = 0, devourer = 0;
        private NPC CrusherNPC => Main.npc[crusher];
        private NPC DevourerNPC => Main.npc[devourer];

        private void AI_SpawnBrother()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
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
            if (crusher == 0 && devourer == 0)
            {
                Stage = 3;
                ai.state = AIState.ZMoving;
                NPC.Opacity = 1.0f;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
            }
            else
            {
                if (!CrusherNPC.active || CrusherNPC.life <= 0)
                {
                    crusher = 0;
                }
                if (!DevourerNPC.active || DevourerNPC.life <= 0)
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

        private float Stage3SpeedFactor {
            get {
                float result = 1.0f;
                foreach(var npc in Main.npc)
                {
                    if(npc.active && npc.type == ModContent.NPCType<RaidingDestroyerHead>())
                    {
                        result += 0.03f;
                    }
                }
                return result;
            }
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
            var speed = 20.0f * Stage3SpeedFactor;

            if(ai.counter >= 240)
            {
                ai.state = AIState.PreparingRaiding;
                ai.counter = 0;
                NPC.netUpdate = true;
            }

            NPC.velocity = direction.RotatedBy(omega) * speed;
        }

        private void AI_PrepareRaiding()
        {
            if(PortalDelta != 0.0f)
            {
                ai.state = AIState.ZMoving;
                ai.counter = 0;
                NPC.netUpdate = true;
                return;
            }
            var expectedDiretion = -Vector2.UnitY;
            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);
            if(Vector2.Dot(direction, expectedDiretion) < 0.99f)
            {
                var speed = 20.0f * Stage3SpeedFactor;
                var omega = Math.Clamp(direction.AngleTo(expectedDiretion), -0.05f, 0.05f);
                NPC.velocity = direction.RotatedBy(omega) * speed;
            }
            else
            {
                var center = NPC.Center;
                PortalX = center.X;
                PortalY = center.Y;
                var expectedPos = TargetPlayer.Center;
                expectedPos.Y += 1200.0f;
                var delta = expectedPos - center;
                var dist = delta.Length();

                // pack the delta
                var angle = Math.Atan2(delta.Y, delta.X) + Math.PI;
                PortalDelta = (float)Math.Floor(dist) * 8.0f + (float)angle;
                
                NPC.Center = expectedPos;

                ai.counter = 0;
                ai.state = AIState.Raiding;
                NPC.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
        }

        private void AI_Raiding()
        {
            var delta = TargetPlayer.Center - NPC.Center;
            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);
            var expectedDirection = delta.SafeNormalize(Vector2.UnitX);
            var dist = delta.Length();
            var dot = Vector2.Dot(direction, expectedDirection);
            var speed = 20.0f * Stage3SpeedFactor;
            var omegaMax = 0.07f;
            if (dot > 0.2f)
            {
                if (dist > 150.0f)
                {
                    var omega = Math.Clamp(direction.AngleTo(expectedDirection), -omegaMax, omegaMax);
                    NPC.velocity = direction.RotatedBy(omega) * speed;
                }
                
                if(!ai.spawnedObject && dist < 300.0f)
                {
                    ai.spawnedObject = true;
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        var velocity = (TargetPlayer.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 1.6f;
                        foreach (var i in Enumerable.Range(0, Main.rand.Next(7, 15)))
                        {
                            var diffuse = Main.rand.NextVector2Circular(220.0f, 220.0f);
                            var projectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + diffuse, velocity,
                                ModContent.ProjectileType<RadiationProjectile>(), (int)(Damage * 1.2f), 0.0f, -1, NPC.whoAmI);
                            projectile.netUpdate = true;
                        }
                    }
                    NPC.netUpdate = true;
                }
            }
            else
            {
                ai.counter = 0;
                ai.spawnedObject = false;
                ai.state = AIState.ReturningToZMoving;
                NPC.netUpdate = true;
            }
        }

        private void AI_ReturningToZMoving()
        {
            var delta = TargetPlayer.Center - NPC.Center;
            var dist = delta.Length();
            if(dist > 600.0f)
            {
                ai.counter = 0;
                ai.state = AIState.ZMoving;
                NPC.netUpdate = true;
            }
        }

        private void AI_Stage4()
        {
            var dist = Vector2.Distance(Main.LocalPlayer.Center, NPC.Center);
            if (Main.netMode != NetmodeID.Server && dist <= 2400.0f)
            {
                // outer 0 -> inner 1
                var factor = Math.Clamp((2000.0f - dist) / 400.0f, 0.0f, 1.0f);
                // max 1 -> die 0
                var progress = 1.0f - factor * Math.Clamp((1000.0f - NPC.life) / 700.0f, 0.0f, 1.0f);

                Filters.Scene.Activate("RaiderBlindness").GetShader().UseProgress(progress);
            }

            var expectedDirection = (TargetPlayer.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);
            var omega = Math.Clamp(direction.AngleTo(expectedDirection), -0.03f, 0.03f);
            NPC.velocity = direction.RotatedBy(omega) * 21.0f;
        }

        private void AI_Dying()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                if (ai.counter < 180)
                {
                    var progress = ai.counter / 180.0f;
                    Filters.Scene.Activate("RaiderBlindness").GetShader().UseProgress(progress);
                }
                else
                {
                    Filters.Scene.Deactivate("RaiderBlindness");
                    NPC.life = 0;
                    NPC.checkDead();
                }
            }
            NPC.velocity = NPC.velocity.SafeNormalize(Vector2.UnitX) * 2.0f;
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
            if(Stage == 2 || Stage == 5)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override bool CheckDead()
        {
            var result = base.CheckDead();
            if(result)
            {
                if(ai.state != AIState.Dying)
                {
                    ai.state = AIState.Dying;
                    ai.counter = 0;
                    Stage = 5;
                    NPC.dontTakeDamage = true;
                    NPC.netUpdate = true;
                }

                if(ai.state == AIState.Dying && ai.counter > 180)
                {
                    return true;
                }
                else
                {
                    NPC.life = 1;
                    return false;
                }
            }
            return result;
        }
    }
}
