using Revelation.Buff;
using Revelation.Buff.正面增益;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Revelation.Properties.武器弹幕.迷你鲨
{
    public class 恩赐子弹:ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width= 22;
            Projectile.height= 22;
            //Projectile.localNPCHitCooldown = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater= true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            //Projectile.usesIDStaticNPCImmunity= true;
            Projectile.penetrate = 5;
            AIType = 14;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage *= ((100 - (5-Projectile.penetrate))/100);
            Main.player[Projectile.owner].AddBuff(ModContent.BuffType<迷你鲨的恩赐>(), 900);
        }
    }
}
