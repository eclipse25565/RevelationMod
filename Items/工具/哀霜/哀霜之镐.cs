using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;
using Revelation.Items.材料.哀霜;

namespace Revelation.Items.工具.哀霜
{
	public class 哀霜之镐 : ModItem
	{
		public override void SetStaticDefaults() {

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 36;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			Item.pick = 200; // 镐力数值，详见: https://terraria.wiki.gg/zh/wiki/%E9%95%90%E5%8A%9B
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<沉哀锭>(20);
			recipe.AddIngredient<冻霜晶体>(10)
				.Register();
		}
	}
}
