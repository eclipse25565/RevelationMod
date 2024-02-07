using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Revelation.NPCs.BOSS.普通BOSS.迷你鲨
{
    public class 击败判断:ModSystem
    {
        public static bool killminisha = false;
        internal static bool 裂隙之眼;

        public override void ClearWorld()
        {
            killminisha = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (killminisha) 
            {
                tag["killminisha"] = true;
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            killminisha = tag.ContainsKey("killminisha");
        }
    }
}
