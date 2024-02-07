using Revelation.NPCs.BOSS.衰竭辐射;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Revelation.NPCs.Raider
{
    internal class RaiderTail : RaiderBody
    {
        public override string Texture => "Revelation/NPCs/BOSS/衰竭辐射/ExampleWormTail";

        private static new int Damage => 40;

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 32;
            NPC.height = 36;
            NPC.scale = 2.0f;
            NPC.defense = 3;
            NPC.damage = Damage;
        }
    }
}
