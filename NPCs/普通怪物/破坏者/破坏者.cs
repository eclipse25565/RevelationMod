using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Revelation.NPCs.BOSS;
using Revelation.NPCs.BOSS.衰竭辐射;

namespace Revelation.NPCs.普通怪物.破坏者
{
    // 这三个类展示了如何使用WormHead，WormBody和WormTail类从Worm.cs
    internal class 破坏者 : WormHead
    {
        public override int BodyType => ModContent.NPCType<破坏者2>();

        public override int TailType => ModContent.NPCType<破坏者3>();

        public override void SetStaticDefaults()
        {
        }
        public static int Life()
        {
            if (Main.masterMode) return 1000 / 3;
            else if (Main.expertMode) return 1000 / 2;
            else return 1000;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.width = 29;
            NPC.height = 31;
            NPC.damage = 10;
            NPC.lifeMax = Life();
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.scale = 1.5f;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 30;
            NPC.boss = true;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
        }
        public override void Init()
        {
            // 长度相关
            MinSegmentLength = 3;
            MaxSegmentLength = 5;

            CommonWormInit(this);
        }

        internal static void CommonWormInit(Worm worm)
        {
            // 速度相关
            worm.MoveSpeed = 20f;
            worm.Acceleration = 0.5f;
        }

        private int attackCounter;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void AI()
        {
            NPC.ai[0]++;
            NPC.TargetClosest(true);
            Player p = Main.player[NPC.target];
            if (NPC.ai[0] <= 200)
            {
                NPC.velocity = 10 * (p.Center - NPC.Center).SafeNormalize(Vector2.Zero);
            }
            if (NPC.ai[0] > 200 && NPC.ai[0] < 400)
            {
                NPC.velocity = Vector2.Zero;
                NPC.life += 1;
            }
            if (NPC.ai[0] == 400)
            {
                NPC.ai[0] = 0;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.expertMode && NPC.AnyNPCs(ModContent.NPCType<袭击者>()) && NPC.CountNPCS(ModContent.NPCType<破坏者>()) < 5)
                return 5f;
            else return 0f;
        }
        internal class 破坏者2 : WormBody
        {
            [System.Obsolete]
            public override void SetStaticDefaults()
            {
                // 隐藏该NPC在最佳iary中的显示，适用于多部分NPC，仅希望有一个条目。
                NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
                {
                    Hide = true
                };
                NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            }

            public override void SetDefaults()
            {
                NPC.CloneDefaults(NPCID.DiggerBody);
                NPC.aiStyle = -1;
                NPC.width = 27;
                NPC.height = 19;
                NPC.damage = 5;
                NPC.lifeMax = 25000;
                NPC.defense = 10;
                NPC.scale = 1.5f;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath7;
                NPC.HitSound = SoundID.NPCHit5;
                NPC.lavaImmune = true;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.npcSlots = 1;
            }

            public override void Init()
            {
                CommonWormInit(this);
            }
        }

        internal class 破坏者3 : WormTail
        {
            [System.Obsolete]
            public override void SetStaticDefaults()
            {
                NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
                {
                    Hide = true
                };
                NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            }

            public override void SetDefaults()
            {
                NPC.CloneDefaults(NPCID.DiggerTail);
                NPC.aiStyle = -1;
                NPC.width = 25;
                NPC.height = 24;
                NPC.damage = 7;
                NPC.lifeMax = 25000;
                NPC.defense = 10;
                NPC.scale = 1.5f;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath7;
                NPC.HitSound = SoundID.NPCHit5;
                NPC.lavaImmune = true;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.npcSlots = 20;
            }

            public override void Init()
            {
                袭击者.CommonWormInit(this);
            }
        }
        public override void OnKill()//尸块死亡相关
        {
            int Gore1 = Mod.Find<ModGore>("Gore1").Type;
            int Gore2 = Mod.Find<ModGore>("Gore2").Type;
            var entitySource = NPC.GetSource_Death();
            for (int i = 0; i < 3; i++)
            {
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Gore1);
            }
            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Gore2); // 生成3个gore1，一个gore2
        }
    }
}