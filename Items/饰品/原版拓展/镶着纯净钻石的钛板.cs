using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.饰品.原版拓展
{
    public class 镶着纯净钻石的钛板 : ModItem
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
			Item.rare = 6;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 8;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
				player.statLifeMax2 += 100;
			}
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient(1527,1)
			.AddIngredient(1198,10)
			.AddIngredient(520,45)
			.AddIngredient(549,15)
			.AddIngredient(1291,1)
			.AddTile(TileID.TinkerersWorkbench)//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			//524精金熔炉 525秘银砧
			.Register();
		}
	}
}


