using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Revelation.Tiles;
using Terraria.ObjectData;

namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 迷你鲨圣物:ModItem
    {
        public override void SetDefaults() 
        {
            Item.width = Item.height = 20;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.useTime = 20;
            Item.useAnimation= 15;
            Item.maxStack = 9999;
            Item.createTile = ModContent.TileType<迷你鲨圣物图块>();
        }
    }
}
