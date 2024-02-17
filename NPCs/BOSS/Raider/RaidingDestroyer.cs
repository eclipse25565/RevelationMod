using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class RaidingDestroyerHead : WormDog
    {
        private static int Life => 200;
        private static int Damage => 20;

        protected override int BodyType => ModContent.NPCType<RaidingDestroyerBody>();
        private class RaidingDestroyerBody : WormLikeBody
        {
            public override void SetDefaults()
            {
                NPC.aiStyle = -1;
                NPC.width = 15;
                NPC.height = 14;
                NPC.scale = 1.5f;
                NPC.damage = Damage;
                NPC.lifeMax = Life;
                NPC.defense = 0;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath1;
                NPC.HitSound = SoundID.NPCHit1;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.value = 0;
            }
        }

        protected override int TailType => ModContent.NPCType<RaidingDestroyerTail>();
        private class RaidingDestroyerTail : WormLikeTail
        {
            public override void SetDefaults()
            {
                NPC.aiStyle = -1;
                NPC.width = 15;
                NPC.height = 12;
                NPC.scale = 1.5f;
                NPC.damage = Damage;
                NPC.lifeMax = Life;
                NPC.defense = 0;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath1;
                NPC.HitSound = SoundID.NPCHit1;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.value = 0;
            }
        }
        protected override int SegmentLength => Main.rand.Next(7, 17);

        protected override float MaxSpeed => 13.0f;

        protected override float Acceleration => 1.7f;

        protected override float MaxAngleSpeed => 0.104719f;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 15;
            NPC.height = 15;
            NPC.scale = 1.5f;
            NPC.damage = Damage;
            NPC.lifeMax = 400;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = 0;
        }
    }
}
