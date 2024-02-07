using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Revelation.Properties.武器弹幕.迷你鲨
{
    public class 迷你鲨弹幕:ModProjectile
    {
        public override void SetDefaults() 
        {
            Projectile.width = 37;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 110;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void AI()
        {
            Projectile.rotation += 3;
        }
    }
}
