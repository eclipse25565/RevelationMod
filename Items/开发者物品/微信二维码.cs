using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.开发者物品
{
	public class 微信二维码 : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = 750; // The cost of the item in copper coins. (1 = 1 copper, 100 = 1 silver, 1000 = 1 gold, 10000 = 1 platinum)
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.rare = 8;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.开发者建筑.微信二维码>(); // The ID of the wall that this item should place when used. ModContent.TileType<T>() method returns an integer ID of the wall provided to it through its generic type argument (the type in angle brackets)..
			Item.placeStyle = 0;		}
		}
		


	}