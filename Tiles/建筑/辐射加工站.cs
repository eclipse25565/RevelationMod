using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Revelation.Tiles.建筑
{
	internal class 辐射加工站 : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			//物块宽多少格
			TileObjectData.newTile.Width = 4;
			//物块高多少格
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 ,16,16,16 };
			TileObjectData.newTile.StyleHorizontal = true; // Optional, if you add more placeStyles for the item 
			TileObjectData.newTile.Origin= new Point16(1,3);// 正x往右，正y往下（反之）
			TileObjectData.addTile(Type);
		}
			public override void AnimateTile(ref int frame, ref int frameCounter)
{
    frameCounter++;
    //我这里写的是每30帧切换一下
    if (frameCounter >= 15)
    {
        //在0到1之间切换，因为我们的灯笼只有2帧
        frame = frame switch
        {
            0 => 1,
            1 => 2,
			2 => 3,
			3 => 0,
            _ => 0
        };
        frameCounter = 0;
	}
}
	
	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
{
    //根据帧去改变所有的物块帧
    frameXOffset = Main.tileFrame[type] * 112;
}
		public override bool CreateDust(int i, int j, ref int type) {
			return false;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		}
	}
}
