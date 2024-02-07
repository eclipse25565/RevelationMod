using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Revelation.Buff;
using Revelation.Buff.召唤buff;

namespace Revelation.Properties.武器弹幕.迷你鲨
{
    public class 迷你鲨召唤物 :ModProjectile
    {
        Player player => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 1;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 25;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 20;
            Projectile.friendly= true; 
            Projectile.penetrate= -1;
            Projectile.aiStyle= -1;
            Projectile.ignoreWater= true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.DamageType = DamageClass.Summon;
        }
        void MT(Vector2 tar,float maxs=20f,float acs = 0.5F) 
        {
            if (Projectile.Center.X - tar.X < 0F) 
            {
                Projectile.velocity.X += Projectile.velocity.X < 0 ? 2 * acs : acs;
            }
            else 
            {
                Projectile.velocity.X -= Projectile.velocity.X < 0 ? 2 * acs : acs;
            }
            if (Projectile.Center.Y - tar.Y < 0F)
            {
                Projectile.velocity.Y += Projectile.velocity.Y < 0 ? 2 * acs : acs;
            }
            else
            {
                Projectile.velocity.Y -= Projectile.velocity.Y < 0 ? 2 * acs : acs;
            }
            if (Math.Abs(Projectile.velocity.X) > maxs) 
            {
                Projectile.velocity.X = maxs*Math.Sign(Projectile.velocity.X);
            }
            if (Math.Abs(Projectile.velocity.Y) > maxs)
            {
                Projectile.velocity.Y = maxs * Math.Sign(Projectile.velocity.Y);
            }
        }
        void ATS(NPC target) 
        {
            int id = 1;
            Projectile.ai[0]++;
            foreach (var por in Main.projectile)
            {
                if (por.type == Projectile.type && por.active && por.whoAmI < Projectile.whoAmI)
                    id++;
            }
            if (id < 9)
            {
                if (Projectile.ai[0] == (10 - id))
                {
                    SoundEngine.PlaySound(SoundID.Item11);
                    Projectile.ai[0] = 0;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, (target.Center - Projectile.Center), 14, Projectile.damage, Projectile.knockBack, Projectile.owner, target.whoAmI);
                }
            }
            else 
            {
                if (Projectile.ai[0] ==2)
                {
                    SoundEngine.PlaySound(SoundID.Item11);
                    Projectile.ai[0] = 0;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, (target.Center - Projectile.Center), 14, Projectile.damage, Projectile.knockBack, Projectile.owner, target.whoAmI);
                }
            }
        }
        public override void AI()
        {
            if (player.HasBuff<迷你鲨buff>() == true) 
            {
                Projectile.timeLeft = 3;
            }
            NPC target = null;
            if (player.HasMinionAttackTargetNPC) 
            {
                target = Main.npc[player.MinionAttackTargetNPC];
                float between=Vector2.Distance(target.Center,Projectile.Center);
                if(between < 2000F) 
                {
                    target = null;
                }
            }
            if(target == null||!target.active) 
            {
                int t = Projectile.FindTargetWithLineOfSight(1145);
                if (t >= 0) 
                {
                    target = Main.npc[t];
                }
            }
            if (target != null)
            {
                if(target.active) 
                {
                    if (Vector2.Distance(player.Center, target.Center) > 2000) 
                    {
                        Vector2 p = Vector2.Lerp(Projectile.Center, player.Center, 0.1f);
                        Projectile.velocity = p - Projectile.Center;
                        return;
                    }
                    Vector2 postion = target.Center + ((Projectile.Center - target.Center).SafeNormalize(Vector2.Zero)*150);
                    MT(postion, 24F, 0.8F);
                    ATS(target);
                }
            }
            else
            {
                Vector2 mypos = player.Center + new Vector2(0, -200);
                float dis = Projectile.Distance(mypos);
                if (dis > 1200)
                {
                    Vector2 p = Vector2.Lerp(Projectile.Center, player.Center, 0.1F);
                    Projectile.velocity = p - Projectile.Center;
                }
                else if (dis >  500)
                {
                    MT(mypos, 20, 0.4F);
                }
                else
                {
                    ss(mypos,0.2F);
                }
            }
        }
        void ss(Vector2 position,float speed=0.5F)
        {
            if (Projectile.localAI[1] < 3) 
            {
                Projectile.position= position;
                Projectile.localAI[1]++;
                return;
            }
            if (Projectile.Center.X < position.X) 
            {
                Projectile.velocity.X += speed;
            }
            if (Projectile.Center.X > position.X)
            {
                Projectile.velocity.X -= speed;
            }
            if (Projectile.Center.Y < position.Y)
            {
                Projectile.velocity.Y += speed;
            }
            if (Projectile.Center.Y > position.Y)
            {
                Projectile.velocity.Y -= speed;
            }
        }
    }
}
