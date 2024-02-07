using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Revelation.Items;
using Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨;

namespace Revelation.NPCs.BOSS.普通BOSS.迷你鲨
{
    [AutoloadBossHead]
    public class 迷你鲨NPC:ModNPC
    {
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<宝藏袋_迷你鲨>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<迷你鲨圣物>()));
            LeadingConditionRule noe=new LeadingConditionRule(new Conditions.NotExpert());
            noe.OnSuccess(ItemDropRule.Common(ModContent.ItemType<迷你鲨剑>(), 5));
            noe.OnSuccess(ItemDropRule.Common(ModContent.ItemType<迷你鲨召唤杖>(),5));
            noe.OnSuccess(ItemDropRule.Common(ModContent.ItemType<迷你鲨弓>(),5));
            noe.OnSuccess(ItemDropRule.Common(98,5));
            noe.OnSuccess(ItemDropRule.Common(ModContent.ItemType<迷你鲨法杖>(), 5));
        }
        public override void SetDefaults()
        {
            NPC.friendly = false;
            NPC.knockBackResist= 0;
            if(Main.expertMode)
            {
                NPC.lifeMax = 1000;
            }
            else if(Main.masterMode)
            {
                NPC.lifeMax = 1150;
            }
            else 
            {
                NPC.lifeMax = 1800;
            }
            
            NPC.defense = 8;
            NPC.boss = true;
            NPC.width = 50;
            NPC.damage = 18;
            NPC.height = 20;
            NPC.noGravity= true;
            NPC.noTileCollide= true;
            NPC.aiStyle = -1;
            Music = MusicID.Boss1;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound= SoundID.NPCDeath14;
            NPC.npcSlots = 10;
        }
        public override void SetStaticDefaults()
        {
            
        }
        
        public ref float timer => ref NPC.ai[0];
        public ref float doing => ref NPC.ai[1];
        public enum xw 
        {
            yd1=1,
            yd2=2,
            kj=0,
            yd3=3
        }
        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[NPC.target];
            int a = (player.Center - NPC.Center).X > 0 ? 1 : -1;
            NPC.spriteDirection = NPC.direction;
            if (a==1) 
            {
                NPC.direction = NPC.spriteDirection = -1;
                NPC.rotation=(player.Center - NPC.Center).ToRotation(); 
            }
            else 
            {
                NPC.direction = NPC.spriteDirection = 1;
                NPC.rotation = (player.Center - NPC.Center).ToRotation()+3.1415926F;
            }
        }
        public override void AI()
        {

            Player player = Main.player[NPC.target];
            if (player.dead || NPC.target < 0 || NPC.target > 255||!player.active) 
            {
                NPC.TargetClosest();
            }
            if(player.dead) 
            {
                NPC.velocity.Y -= 0.086f;
                NPC.EncourageDespawn(10);
                return;
            }
            switch (doing) 
            {
                case(float)xw.kj:
                    kj(player.Center,1);
                    break;
                case (float)xw.yd1:
                    yd1();
                    break;
                case (float)xw.yd2:
                    yd2();
                    break;
                case (float)xw.yd3:
                    yd3();
                    break;
            }
        }
        public void yd1()
        {
            Player player = Main.player[NPC.target];
            if (player.Distance(NPC.Center) < 800) 
            {
                timer++;
                if (NPC.ai[0] <= 10)
                {
                    NPC.velocity = 20 * (player.Center - NPC.Center).SafeNormalize(Vector2.Zero);
                }
                if (NPC.ai[0] > 10)
                {
                    NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.1f);
                }
                if (NPC.ai[0] == 60)
                {
                    NPC.velocity = Vector2.Zero;
                }
                if (timer == 80) 
                {
                    doing = Main.rand.Next(1, 3);
                    timer = 0;
                }
            }
            else
            {

                doing = (float)xw.kj;
            }
        }
        public void yd2() 
        {
            Player player = Main.player[NPC.target];
            Vector2 pos=player.Center+(NPC.Center-player.Center).SafeNormalize(Vector2.Zero)*220;
            kj(pos,2);
            var en = NPC.GetSource_FromAI();
            timer++;
            if (timer % 5 == 0)
            {
                Projectile.NewProjectile(en, NPC.Center, (player.Center - NPC.Center), 180, 10, NPC.whoAmI);
            }
            else if (timer == 80)
            {
                doing = Main.rand.Next(1, 3);
                timer = 0;
            }
        }
        public void yd3() 
        {
            var en = NPC.GetSource_FromAI();
            Player player = Main.player[NPC.target];
            Vector2 pos = player.Center + new Vector2(0, -200);
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
            timer++;
            if (timer % 5 == 0)
            {
                Projectile.NewProjectile(en, NPC.Center, (player.Center - NPC.Center), 180, 10, NPC.whoAmI);
            }
            else if (timer == 60)
            {
                doing = Main.rand.Next(1, 3);
                timer = 0;
            }
        }
        public override void OnKill()
        {
            if (击败判断.killminisha)
            {
            }
            else
            {
                击败判断.killminisha = true;
                //Main.NewText("[c/AA000:算你NB]");
                Main.NewText("[c/AA000:迷你鲨的封印已被解开]");
            }

        }
        public override bool CheckDead()
        {
            return true;
        }
        public bool candeah=false;
        public void kj(Vector2 t,int a) 
        {
            Player player = Main.player[NPC.target];
            if (NPC.Center.X - t.X<0) 
            {
                NPC.velocity.X += NPC.velocity.X < 0 ? 1F : 0.5F;
            }
            if (NPC.Center.X - t.X > 0)
            {
                NPC.velocity.X -= NPC.velocity.X < 0 ? 1F : 0.5F;
            }
            if (NPC.Center.Y - t.Y < 0)
            {
                NPC.velocity.Y += NPC.velocity.Y < 0 ? 1F : 0.5F;
            }
            if (NPC.Center.Y - t.Y > 0)
            {
                NPC.velocity.Y -= NPC.velocity.Y < 0 ? 1F : 0.5F;
            }
            if (Math.Abs(NPC.velocity.X )>20F) 
            {
                NPC.velocity.X =20F*Math.Sign(NPC.velocity.X);
            }
            if (Math.Abs(NPC.velocity.Y) > 20F)
            {
                NPC.velocity.Y = 20F * Math.Sign(NPC.velocity.Y);
            }
            if (player.Distance(NPC.Center) < 200)
            {
                doing = Main.rand.Next(1, 3);
                timer = 0;
            }
        }
    }
}
