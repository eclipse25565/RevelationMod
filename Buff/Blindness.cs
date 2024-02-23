using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static tModPorter.ProgressUpdate;

namespace Revelation.Buff
{
    internal class Blindness : ModBuff
    {
        public override string Texture => "Revelation/Buff/负面减益/致死辐射";

        public override void SetStaticDefaults()
        {
            Main.debuff[ModContent.BuffType<Blindness>()] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(Main.netMode != NetmodeID.Server && player == Main.LocalPlayer)
            {
                var ticksLeft = player.buffTime[buffIndex];
                var active = Filters.Scene["RevelationBlindness"].IsActive();

                
                if (ticksLeft > 1)
                {
                    if(ticksLeft > 20)
                    {
                        Filters.Scene.Activate("RevelationBlindness").GetShader().UseProgress(0.0f);
                    }
                    else
                    {
                        var progress = 1.0f - (ticksLeft - 2) / 18.0f;
                        Filters.Scene.Activate("RevelationBlindness").GetShader().UseProgress(progress);
                    }
                }
                else
                {
                    Filters.Scene.Deactivate("RevelationBlindness");
                }
            }
        }
    }
}
