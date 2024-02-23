using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Revelation.Common.Systems
{
	public class DownedBossSystem : ModSystem
	{
		public static bool downedMinionBoss = false;

		public override void ClearWorld() {
			downedMinionBoss = false;
		}

		public override void SaveWorldData(TagCompound tag) {
			if (downedMinionBoss) {
				tag["downedMinionBoss"] = true;
			}

		}

		public override void LoadWorldData(TagCompound tag) {
			downedMinionBoss = tag.ContainsKey("downedMinionBoss");
		}

		public override void NetSend(BinaryWriter writer) {
			var flags = new BitsByte();
			flags[0] = downedMinionBoss;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader) {
			BitsByte flags = reader.ReadByte();
			downedMinionBoss = flags[0];
		}
	}
}
