using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.饰品.原版拓展
{
    public class 金色护卫 : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
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
			Item.rare = 2;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 1;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
		player.statDefense += player.statLifeMax2/100*4;
		player.GetDamage(DamageClass.Generic) -= 0.08f;
		player.GetCritChance(DamageClass.Generic) -= 1f;
			if (player.statLifeMax2 >=400)
				{
	player.GetDamage(DamageClass.Generic) -= 0.10f;
 }
			}
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient(ItemID.GoldBar,35)
			.AddIngredient(ItemID.Ruby,1)
			.AddIngredient(ItemID.IronskinPotion,3)
			.AddIngredient(ItemID.GoldCoin,1)
			.AddTile(TileID.TinkerersWorkbench)//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();
		}
	}
}


