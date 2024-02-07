using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Revelation.Items.材料.衰竭辐射;
using Revelation.Tiles.建筑;
using Revelation.Items.材料.哀霜;

namespace Revelation.Items.饰品.哀霜
{
    public class 哀霜腰带 : ModItem
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
	player.moveSpeed += 0.10f;
	player.GetAttackSpeed(DamageClass.Ranged) -= 2;
			}
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient<沉哀锭>(10)
			.AddIngredient(963,1)
			.AddIngredient(664,25)
			.AddIngredient<冻霜晶体>(1)
			.AddTile<合金加工站>()//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();
		}
	}
}


