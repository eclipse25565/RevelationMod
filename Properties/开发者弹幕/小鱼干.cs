using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace Revelation.Properties.开发者弹幕
{
    public class 小鱼干 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 180;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Default;
            Projectile.aiStyle = 225;
            AIType = 225;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
            Projectile.penetrate = -1;

        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }




    }
}