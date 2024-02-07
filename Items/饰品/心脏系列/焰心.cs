using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Revelation.Buff.负面减益;


namespace Revelation.Items.饰品.心脏系列
{
    public class 焰心 : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 59; // Influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.value = 35000; // The cost of the item in copper coins. (1 = 1 copper, 100 = 1 silver, 1000 = 1 gold, 10000 = 1 platinum)
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.rare = -13;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 15;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.noKnockback = true;//防击退
            player.GetDamage(DamageClass.Generic) += 0.15f; // 单独增益
            player.GetCritChance(DamageClass.Generic) += 15; // 单独增益
			if (hideVisual)
			{
				player.AddBuff(ModContent.BuffType<极限系统过载>(), 20);
				player.AddBuff(21, 20);
				player.GetDamage(DamageClass.Generic) += 1.25f;
				player.GetCritChance(DamageClass.Generic) += 0.25f;
				player.GetAttackSpeed(DamageClass.Generic) += 0.45f;
				player.endurance += 0.45f;
				player.statDefense += 85;
			}
        }   
   
			public override void AddRecipes() {
			CreateRecipe()//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();

			}
	}
}


