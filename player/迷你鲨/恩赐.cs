using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Revelation.player
{
    public class 恩赐:ModPlayer
    {
        public bool enc=false;
        public override void ResetEffects()
        {
            enc = false;
        }
    }
}
