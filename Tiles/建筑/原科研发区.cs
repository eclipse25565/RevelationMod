using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Revelation.Tiles.建筑
{
	internal class 原科研发区 : ModTile
	{
		public override void SetStaticDefaults() {
			// Properties
			Main.tileFrameImportant[Type] = true;

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			//物块宽多少格
			TileObjectData.newTile.Width = 19;
			//物块高多少格
			TileObjectData.newTile.Height = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 ,16,16,16,16,16,16,16, 16 ,16,16,16,16,16,16};
			TileObjectData.newTile.StyleHorizontal = true; // Optional, if you add more placeStyles for the item 
			TileObjectData.newTile.Origin= new Point16(2,15);// 正x往右，正y往下（反之）
			TileObjectData.addTile(Type);
		}

		public override bool CreateDust(int i, int j, ref int type) {
			return false;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		}
	}
}
