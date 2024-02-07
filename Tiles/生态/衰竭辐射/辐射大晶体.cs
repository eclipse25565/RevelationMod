using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Revelation.Tiles.生态.衰竭辐射
{
	internal class 辐射大晶体 : ModTile
	{
		public override void SetStaticDefaults() {
			// Properties
			Main.tileFrameImportant[Type] = true;

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			//物块宽多少格
			TileObjectData.newTile.Width = 8;
			//物块高多少格
			TileObjectData.newTile.Height = 10;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 ,16,16,16,16,16,16,16, 16 };
			TileObjectData.newTile.StyleHorizontal = true; // Optional, if you add more placeStyles for the item 
			TileObjectData.newTile.Origin= new Point16(0,8);// 正x往右，正y往下（反之）
			TileObjectData.addTile(Type);
		}

		public override bool CreateDust(int i, int j, ref int type) {
			return false;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		}
	}
}
