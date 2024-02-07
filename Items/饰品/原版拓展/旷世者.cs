using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Revelation.Tiles.建筑;
using Revelation.Items.材料.神械;
using Revelation.Items.材料.衰竭辐射;

namespace Revelation.Items.饰品.原版拓展
{
    public class 旷世者 : ModItem
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
			Item.rare = -13;
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
	player.GetDamage(DamageClass.Generic) += 0.10f;
	player.GetCritChance(MeleeDamageClass.Generic) += 0.50f;
	player.GetAttackSpeed(MeleeDamageClass.Generic) += 0.50f;

	if (player.statLifeMax2 >=200)
				{
    player.statDefense += 35 ;
	player.GetDamage(DamageClass.Generic) += 0.05f;
 				}
	}// Make the player immune to Fire
		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient<趋光执法者>(1)
			.AddIngredient<聚合装夹铀>(8)
			.AddIngredient<能量晶体>(35)
			.AddTile<原科研发区>()//铁砧WorkBenches 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench 地狱熔炼Hellforge
			.Register();
		}
	}
}


