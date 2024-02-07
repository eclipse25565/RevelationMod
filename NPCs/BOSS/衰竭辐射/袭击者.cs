using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Revelation.NPCs.普通怪物.破坏者;
using Revelation.Items.生物掉落物.BOSS掉落物.衰竭辐射.袭击者;

namespace Revelation.NPCs.BOSS.衰竭辐射
{
    [AutoloadBossHead]
    // 这三个类展示了如何使用WormHead，WormBody和WormTail类从Worm.cs
    internal class 袭击者 : WormHead
    {
        public override int BodyType => ModContent.NPCType<ExampleWormBody>();

        public override int TailType => ModContent.NPCType<ExampleWormTail>();

        [System.Obsolete]
        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                // 影响如何在最佳iary中显示NPC
                CustomTexturePath = "Revelation/NPCs/BOSS/衰竭辐射/ExampleWorm_Bestiary",
                // 如果NPC是多个部分组成的（如蠕虫），则需要为最佳iary提供自定义纹理
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                 PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }
        public static int Life()
        {
            if (Main.masterMode) return 25000 / 3;
            else if (Main.expertMode) return 25000 / 2;
            else return 25000;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 60;
            NPC.lifeMax = Life();
            NPC.defense = 40;
            NPC.knockBackResist = 0f;
            NPC.scale = 2.0f;
            NPC.DeathSound = SoundID.NPCDeath7;
                NPC.HitSound = new SoundStyle ("Revelation/Sound/BOSS音效/袭击者/袭击者受击2");
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 30;
            NPC.boss = true;
            NPC.value = Item.buyPrice(0, 4, 50, 0);
            Music = MusicLoader.GetMusicSlot("Revelation/Assets/Music/衰竭辐射boss战1");
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // 描述
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("看起来像是掉进了一桶 Aqua 色油漆的 Digger。哦,算了。")
            });
        }

         public override void Init()
        {
            // 长度相关
            MinSegmentLength = 100;
            MaxSegmentLength = 100;

            CommonWormInit(this);
        }

        internal static void CommonWormInit(Worm worm)
        {
            // 速度相关
            worm.MoveSpeed = 35f;
            worm.Acceleration = 0.8f;
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
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (attackCounter > 0)
                {
                attackCounter--;
                }
            Player target = Main.player[NPC.target];
            // 检查玩家位置
            if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 )
                {
                    for (int i = 0; i < 3; i++) 
                    { NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<破坏者>());
                    }
                    NPC.velocity = Vector2.Normalize(target.Center - NPC.Center) * 1.5f;
                    attackCounter = 2000;
                }
                if (attackCounter  < 100 && Vector2.Distance(NPC.Center, target.Center) < 100 )
                {
                Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                direction *= 150;
                NPC.velocity = direction;
                }
                if (attackCounter < 100 && Vector2.Distance(NPC.Center, target.Center) >= 100 )
                {
                    int Timer = 60;
                    Timer++;
                    // 直到360帧（6秒）后变成1
                    float factor = Timer / 10f;
                    factor *= factor;
                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    NPC.velocity = Vector2.Normalize(target.Center - NPC.Center) * 2f;
                }
            }
        }

        internal class ExampleWormBody : WormBody
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
                NPC.width = 30;
                NPC.height = 30;
                NPC.damage = 50;
                NPC.lifeMax = 25000;
                NPC.defense = 40;
                NPC.scale = 2.0f;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath7;
                NPC.HitSound = new SoundStyle ("Revelation/Sound/BOSS音效/袭击者/袭击者受击1");
                NPC.lavaImmune = true;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.npcSlots = 1;
            }

            public override void Init()
            {
                袭击者.CommonWormInit(this);
            }
        }

        internal class ExampleWormTail : WormTail
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
                NPC.width = 30;
                NPC.height = 36;
                NPC.damage = 70;
                NPC.lifeMax = 25000;
                NPC.defense = 100;
                NPC.scale = 2.0f;
                NPC.knockBackResist = 0f;
                NPC.DeathSound = SoundID.NPCDeath7;
                NPC.HitSound = new SoundStyle ("Revelation/Sound/BOSS音效/袭击者/袭击者受击1");
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
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert()); //创建一个掉落规则，在非专家大师模式时掉落
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射组织>(), 1, 20, 35));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射心脏>(), 2, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射利齿>(), 1, 1, 3));//向这个掉落规则里放一个掉落，1/3概率掉落，一次掉落5~15个石头
            npcLoot.Add(notExpertRule);
            //npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<袭击者宝藏袋>())); //向总掉落里放一个石头，这个石头以宝藏袋的标准掉落出来（只在专家大师掉落）
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<袭击者令牌>())); //在大师模式下掉落一个石头
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<袭击者令牌>()));//在大师模式下，给所有玩家，都掉落一个石头，人人有份，每个人在自己的客户端都有一个
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
            SoundEngine.PlaySound(SoundID.Roar, NPC.Center); // 播放吼叫音效
        }
    }
}