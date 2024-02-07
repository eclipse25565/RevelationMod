using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
namespace Revelation.Items.饰品.心脏系列
{
    public class 人工心脏 : ModItem
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
			Item.rare = 8;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 15;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{

			player.GetDamage(DamageClass.Generic) += 0.15f; // 单独增益
			player.GetCritChance(DamageClass.Generic) += 15; // 单独增益
			player.noKnockback = true;//防击退
			player.lifeRegen += 10;
			player.endurance += 0.1f;
			player.moveSpeed += 0.15f;
			player.statLifeMax2 += 40;
			player.lifeRegenTime += 25;
        }   
   
			public override void AddRecipes() {
			CreateRecipe()//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();

			}
	}
}


