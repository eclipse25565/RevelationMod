using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Revelation.Projectiles.开发者弹幕;

namespace Revelation.Items.开发者物品
{
    public class 超级猫猫爪 : ModItem
	{
		
		public override void SetDefaults()
		{
            Item.damage = 128;
			Item.DamageType = DamageClass.Generic;
			Item.knockBack = 1;
			Item.crit = 5;
			Item.useStyle = 1;
			Item.width = 15;
			Item.height = 15;
			Item.scale = 1;
			Item.value = 999999;
			Item.rare =-12;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.useTime= 15;
			Item.useAnimation = 15;
			Item.shoot = ModContent.ProjectileType<小鱼干>();
			Item.shootSpeed = 24f;
			Item.UseSound = SoundID.Item57;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.statLife += 10;
            player.HealEffect(10);
			return true;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(3063, 3);
			recipe.AddIngredient(3013, 1);
			recipe.AddIngredient(3467, 30);
			recipe.AddIngredient(2304, 1);
			recipe.AddTile(412);
			recipe.Register();
		}
	}

}