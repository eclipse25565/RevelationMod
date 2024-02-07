using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace Revelation.NPCs
{
    internal abstract class WormDog : WormLikeHead
    {
        protected abstract float MaxSpeed { get; }
        protected abstract float Acceleration { get; }
        protected abstract float MaxAngleSpeed { get; }

        private float speed = 0.0f;

        public override void AI()
        {
            base.AI();

            speed = Math.Min(speed + Acceleration, MaxSpeed);

            var target = Main.player[NPC.target];
            var delta = target.Center - NPC.Center;
            var expectedDirection = delta.SafeNormalize(Vector2.UnitX);

            var direction = NPC.velocity.SafeNormalize(Vector2.UnitX);

            float omegaMax = 0.0f;
            if (speed > 0.0f)
            {
                omegaMax = Math.Min(Math.Abs(Acceleration / speed), MaxAngleSpeed);
            }
            var omega = Math.Clamp(direction.AngleTo(expectedDirection), -omegaMax, omegaMax);
            if (Math.Abs(omega) > 0)
            {
                speed = Math.Min(Math.Abs(Acceleration / omega), MaxSpeed);
            }


            NPC.velocity = direction.RotatedBy(omega) * speed;
        }
    }
}
