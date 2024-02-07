
using Revelation.Items.材料.哀霜;
using Revelation.Tiles.建筑;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.武器.近战.哀霜
{
	public class 冰之哀霜 : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 288;
			Item.crit = 14;
			Item.DamageType = DamageClass.Melee;//
			Item.width =  50;
			Item.height = 50;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 999999;//价值
			Item.rare = 9;//稀有度
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<沉哀锭>(20);
			recipe.AddIngredient<冻霜晶体>(10);
			recipe.AddIngredient(ItemID.Frostbrand,1);
			recipe.AddIngredient(ItemID.IceBlade,1);
			recipe.AddIngredient(ItemID.SoulofMight,25);
			recipe.AddIngredient(ItemID.TrueNightsEdge,1);
			recipe.AddTile<合金加工站>();
			recipe.Register();
		}
	}
}