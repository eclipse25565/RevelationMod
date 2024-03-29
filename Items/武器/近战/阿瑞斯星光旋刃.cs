﻿using Microsoft.Xna.Framework;
using Revelation.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.武器.近战
{
	public class 阿瑞斯星光旋刃 : ModItem
	{
        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
			ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			// Common Properties
			Item.rare = ItemRarityID.Pink; // Assign this item a rarity level of Pink
			Item.value = Item.sellPrice(silver: 10); // The number and type of coins item can be sold for to an NPC

			// Use Properties
			Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
			Item.useAnimation = 25; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useTime = 25; // The length of the item's use time in ticks (60 ticks == 1 second.)
			Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
			Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			// Weapon Properties
			Item.damage = 150;
			Item.width = 25;
			Item.height = 25;
            Item.knockBack = 6.5f;
			Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
            Item.DamageType = DamageClass.Melee; 
            Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            Item.crit = 5;
            // Projectile Properties
            Item.shootSpeed = 25f; // The speed of the projectile measured in pixels per frame.
            Item.shoot = ModContent.ProjectileType<阿瑞斯星光旋刃1>();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const int NumProjectiles = 2;

			for (int i = 0; i < NumProjectiles; i++)
			{
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

				newVelocity *= 1f - Main.rand.NextFloat(0.3f);

				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

				Projectile p = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<阿瑞斯星光旋刃2>(), damage, knockback, player.whoAmI);

            }
			return false;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.Register();
		}
	}
}
