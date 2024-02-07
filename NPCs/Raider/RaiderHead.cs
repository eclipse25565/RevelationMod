using Revelation.NPCs.BOSS.衰竭辐射;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Terraria.GameContent.Animations;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.GameContent.ItemDropRules;
using Revelation.Items.生物掉落物.BOSS掉落物.衰竭辐射.袭击者;

namespace Revelation.NPCs.Raider
{
    [AutoloadBossHead]
    internal class RaiderHead : ModNPC
    {

        public override string Texture => "Revelation/NPCs/BOSS/衰竭辐射/袭击者";

        public override string BossHeadTexture => "Revelation/NPCs/BOSS/衰竭辐射/袭击者_Head_Boss";

        public static int Life => Main.masterMode ? 25000 : Main.expertMode ? 22500 : 30000;

        public static int WhenToStage2 => Main.masterMode ? 45000 : Main.expertMode ? 22500 : 10000;

        public static int BackgroundMusic => MusicLoader.GetMusicSlot("Revelation/Assets/Music/衰竭辐射boss战1");

        private static int Damage => 80;

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

        private enum AIStatus
        {
            PreparingNextAttack,
            Targeting,
            Marching,

        }

        private struct AIData
        {
            public int counter = 0;
            public AIStatus status = AIStatus.PreparingNextAttack;
            public bool triedSpawningDestroyer = false;
            public bool spawnedTail = false;

            public AIData()
            {
            }

            public void Serialize(BinaryWriter writer)
            {
                writer.Write(counter);
                writer.Write((int)status);
            }

            public void Serialize(BinaryReader reader)
            {
                counter = reader.ReadInt32();
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
            if (!ai.spawnedTail)
            {
                SpawnTail();
            }

            if(!NPC.HasValidTarget || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest();
            }

            ++ai.counter;
            switch (ai.status)
            {
                case AIStatus.PreparingNextAttack:
                    AI_PreparingNextAttack();
                    break;
                case AIStatus.Targeting:
                    AI_Targeting();
                    break;
                case AIStatus.Marching:
                    AI_Marching();
                    break;
            }
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
            if(NPC.life < WhenToStage2 && (int)this.NPC.ai[3] == 0 && ai.counter == 0)
            {
                this.NPC.damage += 20;
                this.NPC.defense += 5;
                this.NPC.ai[3] = 1;
                SoundEngine.PlaySound(SoundID.Roar, this.NPC.Center);
                this.NPC.netUpdate = true;
            }
        }

        private void SpawnTail()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                var last = NPC.whoAmI;
                var segment = NPC.whoAmI;
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
            ai.spawnedTail = true;
        }

        private void AI_PreparingNextAttack()
        {
            var target = Main.player[NPC.target];
            var delta = target.Center - NPC.Center;

            var dist = delta.Length();

            if (ai.counter < 300 && dist < 800.0f)
            {
                var speed = 17.0f;

                var oldDirection = NPC.velocity.SafeNormalize(Vector2.UnitX);
                var normal = -delta.SafeNormalize(Vector2.UnitX);
                var tangent = new Vector2(-normal.Y, normal.X);

                var dot = normal.X * oldDirection.X + normal.Y * oldDirection.Y;
                if (dot > -0.1f)
                {
                    var orbitDirection = (oldDirection.X * tangent.X + oldDirection.Y * tangent.Y) * tangent;
                    orbitDirection = orbitDirection.SafeNormalize(Vector2.UnitX);
                    var sign = Math.Sign(orbitDirection.Y * oldDirection.X - orbitDirection.X * oldDirection.Y);

                    var omega = sign * Math.Min(0.01f + speed / dist, 0.0523f);
                    omega *= 0.1f + Math.Min(dist / 500.0f * 0.9f, 0.9f);

                    NPC.velocity = oldDirection.RotatedBy(omega) * speed;
                }
                else if (NPC.velocity.Length() < 0.01f)
                {
                    NPC.velocity = normal * speed;
                }

            }
            else
            {
                ai.counter = 0;
                ai.status = AIStatus.Targeting;
            }
        }

        private void AI_Targeting()
        {
            var target = Main.player[NPC.target];
            var delta = target.Center - NPC.Center;

            if (ai.counter < 120)
            {
                float speed;
                if (ai.counter < 60)
                {
                    speed = 17.0f - 3.0f * (ai.counter / 60.0f);
                }
                else
                {
                    speed = 14.0f;
                }

                var normal = -delta.SafeNormalize(Vector2.UnitX);
                var tangent = new Vector2(-normal.Y, normal.X);
                var normalizedVelocity = NPC.velocity.SafeNormalize(Vector2.UnitX);
                var direction = (normalizedVelocity.X * tangent.X + normalizedVelocity.Y * tangent.Y) * tangent;
                direction = direction.SafeNormalize(Vector2.UnitX);

                NPC.velocity = direction * speed;
            }
            else
            {
                if(Main.rand.NextBool())
                {
                    ai.counter = 80;
                }
                ai.counter = 0;
                ai.status = AIStatus.Marching;
            }
        }

        private void AI_Marching()
        {
            var target = Main.player[NPC.target];
            var delta = target.Center - NPC.Center;
            var dist = delta.Length();
            var normalizedVelocity = NPC.velocity.SafeNormalize(Vector2.UnitX);
            var normalizedDelta = delta.SafeNormalize(Vector2.UnitY);
            var dot = normalizedVelocity.X * normalizedDelta.X + normalizedVelocity.Y * normalizedDelta.Y;

            var circle = (int)NPC.ai[3] == 0 ? 300.0f : 450.0f;

            // 冲向玩家时如果没生成则尝试生成
            if (dist < circle && !ai.triedSpawningDestroyer && dot > 0.0f)
            {
                var factor = (int)NPC.ai[3] == 0 ? 0.4f : 0.2f;
                if (Main.rand.NextDouble() > factor)
                {
                    var pos = target.Center + delta.SafeNormalize(Vector2.UnitX) * (Main.rand.NextFloat() * 0.5f + 0.5f) * 128.0f;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        var destroyer = NPC.NewNPC(NPC.GetSource_FromAI(), (int)pos.X, (int)pos.Y, ModContent.NPCType<RaidingDestroyerHead>());
                        Main.npc[destroyer].netUpdate = true;

                        if ((int)NPC.ai[3] != 0)
                        {
                            foreach (var i in Enumerable.Range(0, Main.rand.Next(10) + 7))
                            {
                                var velocity = Vector2.Zero;
                                var position = NPC.Center + normalizedVelocity * 128.0f + Main.rand.NextVector2Circular(128.0f, 128.0f);
                                var projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), position,
                                    velocity, ModContent.ProjectileType<RaiderProjectile>(), 40, 0.0f);
                                Main.projectile[projectile].netUpdate = true;
                            }
                        }
                    }

                }
                ai.triedSpawningDestroyer = true;
            }

            // 如果玩家在圈内但是角度过大，或者追击时间太长，则放弃追击
            if (dist < 300.0f && (dot < 0.75f || ai.counter > 90))
            {
                ai.counter = 0;
                ai.triedSpawningDestroyer = false;
                ai.status = AIStatus.PreparingNextAttack;
            }
            // 追击玩家
            else
            {
                var speed = 14.0f;
                var up = delta.SafeNormalize(Vector2.UnitY);

                var sign = Math.Sign(up.Y * normalizedVelocity.X - up.X * normalizedVelocity.Y);

                var omega = sign * (0.04f + speed / dist);

                NPC.velocity = (normalizedVelocity.RotatedBy(omega)) * speed;
            }
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
    }
}
