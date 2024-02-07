using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Revelation.Items.材料.神械;
using Revelation.Tiles.建筑;

namespace Revelation.Items.套装.纳米
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class 纳米护靴 : ModItem
	{
		public override void SetStaticDefaults() {

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare =6; // The rarity of the item
			Item.defense = 18; // The amount of defense the item will give when equipped
		}
		public override void UpdateEquip(Player player) {
				player.moveSpeed += 0.08f; // Increase the movement speed of the player
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<能量晶体>(20);
                  recipe.AddIngredient<合金甲板>(30);
			recipe.AddTile<合金加工站>();
			recipe.Register();
		}
	}
}
