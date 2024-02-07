using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Revelation.Buff.正面增益
{
    public class 迷你鲨的恩赐:ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Generic) +=(player.buffTime[buffIndex]/30*0.05F)+(1*0.05F);
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if (player.buffTime[buffIndex] <= 3600)
            {
                player.buffTime[buffIndex] += time;
                return base.ReApply(player, time, buffIndex);
            }
            return base.ReApply(player, time, buffIndex);
        }
    }
}
