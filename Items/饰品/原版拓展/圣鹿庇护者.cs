using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
namespace Revelation.Items.饰品.原版拓展
{
    public class 圣鹿庇护者 : ModItem
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
			Item.rare = 5;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 6;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
	player.buffImmune[BuffID.Poisoned] = true; // Make the player immune to Fire
	player.buffImmune[BuffID.Darkness] = true; 
	player.buffImmune[BuffID.Chilled] = true; 
	player.buffImmune[BuffID.Frozen] = true; 
	player.buffImmune[BuffID.Blackout] = true; 
	player.AddBuff(87,1);
	player.GetDamage(DamageClass.Generic) -= 0.05f;
	player.GetDamage(DamageClass.Magic) -= 0.50f;
if (Main.hardMode && Main.dayTime) {
	player.lifeRegen += 1;
	player.statLifeMax2 += 50;
}
else if (Main.hardMode && !Main.dayTime) {
player.moveSpeed += 0.10f;
player.AddBuff(11,1);
}
	}// Make the player immune to Fire
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient<烈焰金辉>(1)
			.AddIngredient(1225,25)
			.AddIngredient(899,1)
			.AddIngredient(900,1)
			.AddIngredient(501,35)
			.AddIngredient(520,15)
			.AddTile(TileID.WorkBenches)//铁砧WorkBenches 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench 地狱熔炼Hellforge
			.Register();
		}
	}
}


