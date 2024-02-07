using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Revelation.NPCs.Raider
{
    internal class RaiderProjectile : ModProjectile
    {
        public override string Texture => "Revelation/NPCs/Raider/辐射粒子";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.arrow = false;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 2.25f;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 240;
        }

        public override void AI()
        {
            this.Projectile.Opacity = Projectile.timeLeft / 300.0f * 0.7f + 0.3f;
            Lighting.AddLight(this.Projectile.Center, new Vector3(0.1f, 0.9f, 0.1f) * this.Projectile.Opacity);

            Player closest = null;
            var closestDist = 999999999.0f;
            Vector2 closestDelta = Vector2.Zero;
            foreach(var player in Main.player)
            {
                var delta = player.Center - Projectile.Center;
                var dist = delta.Length();
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestDelta = delta;
                    closest = player;
                }
            }

            if ( closest != null && closestDist < 400.0f )
            {
                Projectile.velocity = (closestDelta / closestDist) * (1 - closestDist / 800.0f) * 2.5f;
            }
        }
    }
}
