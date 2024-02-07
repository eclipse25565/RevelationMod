using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.放置物
{
    public class 辐射加工站 : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 59; // Influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = 750; // The cost of the item in copper coins. (1 = 1 copper, 100 = 1 silver, 1000 = 1 gold, 10000 = 1 platinum)
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.rare = 8;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.建筑.辐射加工站>(); // The ID of the wall that this item should place when used. ModContent.TileType<T>() method returns an integer ID of the wall provided to it through its generic type argument (the type in angle brackets)..
			Item.placeStyle = 0;		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

	}
}
