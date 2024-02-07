using Terraria;
using Terraria.ModLoader;
using Revelation.Properties;
using Revelation.Properties.武器弹幕.迷你鲨;

namespace Revelation.Buff.召唤buff
{
    public class 迷你鲨buff : ModBuff 
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<迷你鲨召唤物>()]>0) 
            {
                player.buffTime[buffIndex] = 114514;
            }
            else 
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }
}
