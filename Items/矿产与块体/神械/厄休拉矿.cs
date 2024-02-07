using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.矿产与块体.神械
{
    public class 厄休拉矿 : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 59; 
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = 750;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.rare = 1;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.厄休拉矿>();
			Item.placeStyle = 0;		}

	}
}
