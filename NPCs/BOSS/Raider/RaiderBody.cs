using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class RaiderBody : ModNPC
    {
        public static readonly SoundStyle SoundShoot = new SoundStyle("Revelation/NPCs/BOSS/Raider/Shoot");

        protected virtual int Damage => 40;
        protected virtual int Defense => 0;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 30;
            NPC.scale = 2.0f;
            NPC.lifeMax = RaiderHead.Life;
            NPC.defense = Defense;
            NPC.knockBackResist = 0.0f;
            NPC.damage = Damage;
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.HitSound = new SoundStyle("Revelation/Sound/BOSS音效/袭击者/袭击者受击2");

            Music = RaiderHead.BackgroundMusic;
        }

        private int Head => (int)NPC.ai[0];
        private NPC HeadNPC => Main.npc[Head];
        private int HeadStage => (int)HeadNPC.ai[0];

        private int Following => (int)NPC.ai[1];
        private NPC FollowingNPC => Main.npc[Following];
        private Player Target => Main.player[NPC.target];

        public override void AI()
        {
            if (!NPC.HasValidTarget)
            {
                NPC.TargetClosest();
            }

            bool shouldDespawn = false;
            if (Head > 0 && Following > 0 && FollowingNPC.active)
            {
                if (HeadNPC.active && HeadNPC.life > 0)
                {
                    NPC.realLife = Head;
                }
                else
                {
                    shouldDespawn = true;
                }
            }
            else
            {
                shouldDespawn = true;
            }

            if (shouldDespawn)
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
                NPC.checkDead();
                return;
            }

            if(HeadStage == 1)
            {
                if(Main.rand.NextBool(7200))
                {
                    AI_ShootDart();
                }
                if(Main.rand.NextBool(5400))
                {
                    AI_SpawnEgg();
                }
            }
            else if(HeadStage == 2)
            {
                NPC.Opacity = 0.2f;
            }
            else if(HeadStage == 3)
            {
                NPC.Opacity = 1.0f;
            }

            var delta = FollowingNPC.Center - NPC.Center;
            var dist = delta.Length();
            float speed;
            if (dist >= (NPC.height + FollowingNPC.height) / 2)
            {
                speed = FollowingNPC.velocity.Length();
            }
            else
            {
                speed = 0.01f;
            }
            NPC.velocity = speed * delta.SafeNormalize(Vector2.UnitX);
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
        }

        private void AI_ShootDart()
        {
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                var velocity = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 12.5f;
                var projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity,
                    ModContent.ProjectileType<PoisonDart>(), (int)(Damage * 1.5f), 1.0f);
                Main.projectile[projectile].netUpdate = true;
            }
            SoundEngine.PlaySound(SoundShoot, NPC.Center);
        }

        private void AI_SpawnEgg()
        {
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                var delta = Target.Center - NPC.Center;
                if(delta.Y > 32.0f)
                {
                    var velocity = (delta.SafeNormalize(Vector2.UnitX) * 7.0f + Main.rand.NextVector2Unit(0, (float)Math.PI))
                        .SafeNormalize(Vector2.UnitX) * (float)(Main.rand.NextDouble() * 6.0 + 6.0f);
                    var pos = NPC.Center;
                    var npc = NPC.NewNPC(NPC.GetSource_FromAI(), (int)pos.X, (int)pos.Y, ModContent.NPCType<BugEgg>());
                    Main.npc[npc].velocity = velocity;
                    Main.npc[npc].netUpdate = true;
                }
                
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if(HeadStage == 2)
            {
                modifiers.SetMaxDamage(0);
            }
            else if (Vector2.Distance(FollowingNPC.Center, NPC.Center) < NPC.height / 6.0f)
            {
                modifiers.SetMaxDamage(1);
            }
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if(HeadStage == 2)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}
