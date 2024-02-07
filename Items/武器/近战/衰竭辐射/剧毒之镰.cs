
using Revelation.Items.材料.衰竭辐射;
using Revelation.Items.矿产与块体.衰竭辐射;
using Revelation.Items.饰品.衰竭辐射;
using Revelation.Tiles.建筑;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.武器.近战.衰竭辐射
{
	public class  剧毒之镰 : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 89;
			Item.crit = 30;
			Item.DamageType = DamageClass.Melee;//
			Item.width =  50;
			Item.height = 50;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 999999;//价值
			Item.rare = 7;//稀有度
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<聚合装夹铀>(20);
			recipe.AddIngredient<辐射花>(1);
			recipe.AddIngredient<衰竭矿石>(80);
			recipe.AddIngredient(ItemID.SoulofMight,30);
			recipe.AddTile<合金加工站>();
			recipe.Register();
		}
	}
}