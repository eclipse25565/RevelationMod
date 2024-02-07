using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.饰品.原版拓展
{
    public class 烈焰金辉 : ModItem
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
			Item.rare = 4;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 3;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
	player.buffImmune[BuffID.Burning] = true; // Make the player immune to Fire
	player.buffImmune[BuffID.OnFire] = true; 
	if (player.statLifeMax2 <=350)
				{
    player.statDefense += 4 ;
	player.GetDamage(DamageClass.Melee) += 0.15f;
	player.GetCritChance(MeleeDamageClass.Melee) += 0.08f;
 }
	}// Make the player immune to Fire
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient<金色护卫>(1)
			.AddIngredient(ItemID.Ruby,1)
			.AddIngredient(ItemID.HellstoneBar,25)
			.AddIngredient(ItemID.WarriorEmblem,1)
			.AddTile(TileID.Hellforge)//铁砧WorkBenches 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench 地狱熔炼Hellforge
			.Register();
		}
	}
}


