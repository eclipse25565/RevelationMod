using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.饰品.原版拓展
{
    public class 腐蚀项链 : ModItem
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
			Item.rare = 3;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
			if (player.statDefense <=120)
			{
		player.GetDamage(DamageClass.Melee) += player.statDefense/25*0.03f;
		player.GetDamage(DamageClass.Magic) += player.statLifeMax2/100*0.08f;
		player.GetDamage(DamageClass.Ranged) += player.moveSpeed/0.08f*0.04f;
			}

			}
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient(ItemID.DemoniteBar,20)
			.AddIngredient(ItemID.VileMushroom,5)
			.AddIngredient(ItemID.EbonstoneBlock,20)
			.AddIngredient(ItemID.ShadowScale,10)
			.AddIngredient(ItemID.Obsidian,5)
			.AddIngredient(ItemID.Amethyst,4)
			.AddTile(TileID.DemonAltar)//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();
		}
	}
}


