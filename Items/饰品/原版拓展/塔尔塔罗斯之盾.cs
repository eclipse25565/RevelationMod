using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.饰品.原版拓展
{
    public class 塔尔塔罗斯之盾 : ModItem
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
			Item.defense = 4;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
    player.statLifeMax2 += 100;
	player.lifeRegen +=1;
	player.noKnockback = true;
	player.buffImmune[BuffID.Burning] = true; // Make the player immune to Fire
	player.buffImmune[BuffID.OnFire] = true; // Make the player immune to Fire
	player.GetAttackSpeed(MeleeDamageClass.Melee) += 0.07f;
	player.GetCritChance(MeleeDamageClass.Melee) += 0.07f;
	player.GetDamage(DamageClass.Melee) += 0.5f;
	if (player.statLifeMax2 >=350)
				{
    player.statDefense += 4 ;
	player.GetDamage(DamageClass.Generic) += 0.10f;
 }
			}
		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient<守护者之盾>(1)
			.AddIngredient<疾刃之盾>(1)
			.AddIngredient(ItemID.HellstoneBar,25)
			.AddIngredient(ItemID.MeteoriteBar,15)
			.AddIngredient(ItemID.ObsidianBrick,50)
			.AddTile(TileID.Hellforge)//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();
		}
	}
}


