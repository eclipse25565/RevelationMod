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
    internal class RaiderTail : ModNPC
    {
        public override string Texture => "Revelation/NPCs/BOSS/衰竭辐射/ExampleWormTail";

        private static int Damage => 40;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 36;
            NPC.scale = 2.0f;
            NPC.lifeMax = RaiderHead.Life;
            NPC.defense = 3;
            NPC.knockBackResist = 0.0f;
            NPC.damage = Damage;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.HitSound = new SoundStyle("Revelation/Sound/BOSS音效/袭击者/袭击者受击2");

            Music = RaiderHead.BackgroundMusic;
        }

        public override void AI()
        {
            RaiderBody.CommonAI(this);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}
