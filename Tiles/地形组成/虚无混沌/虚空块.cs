using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria. ModLoader;

namespace Revelation.Tiles.地形组成.虚无混沌
{
	public class 虚空块 : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileStone[Type] = true;
			MineResist = 8f;//物块被挖掘时受到“伤害”的系数，越大则越难以破坏
			MinPick = 150;//能被挖掘需要的最小镐力，默认0
			HitSound = SoundID.Tink;//物块被挖掘时的声音
			DustType = DustID.Dirt;//产生的粒子
			Main.tileMergeDirt[Type] = true;	
				}
		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}

		// todo: implement
		// public override void ChangeWaterfallStyle(ref int style) {
		// 	style = mod.GetWaterfallStyleSlot("ExampleWaterfallStyle");
		// }
	}
}