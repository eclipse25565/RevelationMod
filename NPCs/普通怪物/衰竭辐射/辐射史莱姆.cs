using Microsoft.Xna.Framework;
using Revelation.Buff.负面减益;
using Revelation.Items.生物掉落物.普通怪物.衰竭辐射;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.普通怪物.衰竭辐射
{
    public class 辐射史莱姆:ModNPC
    {
        public override void SetDefaults()//数值有一点点离谱而已
        {
            NPC.damage = 80;
            NPC.defense = 25;
            NPC.lifeMax = 480;
            NPC.life = 480;
            NPC.width= 32;
            NPC.height= 22;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.friendly = false;
            NPC.value=Item.buyPrice(0,0,7,5);
            NPC.knockBackResist = 0.33F;
            NPC.aiStyle = -1;
        }
        public override void SetStaticDefaults() 
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public enum now 
        {
            sleep,
            jump,
            fall
        }
        public ref float doing => ref NPC.ai[0];
        public ref float Ftimer => ref NPC.ai[1];
        public ref float timer => ref NPC.ai[2];
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (doing == 0)
            {
                Ftimer = 0;
                for (int i = 0; i < 4; i++)
                {
                    Ftimer++;
                    if (Ftimer == 5)
                    {
                        NPC.frame.Y = frameHeight * 0;
                    }
                    if (Ftimer == 10)
                    {
                        NPC.frame.Y = frameHeight * 1;
                        Ftimer = 0;
                    }
                    
                };
            }
            else
            {

                Ftimer++;
                if (Ftimer == 10)
                {
                    NPC.frame.Y = frameHeight * 0;
                }
                if (Ftimer == 15)
                {
                    NPC.frame.Y = frameHeight * 1;
                    Ftimer = 0;
                }
                
            }      
            
        }
        public override void AI()
        {
            if (doing == 0)
            {
                NPC.TargetClosest(true);
                timer++;
                
                if (timer >= 40)
                {
                    NPC.velocity = new Vector2(NPC.direction * 0, 0);
                    doing = (float)now.jump;
                    timer = 0;
                }
            }
            else if (doing == 1)
            {
                timer++;
                if (timer % 10 != 0) 
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center,new Vector2(NPC.direction*2, 0), 44,80,NPC.whoAmI);//生成弹幕，随便改
                }
                if (timer >= 5)
                {
                    doing = (float)now.fall;
                    timer = 0;
                }
                NPC.velocity = new Vector2(NPC.direction * 2, -10F);
            }
            else if (doing == 2)
            {

                timer++;
                if (timer >= 100&&NPC.velocity.Y==0)
                {
                    doing = (float)now.sleep;
                    timer = 0;
                }
            }
            else 
            {
                doing =0;
            }
                    
            
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
       npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<辐射溶块>(), 2,3,8));
       //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<辐射溶块>(), 概率，最小数，最大数))


        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
       target.AddBuff(ModContent.BuffType<致死辐射>(),300);//1s=60帧
        }
    }
}
