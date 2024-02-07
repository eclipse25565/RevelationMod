using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class PoisonDart : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.timeLeft = 20 * 60;
            Projectile.width = 12;
            Projectile.height = 14;
            Projectile.scale = 1.0f;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.04f;
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }
    }
}
