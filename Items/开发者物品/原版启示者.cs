using Revelation.Items.材料.神械;
using Revelation.Items.材料.衰竭辐射;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Revelation.Items.开发者物品
{
	public class 原版启示者 : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.damage = 2440;
			Item.crit = 55;
			Item.DamageType = DamageClass.Melee;//
			Item.width =  50;
			Item.height = 50;
			Item.useTime = 11;
			Item.useAnimation = 11;
			Item.useStyle = 1;
			Item.knockBack = 7;
			Item.value = 999999;//价值
			Item.rare = -12;//稀有度
			Item.UseSound = SoundID.Item88;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = 503;
			Item.shootSpeed = 30;
			
		}
		

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<聚合装夹铀>(20);
			recipe.AddIngredient<湮石>(395);
			recipe.Register();
			
			
		}
	}
}