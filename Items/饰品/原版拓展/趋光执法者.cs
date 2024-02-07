using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.饰品.原版拓展
{
    public class 趋光执法者 : ModItem
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
			Item.rare = 8;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 5;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
	player.buffImmune[BuffID.Burning] = true; // Make the player immune to Fire
	player.buffImmune[BuffID.OnFire] = true; 
	player.noKnockback = true;
	player.GetDamage(DamageClass.Generic) += 0.12f;
	player.GetCritChance(MeleeDamageClass.Generic) += 0.10f;
	player.GetAttackSpeed(MeleeDamageClass.Generic) += 0.10f;

	if (player.statLifeMax2 <=200)
				{
    player.statDefense += 30 ;
	player.GetDamage(DamageClass.Generic) += 0.15f;
 				}
	}// Make the player immune to Fire
		public override void AddRecipes() {
			CreateRecipe()
			
			.AddIngredient<圣鹿庇护者>(1)
			.AddIngredient<塔尔塔罗斯之盾>(1)
			.AddIngredient<镶着纯净钻石的钛板>(1)
			.AddIngredient<全能者勋章>(1)
			.AddIngredient(547,20)
			.AddIngredient(548,20)
			.AddIngredient(549,20)
			.AddTile(TileID.Hellforge)//铁砧WorkBenches 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench 地狱熔炼Hellforge
			.Register();
		}
	}
}


