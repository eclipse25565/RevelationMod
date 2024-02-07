using Microsoft.Xna.Framework;
using Revelation.NPCs.BOSS.普通BOSS.迷你鲨;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.神械
{
    [AutoloadBossHead]
    public class 红械判定者 : ModNPC
    {
        public ref float jd => ref NPC.ai[0]; 
        public ref float doing => ref NPC.ai[1]; 
        public ref float timer => ref NPC.ai[2];
        public ref float ccs=>ref NPC.ai[3];
        public override void SetDefaults()
        {

            if (Main.expertMode == true) 
            {
                NPC.lifeMax = 25000;
                NPC.damage = 30;
            }
            else if(Main.masterMode == true) 
            {
                NPC.lifeMax = 25000;
                NPC.damage = 35;
            }
            else 
            {
                NPC.lifeMax = 30000;
                NPC.damage = 40;
            }
            NPC.friendly = false;
            NPC.boss = true;
            NPC.noGravity= true;
            NPC.knockBackResist = 0;
            NPC.noTileCollide= true;
            NPC.npcSlots = 20;
            Music = MusicID.Boss2;
            NPC.value = Item.buyPrice(0, 18, 0, 0);
            NPC.lavaImmune= true;
            NPC.defense = 20;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.width = 168;
            NPC.height = 134;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(69, 1200);
            target.AddBuff(39, 1200);
        }
        public ref Player player => ref Main.player[NPC.target];
        public override void FindFrame(int frameHeight)
        {


        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            int a = (player.Center - NPC.Center).X > 0 ? 1 : -1;
            NPC.spriteDirection = NPC.direction;
            if (a == 1)
            {
                NPC.direction = NPC.spriteDirection = -1;
                NPC.rotation = (player.Center - NPC.Center).ToRotation();
            }
            else
            {
                NPC.direction = NPC.spriteDirection = 1;
                NPC.rotation = (player.Center - NPC.Center).ToRotation() + 3.1415926F;
            }
            int prd = Main.masterMode ?10:Main.expertMode?15:20 ;
            #region 索敌+脱战判断
            if (NPC.target >= 255 || !player.active || player.dead||NPC.target<=0) 
            {
                NPC.TargetClosest();
            }
            
            if (player.dead||player.Distance(NPC.Center)>10000F) 
            {
                NPC.velocity.Y -= 0.3F;
                NPC.EncourageDespawn(10);
            }
            #endregion
            Vector2 tar=player.Center- NPC.Center;
            #region 1阶段
            if (jd == 0) 
            {
                if (NPC.life <= NPC.lifeMax / 2)
                {
                    
                    NPC.dontTakeDamage = true;
                    NPC.velocity = Vector2.Zero;
                    for (int i = 0; i < 10; timer++)
                    {
                        if (timer % 10 == 0)
                        {
                            NPC.rotation += MathHelper.TwoPi / 36;
                            NPC.life += ((NPC.lifeMax-NPC.life) / 10);
                            NPC.HealEffect(NPC.lifeMax / 2 / 10);
                            i++;
                        }
                    }
                    ccs = 2;
                    jd = 1;
                    doing = 0;
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<无人机>());
                    NPC.dontTakeDamage=false;
                }

                
                switch (doing)
                {
                    #region 靠近
                    case 0:
                        ccs = 2;
                        if (NPC.Center.X - player.Center.X < 0)
                        {
                            NPC.velocity.X += NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.X - player.Center.X > 0)
                        {
                            NPC.velocity.X -= NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - player.Center.Y < 0)
                        {
                            NPC.velocity.Y += NPC.velocity.Y < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - player.Center.Y > 0)
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
                            doing = Main.rand.Next(1, 4);
                        }
                        break;
                    #endregion
                    #region 发射散弹
                    case 1:
                        Vector2 pos1 =player.Center + ((NPC.Center - player.Center).SafeNormalize(Vector2.Zero) * 400);
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
                            doing = Main.rand.Next(1, 4);
                        }
                        timer++;
                        if (timer % 20 == 0)
                        {
                            for (int i = -3; i < 3; i++)
                            {
                                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                                int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, tar.RotatedBy(i * MathHelper.Pi / 36), 104, prd, NPC.whoAmI);
                                Main.projectile[j].friendly = false;
                                Main.projectile[j].hostile = true;
                                Main.projectile[j].timeLeft = 120;
                                Main.projectile[j].tileCollide= false;
                            }
                        }
                        if (timer >= 60) 
                        {
                           
                            doing = Main.rand.Next(1, 4);
                            timer = 0;
                        }
                        break;
                    #endregion
                    #region 发射灵液
                    case 2:
                        timer++;
                        int x;
                        if (Main.rand.Next(1, 2) == 1) 
                        {
                             x = 200;
                        }
                        else 
                        {
                             x = -200;
                        }
                            Vector2 pos = player.Center + new Vector2(0, x);
                        if (NPC.Center.X < pos.X)
                        {
                            NPC.velocity.X += 0.2F;
                        }
                        if (NPC.Center.X > pos.X)
                        {
                            NPC.velocity.X -= 0.2F;
                        }
                        if (NPC.Center.Y < pos.Y)
                        {
                            NPC.velocity.Y += 0.2F;
                        }
                        if (NPC.Center.Y > pos.Y)
                        {
                            NPC.velocity.Y -= 0.2F;
                        }

                        if (timer % 15 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                            int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, tar.RotatedBy(Main.rand.NextFloat(0.05F,-0.05F)), 279, prd, NPC.whoAmI);
                            Main.projectile[j].friendly= false;
                            Main.projectile[j].timeLeft = 120;
                            Main.projectile[j].tileCollide = false;
                            Main.projectile[j].hostile= true;
                        }
                        if (timer >= Main.rand.Next(80,100))
                        {
                            timer = 0;
                            doing = Main.rand.Next(1, 4);
                        }
                        break;
                    #endregion
                    #region 冲刺
                    case 3:
                        
                        timer++;
                        
                        if (timer <= 10) 
                        {
                            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                            NPC.velocity = 33f * (player.Center-NPC.Center).SafeNormalize(Vector2.Zero);
                            
                        }
                        if(timer >=10) 
                        {
                            NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.09f);
                        }
                        if(timer >= 30 ) 
                        {                      
                            timer = 0;
                            ccs--;
                        }
                        if (ccs <= 0) 
                        {
                            ccs=Main.rand.Next(1,3);
                            timer = 0;
                            doing = Main.rand.Next(1, 4);
                        }
                        break;
                        #endregion
                }
            }
            #endregion 
            else if(jd==1)
            {
                if (NPC.life < 100&& NPC.AnyNPCs(ModContent.NPCType<无人机>())) 
                {
                    NPC.dontTakeDamage= true;
                }
                else 
                {
                    NPC.dontTakeDamage= false;
                }
                switch (doing)
                {
                    #region 靠近
                    case 0:
                        ccs = 2;
                        if (NPC.Center.X - player.Center.X < 0)
                        {
                            NPC.velocity.X += NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.X - player.Center.X > 0)
                        {
                            NPC.velocity.X -= NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - player.Center.Y < 0)
                        {
                            NPC.velocity.Y += NPC.velocity.Y < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - player.Center.Y > 0)
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
                            timer = 0;
                            doing = Main.rand.Next(0, 3);
                        }
                        break;
                    #endregion
                    #region 冲刺
                    case 1:

                        timer++;

                        if (timer <= 10)
                        {
                            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                            NPC.velocity = 33f * (player.Center - NPC.Center).SafeNormalize(Vector2.Zero);

                        }
                        if (timer >= 10)
                        {
                            NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.09f);
                        }
                        if (timer >= 30)
                        {
                            timer = 0;
                            ccs--;
                        }

                        if (ccs <= 0)
                        {
                            ccs = Main.rand.Next(1, 3);
                            timer = 0;
                            doing = Main.rand.Next(0, 3);
                        }
                        break;
                    #endregion
                    #region 灵液
                    case 2:
                        Vector2 Center = player.Center;
                        timer++;
                        Vector2 pos = player.Center + new Vector2(0, -200);
                        if (NPC.Center.X < pos.X)
                        {
                            NPC.velocity.X += 0.1F;
                        }
                        if (NPC.Center.X > pos.X)
                        {
                            NPC.velocity.X -= 0.2F;
                        }
                        if (NPC.Center.Y < pos.Y)
                        {
                            NPC.velocity.Y += 0.1F;
                        }
                        if (NPC.Center.Y > pos.Y)
                        {
                            NPC.velocity.Y -= 0.1F;
                        }
                        if (timer % 15 == 0) 
                        {
                            for (int i = -3; i < 3; i++)
                           
                            {
                                int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, tar.RotatedBy(Main.rand.NextFloat(0.05F, -0.05F)), 592, prd, NPC.whoAmI);
                                Main.projectile[j].friendly = false;
                                Main.projectile[j].timeLeft = 120;
                                Main.projectile[j].tileCollide = false;
                                Main.projectile[j].hostile = true;
                            }
                        }
                        if (timer >= 30) 
                        {
                            timer = 0;
                            doing = Main.rand.Next(0, 3);
                        }
                        break;
                        #endregion
                }
            }
        }
    }
}
