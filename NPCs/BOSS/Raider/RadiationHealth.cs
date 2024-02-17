using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class RadiationHealth : ModProjectile
    {
        private int Target => (int)Projectile.ai[0];
        private NPC TargetNPC => Main.npc[Target];

        public override string Texture => "Revelation/NPCs/BOSS/Raider/RadiationProjectile";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.arrow = false;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.scale = 2.25f;
        }

        public override void AI()
        {
            Projectile.velocity = (TargetNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX) * 26.0f;
            Dust.NewDust(Projectile.Center, 0, 0, DustID.YellowStarDust, 0.0f, 0.0f, 0, new Color(0.1f, 0.9f, 0.2f));
            Lighting.AddLight(this.Projectile.Center, new Vector3(0.1f, 0.9f, 0.1f) * this.Projectile.Opacity);

            if (!TargetNPC.active)
            {
                Projectile.Kill();
            }
            else if(Vector2.Distance(Projectile.Center, TargetNPC.Center) <= 75.0f)
            {
                TargetNPC.life += 40;
                TargetNPC.HealEffect(40);
                TargetNPC.netUpdate = true;
                Projectile.Kill();
                Projectile.netUpdate = true;
            }
        }
    }
}
