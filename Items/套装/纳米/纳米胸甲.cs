using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Revelation.Tiles.建筑;
using Revelation.Items.材料.神械;

namespace Revelation.Items.套装.纳米
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class 纳米胸甲 : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 35); // How many coins the item is worth
			Item.rare =6; // The rarity of the item
			Item.defense = 20; // The amount of defense the item will give when equipped
		}
		

		public override void UpdateEquip(Player player) {
			player.buffImmune[BuffID.OnFire] = true; // Make the player immune to Fire
			player.statManaMax2 += 40; // Increase how many mana points the player can have by 20
				player.moveSpeed += 0.05f; // Increase the movement speed of the player
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
			public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<能量晶体>(30);
                  recipe.AddIngredient<合金甲板>(35);
			recipe.AddTile<合金加工站>();
			recipe.Register();
		}
	}
}
