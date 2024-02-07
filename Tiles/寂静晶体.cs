using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria. ModLoader;

namespace Revelation.Tiles
{
	public class 寂静晶体 : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileStone[Type] = true;
			Main.tileSpelunker[Type] = true;//是否高亮
			Main.tileOreFinderPriority[Type] = 400;//侦测优先等级（金矿260叶绿矿700）
			Main.tileShine2[Type] = true;//是否能发出闪光粒子，就像矿物一样，另外光照如果太低的话将无法发出粒子
			Main.tileShine[Type] = 800;//发出闪亮粒子的“频率”，这个数字越大则“频率”越低
			MineResist = 8f;//物块被挖掘时受到“伤害”的系数，越大则越难以破坏
			MinPick = 200;//能被挖掘需要的最小镐力，默认0
			HitSound = SoundID.Tink;//物块被挖掘时的声音
			DustType = DustID.Dirt;//产生的粒子
			TileID.Sets.Ore[Type] = true;//是否为矿石
			Main.tileLighted[Type] = true;//是否发光
			Main.tileMergeDirt[Type] = true;	
				}

public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
{

    {
        r = 2.55f;
        g = 0.51f;
        b = 2.55f;
    }
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