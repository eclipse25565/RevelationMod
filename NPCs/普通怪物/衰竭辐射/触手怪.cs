using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace Revelation.NPCs.普通怪物.衰竭辐射
{
    public class 触手怪:ModNPC
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 100;
            NPC.width = 26;
            NPC.height = 46;
            NPC.damage = 30;
            NPC.width = 22;
            NPC.height= 46;
            NPC.defense = 20;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.aiStyle = -1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist= 0;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }
        public ref float ftimer => ref NPC.ai[2];
        public override void AI() 
        {
            NPC.velocity.X = 0;
        }
        public override void FindFrame(int frameHeight)
        {
            ftimer++;
            if (ftimer == 10) 
            {
                NPC.frame.Y = frameHeight * 1;
            }
            if (ftimer == 20)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            if (ftimer == 30)
            {
                NPC.frame.Y = frameHeight * 3;
            }
             if (ftimer == 40)
            {
                NPC.frame.Y = frameHeight * 4;
            }
            if (ftimer == 50)
            {
                NPC.frame.Y = frameHeight * 0;
                ftimer = 0;
            }
        }
    }
}
