using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.材料.哀霜
{
	public class 冻霜晶体 : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 7));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
		}

		public override void SetDefaults() {
			Item.width = 25;
			Item.height = 16;
			Item.value = Item.sellPrice(silver: 1);
			Item.rare = 9;
			Item.maxStack = 999;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.Register();
		}
	}
}