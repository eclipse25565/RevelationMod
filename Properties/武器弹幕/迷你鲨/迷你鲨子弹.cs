using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Revelation.Properties.武器弹幕.迷你鲨
{
    public class 迷你鲨子弹:ModProjectile
    {
        public override void SetDefaults() 
        {
            Projectile.width = 2;
            Projectile.height = 20;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
            Projectile.friendly = false;
            Projectile.aiStyle = 3;
        }
    }
}
