using Revelation.NPCs.BOSS.衰竭辐射;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Revelation.NPCs.Raider
{
    internal class RaidingDestroyerHead : WormHead
    {
        public override string Texture => "Revelation/NPCs/普通怪物/破坏者/破坏者";

        private static void CommonInit(Worm worm)
        {
            worm.MoveSpeed = 10.0f;
            worm.Acceleration = 4.5f;
        }


        public override int BodyType => ModContent.NPCType<RaidingDestroyerBody>();
        private class RaidingDestroyerBody : WormBody
        {
            public override string Texture => "Revelation/NPCs/普通怪物/破坏者/破坏者2";
            public override void Init()
            {
                CommonInit(this);
            }



            public override void SetDefaults()
            {
                NPC.CloneDefaults(NPCID.WyvernBody);
                NPC.width = 15;
                NPC.height = 14;
                NPC.scale = 1.5f;
                NPC.lifeMax = Life;
                NPC.damage = Damage;
                NPC.defense = 10;
                NPC.npcSlots = 0.5f;
                NPC.noGravity = true;
                NPC.HitSound = SoundID.NPCHit1;
                NPC.DeathSound = SoundID.NPCDeath1;
                NPC.value = 0;
            }
        }

        public override int TailType => ModContent.NPCType<RaidingDestroyerTail>();
        private class RaidingDestroyerTail : WormTail
        {
            public override string Texture => "Revelation/NPCs/普通怪物/破坏者/破坏者3";
            public override void Init()
            {
                CommonInit(this);
            }

            public override void SetDefaults()
            {
                NPC.CloneDefaults(NPCID.WyvernTail);
                NPC.width = 15;
                NPC.height = 12;
                NPC.scale = 1.5f;
                NPC.lifeMax = Life;
                NPC.damage = Damage;
                NPC.npcSlots = 0.5f;
                NPC.noGravity = true;
                NPC.HitSound = SoundID.NPCHit1;
                NPC.DeathSound = SoundID.NPCDeath1;
                NPC.value = 0;
            }
        }

        public override void Init()
        {
            CommonInit(this);
            MaxSegmentLength = 15;
            MinSegmentLength = 8;
        }

        public static int Life => 200;
        private static int Damage => 40;

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.WyvernHead);
            NPC.width = 15;
            NPC.height = 15;
            NPC.scale = 1.5f;
            NPC.lifeMax = Life;
            NPC.damage = Damage;
            NPC.npcSlots = 0.5f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0;
        }
    }
}
