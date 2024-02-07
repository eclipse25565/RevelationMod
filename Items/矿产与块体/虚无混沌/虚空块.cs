using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.Items.矿产与块体.虚无混沌
{
    public class 虚空块 : ModItem
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
		    Item.rare = 2;
			Item.useTime = 7;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.地形组成.虚无混沌.虚空块>();
			Item.placeStyle = 0;		}

	}
}
