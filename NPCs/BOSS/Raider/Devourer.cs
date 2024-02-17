using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class DevourerHead : WormLikeHead
    {
        private static int Life => Main.masterMode? 3000 : Main.expertMode? 2500 : 3000;
        private static int Damage => 15;

        protected override int BodyType => ModContent.NPCType<DevourerBody>();
        private class DevourerBody : WormLikeBody
        {
            public override void SetDefaults()
            {
                NPC.aiStyle = -1;
                NPC.width = 32;
                NPC.height = 28;
                NPC.damage = Damage / 2;
                NPC.lifeMax = Life;
                NPC.defense = 0;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath1;
                NPC.HitSound = SoundID.NPCHit1;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.value = 0;
                NPC.boss = true;
                NPC.lavaImmune = true;
            }
        }

        protected override int TailType => ModContent.NPCType<DevourerTail>();
        private class DevourerTail : WormLikeTail
        {
            public override void SetDefaults()
            {
                NPC.aiStyle = -1;
                NPC.width = 32;
                NPC.height = 28;
                NPC.damage = Damage / 3;
                NPC.lifeMax = Life;
                NPC.defense = 0;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath1;
                NPC.HitSound = SoundID.NPCHit1;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.value = 0;
                NPC.boss = true;
                NPC.lavaImmune = true;
                crowdProtectionFactor = 12.0f;
            }
        }
        protected override int SegmentLength => 30;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 28;
            NPC.damage = Damage;
            NPC.lifeMax = Life;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = 0;
            NPC.boss = true;
            NPC.lavaImmune = true;
        }

        private enum AIState
        {
            Shooting,
            Adjusting
        };
        private AIState state
        {
            get => (AIState)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }

        private int counter
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private Player Target => Main.player[NPC.target];
        private const float minSpeed = 2.0f;
        private const float maxSpeed = 28.0f;
        private float speed = minSpeed;
        private const float acceleration = 0.8f;
        public override void AI()
        {
            
            if (!NPC.HasValidTarget || Target.dead)
            {
                NPC.TargetClosest();
            }

            var delta = Target.Center - NPC.Center;
            var targetDirection = delta.SafeNormalize(Vector2.UnitX);
            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);
            var dot = Vector2.Dot(targetDirection, direction);
            var dist = delta.Length();

            if (state == AIState.Shooting)
            {
                if(dot > 0.2f && dist > 300.0f && dist < 900.0f && delta.Y > 0.0f)
                {
                    ++counter;
                    counter %= 40;
                    if(counter == 0)
                    {
                        AI_ShootDart();
                    }
                }
                else
                {
                    state = AIState.Adjusting;
                }
                speed = Math.Clamp(speed - acceleration, minSpeed, maxSpeed);
                NPC.velocity = speed * direction;
            }
            else if (state == AIState.Adjusting)
            {
                if(dist <= 500.0f)
                {
                    var expectedDirection = -targetDirection;
                    speed = Math.Clamp(speed + acceleration, minSpeed, maxSpeed);
                    var maxOmega = 64.0f / speed;
                    var omega = Math.Clamp(direction.AngleTo(expectedDirection), -maxOmega, maxOmega);
                    NPC.velocity = direction.RotatedBy(omega) * speed;
                }
                else if(dist >= 700.0f)
                {
                    speed = Math.Clamp(speed + acceleration, minSpeed, maxSpeed);
                    var maxOmega = 64.0f / speed;
                    var omega = Math.Clamp(direction.AngleTo(targetDirection), -maxOmega, maxOmega);
                    NPC.velocity = direction.RotatedBy(omega) * speed;
                }
                else if(dot <= 0.3f)
                {
                    var maxOmega = 0.1047f;
                    var omega = Math.Clamp(direction.AngleTo(targetDirection), -maxOmega, maxOmega);
                    NPC.velocity = direction.RotatedBy(omega) * speed;
                }
                else if(delta.Y <= 100.0f)
                {
                    var maxOmega = 0.1047f;
                    var omega = Math.Clamp(direction.AngleTo(-Vector2.UnitY), -maxOmega, maxOmega);
                    speed = Math.Clamp(speed + acceleration, minSpeed, maxSpeed);
                    NPC.velocity = direction.RotatedBy(omega) * speed;
                }
                else
                {
                    state = AIState.Shooting;
                }
            }
            else
            {
                state = AIState.Adjusting;
            }

            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.570795f;
        }

        private void AI_ShootDart()
        {
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                var direction = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                var rand = Main.rand.NextVector2Unit();
                var velocity = (direction * 24.0f + rand).SafeNormalize(Vector2.UnitX) * 12.5f;
                var projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity,
                    ModContent.ProjectileType<PoisonDart>(), Damage * 2, 1.0f);
                Main.projectile[projectile].netUpdate = true;
            }
            SoundEngine.PlaySound(RaiderBody.SoundShoot, NPC.Center);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
        }
    }
}
