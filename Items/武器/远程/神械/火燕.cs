using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.武器.远程.神械
{
	public class 火燕 : ModItem
	{
		public override void SetStaticDefaults()
		{
		}


        public override void SetDefaults()
        {
            Item.damage = 103;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 10;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 1000;
            Item.rare = 8;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 89;
            Item.shootSpeed = 20f;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Bullet;

      	}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.Register();
		}
	}
}