using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.生物掉落物.BOSS掉落物.衰竭辐射.袭击者
{
    public class 袭击者令牌:ModItem
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<辐射利齿>(), 1, 5, 8));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<辐射组织>(), 1, 20, 35));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<辐射心脏>(), 1, 1, 1));
            // 从 ModNPC.ModifyNPCLoot 中复制非专家掉落
            //itemLoot.Add(ItemDropRule.Common(ItemID.JungleYoyo)); // 专家模式特定掉落
        }
    }
}
