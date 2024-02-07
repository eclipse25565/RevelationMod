using Revelation.Items.材料.地狱;
using Revelation.Items.材料.虚无混沌;
using Revelation.Tiles.建筑;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.武器.近战.虚无混沌
{
	public class 噩梦缠绕 : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 155;
			Item.crit = 14;
			Item.DamageType = DamageClass.Melee;//
			Item.width =  50;
			Item.height = 50;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 999999;//价值
			Item.rare = 11;//稀有度
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<灼热之光>(20);
			recipe.AddIngredient<远古魂魄>(10);
			recipe.AddIngredient<咒狱锭>(35);
			recipe.AddIngredient(ItemID.Frostbrand,1);
			recipe.AddIngredient(ItemID.IceBlade,1);
			recipe.AddIngredient(ItemID.SoulofMight,25);
			recipe.AddIngredient(ItemID.TrueNightsEdge,1);
			recipe.AddTile<合金加工站>();
			recipe.Register();
		}
	}
}