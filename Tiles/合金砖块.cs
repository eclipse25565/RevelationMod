using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria. ModLoader;
using Terraria.ObjectData;

namespace Revelation.Tiles
{
	public class 合金砖块 : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			HitSound = SoundID.Tink;
			DustType = 84;
			AddMapEntry(new Color(37, 41, 43));

		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}

	}
}