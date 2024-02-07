using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.神械
{
    public class 无人机:ModNPC
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 10000;
            NPC.damage = 20;
            NPC.defense = 20;
            NPC.width = 48;
            NPC.height = 66;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.npcSlots = 10;
            NPC.noGravity = true;
            NPC.knockBackResist= 0;
            NPC.noTileCollide = true;
            NPC.boss = true;
        }
        public ref float doing => ref NPC.ai[1];
        public ref float timer => ref NPC.ai[2];
        public ref float ccs => ref NPC.ai[3];
        public ref Player player => ref Main.player[NPC.target];
        public override void AI()
        {
            int a = (player.Center - NPC.Center).X > 0 ? 1 : -1;
            NPC.spriteDirection = NPC.direction;
            if (a == 1)
            {
                NPC.direction = NPC.spriteDirection = -1;
            }
            else
            {
                NPC.direction = NPC.spriteDirection = 1;
            }
            ccs = 3;
            int PRD=20;
            #region 索敌+脱战判断
            if (NPC.target >= 255 || !player.active || player.dead || NPC.target <= 0)
            {
                NPC.TargetClosest();
            }

            if (player.dead || player.Distance(NPC.Center) > 10000F)
            {
                NPC.velocity.Y -= 0.3F;
                NPC.EncourageDespawn(10);
            }
            #endregion
            Vector2 tar = player.Center - NPC.Center;
            switch (doing)
            {
                #region 瞬移
                case 0:
                    timer++;
                    if (timer % 45 == 0)
                    {
                        for (int i = -2; i < 1; i++)
                        {
                            SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                            int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, tar.RotatedBy(i * MathHelper.Pi / 36), 104, PRD, NPC.whoAmI);
                            Main.projectile[j].friendly = false;
                            Main.projectile[j].hostile = true;
                            Main.projectile[j].timeLeft = 120;
                            Main.projectile[j].tileCollide = false;
                        }
                    }
                    if (NPC.alpha == 255) 
                    {
                        Vector2 pos = player.position - new Vector2(Main.rand.Next(-300, 300), Main.rand.Next(-300, 300));
                        if(player.Distance(pos) < 100) 
                        {
                            pos = player.position - new Vector2(Main.rand.Next(-300, 300), Main.rand.Next(-300, 300));
                        }
                        else 
                        {
                            timer = 0;
                            NPC.position= pos;
                            for (int i = 0; i < 51; i++)
                            {
                                    NPC.alpha -= 5;
                            }
                            doing=Main.rand.Next(0,3); 
                            break;
                        }
                    }
                    else 
                    {
                        if (timer >= 60)
                        {
                            NPC.alpha += 5;
                        }
                    }
                    break;
                #endregion
                #region 发射散弹
                case 1:
                    Vector2 pos1 = player.Center + ((NPC.Center - player.Center).SafeNormalize(Vector2.Zero) * 400);
                    if (NPC.Center.X - pos1.X < 0)
                    {
                        NPC.velocity.X += NPC.velocity.X < 0 ? 1F : 0.5F;
                    }
                    if (NPC.Center.X - pos1.X > 0)
                    {
                        NPC.velocity.X -= NPC.velocity.X < 0 ? 1F : 0.5F;
                    }
                    if (NPC.Center.Y - pos1.Y < 0)
                    {
                        NPC.velocity.Y += NPC.velocity.Y < 0 ? 1F : 0.5F;
                    }
                    if (NPC.Center.Y - pos1.Y > 0)
                    {
                        NPC.velocity.Y -= NPC.velocity.Y < 0 ? 1F : 0.5F;
                    }
                    if (Math.Abs(NPC.velocity.X) > 20F)
                    {
                        NPC.velocity.X = 20F * Math.Sign(NPC.velocity.X);
                    }
                    if (Math.Abs(NPC.velocity.Y) > 20F)
                    {
                        NPC.velocity.Y = 20F * Math.Sign(NPC.velocity.Y);
                    }
                    if (player.Distance(NPC.Center) < 300)
                    {
                        doing = Main.rand.Next(0, 3);
                    }
                    timer++;
                    if (timer % 20 == 0)
                    {
                        for (int i = -3; i < 3; i++)
                        {
                            SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                            int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, tar.RotatedBy(i * MathHelper.Pi / 36), 104, PRD, NPC.whoAmI);
                            Main.projectile[j].friendly = false;
                            Main.projectile[j].hostile = true;
                            Main.projectile[j].timeLeft = 120;
                            Main.projectile[j].tileCollide = false;
                        }
                    }
                    if (timer >= 60)
                    {

                        doing = Main.rand.Next(0, 3);
                        timer = 0;
                    }
                    break;
                #endregion
                #region 冲刺
                case 2:

                    timer++;

                    if (timer <= 10)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.velocity = 40f * (player.Center - NPC.Center).SafeNormalize(Vector2.Zero);

                    }
                    if (timer >= 10)
                    {
                        if (timer % 10 == 0)
                        {
                            int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center,tar, 242, PRD, NPC.whoAmI);
                            Main.projectile[j].friendly = false;
                            Main.projectile[j].timeLeft = 120;
                            Main.projectile[j].tileCollide = false;
                            Main.projectile[j].hostile = true;
                        }
                        NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.09f);
                    }
                    if (timer >= 30)
                    {
                        timer = 0;
                        doing = Main.rand.Next(1, 3);
                    }
                    break;
                    #endregion
            }
        }
    }
}
