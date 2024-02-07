using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Revelation.Tiles.建筑;
using Revelation.Items.材料.神械;

namespace Revelation.Items.生物掉落物.BOSS掉落物.衰竭辐射.袭击者
{
    public class 辐射利齿 : ModItem
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
			Item.rare = 7;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;		
		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.Register();
		}
	}
}	

