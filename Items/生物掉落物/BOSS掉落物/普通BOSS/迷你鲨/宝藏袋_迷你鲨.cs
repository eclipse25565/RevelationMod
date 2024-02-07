using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 宝藏袋_迷你鲨:ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        }
        public override void SetDefaults() 
        {
            Item.maxStack =9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {


            itemLoot.Add(ItemDropRule.NotScalingWithLuck(73,1,1,3));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<迷你鲨剑>(),5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<迷你鲨召唤杖>(),5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<迷你鲨法杖>(),5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(98,5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<迷你鲨弓>(), 5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<迷你鲨的恩赐>(), 1));
        }
    }
}
