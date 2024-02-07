using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Revelation.Tiles.生态.衰竭辐射
{
	internal class 低语头颅 : ModTile
	{
		public override void SetStaticDefaults() {
			// Properties
			Main.tileFrameImportant[Type] = true;

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			//物块宽多少格
			TileObjectData.newTile.Width = 2;
			//物块高多少格
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 ,16};
			TileObjectData.newTile.StyleHorizontal = true; // Optional, if you add more placeStyles for the item 
			TileObjectData.newTile.Origin= new Point16(0,1);// 正x往右，正y往下（反之）
			Main.tileLighted[Type] = true;
			TileObjectData.addTile(Type);
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
{

    {
        r = 0.76f;
        g = 1.53f;
        b = 0.00f;
    }
}

		public override bool CreateDust(int i, int j, ref int type) {
			return false;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		}
	}
}
