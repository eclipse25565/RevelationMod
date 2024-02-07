using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Revelation.Items.材料.衰竭辐射;
using Revelation.Tiles.建筑;

namespace Revelation.Items.饰品.衰竭辐射
{
    public class 辐射花 : ModItem
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
			Item.rare = 1;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
	player.buffImmune[20] = true; 
	player.AddBuff(79,20);
			}
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient<小块铀棒>(15)
			.AddIngredient(208,1)
			.AddIngredient(522,10)
			.AddTile<辐射加工站>()//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();
		}
	}
}


