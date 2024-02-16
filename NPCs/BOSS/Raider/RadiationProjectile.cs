using Microsoft.Xna.Framework;
using Revelation.Buff.负面减益;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class RadiationProjectile : ModProjectile
    {
        public override string Texture => "Revelation/NPCs/BOSS/Raider/RadiationProjectile";

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
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<致死辐射>(), 420);
        }
    }
}
