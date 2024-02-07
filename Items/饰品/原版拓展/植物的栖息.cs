using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.饰品.原版拓展
{
    public class 植物的栖息 : ModItem
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
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
				player.lifeRegen += 1;
				player.statManaMax2 = 40 ;
				player.GetDamage(DamageClass.Magic) += 0.25f;
				player.statLifeMax2 -= 80;
			}
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient<草头角>()
			.AddIngredient(210,20)
			.AddIngredient(191,1)
			.AddIngredient(489,1)
			.AddTile(TileID.TinkerersWorkbench)//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();
		}
	}
}


