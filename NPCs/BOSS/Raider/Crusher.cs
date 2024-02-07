using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class CrusherHead : WormLikeHead
    {
        private static int Life => 20000;
        private static int Damage => 170;

        protected override int BodyType => ModContent.NPCType<CrusherBody>();
        private class CrusherBody : WormLikeBody
        {
            public override void SetDefaults()
            {
                NPC.aiStyle = -1;
                NPC.width = 32;
                NPC.height = 28;
                NPC.damage = 10;
                NPC.lifeMax = Life;
                NPC.defense = 50;
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

        protected override int TailType => ModContent.NPCType<CrusherTail>();
        private class CrusherTail : WormLikeTail
        {
            public override void SetDefaults()
            {
                NPC.aiStyle = -1;
                NPC.width = 32;
                NPC.height = 28;
                NPC.damage = Damage;
                NPC.lifeMax = Life;
                NPC.defense = 10;
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
        protected override int SegmentLength => 30;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 28;
            NPC.damage = Damage;
            NPC.lifeMax = Life;
            NPC.defense = 50;
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
            Aiming,
            Marching
        };
        private AIState state
        {
            get => (AIState)NPC.ai[0];
            set
            {
                NPC.ai[0] = (int)value;
            }
        }

        private const float acceleration = 4.0f;
        private const float accelerationBrake = 0.3f;
        private const float maxSpeed = 35.0f;
        private const float minSpeed = 3.0f;
        private float speed = minSpeed;

        public override void AI()
        {
            var target = Main.player[NPC.target];
            if (!NPC.HasValidTarget || target.dead)
            {
                NPC.TargetClosest();
            }

            var delta = target.Center - NPC.Center;
            var targetDirection = delta.SafeNormalize(Vector2.UnitX);
            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);
            var dot = Vector2.Dot(targetDirection, direction);

            if (state == AIState.Aiming)
            {
                if(dot > 0.9998f)
                {
                    state = AIState.Marching;
                }
                else if(speed > minSpeed)
                {
                    speed = Math.Clamp(speed - accelerationBrake, minSpeed, maxSpeed);
                    NPC.velocity = direction * speed;
                }
                else
                {
                    var omega = speed / 48.0f;
                    NPC.velocity = direction.RotatedBy(omega) * speed;
                }
            }
            else if(state == AIState.Marching)
            {
                var dist = delta.Length();
                if(dot > -0.5f)
                {
                    speed = Math.Clamp(speed + acceleration, minSpeed, maxSpeed);
                }
                else if (dot > -0.92f && dist > 384.0f)
                {
                    speed = Math.Clamp(speed - accelerationBrake, minSpeed, maxSpeed);
                }
                else
                {
                    state = AIState.Aiming;
                }
                NPC.velocity = direction * speed;
            }
            else
            {
                state = AIState.Aiming;
            }

            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.570795f;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
        }
    }
}
