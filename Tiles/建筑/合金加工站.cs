using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Revelation.Tiles.建筑
{
	internal class 合金加工站 : ModTile
	{
		public override void SetStaticDefaults() {
			// Properties
			Main.tileFrameImportant[Type] = true;

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 ,16};
			TileObjectData.newTile.StyleHorizontal = true; // Optional, if you add more placeStyles for the item 

			// Etc
			TileObjectData.addTile(Type);
		}

		public override bool CreateDust(int i, int j, ref int type) {
			return false;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		}
	}
}
