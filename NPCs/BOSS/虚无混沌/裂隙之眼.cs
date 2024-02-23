using Revelation.Common.Systems;
using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Revelation.Items.生物掉落物.BOSS掉落物.衰竭辐射.袭击者;
using Terraria.Audio;

namespace Revelation.NPCs.BOSS.虚无混沌
{				
	[AutoloadBossHead]//地图图标
	public class 裂隙之眼 : ModNPC
	{
		public static int secondStageHeadSlot = -1;
		public bool SecondStage {
			get => NPC.ai[0] == 1f;
			set => NPC.ai[0] = value ? 1f : 0f;
		}
		public Vector2 FirstStageDestination {
			get => new Vector2(NPC.ai[1], NPC.ai[2]);
			set {
				NPC.ai[1] = value.X;
				NPC.ai[2] = value.Y;
			}
		}
        		public bool SpawnedMinions {
			get => NPC.localAI[0] == 1f;
			set => NPC.localAI[0] = value ? 1f : 0f;
		}
        private const int FirstStageTimerMax = 90;
        public ref float FirstStageTimer => ref NPC.localAI[1];
		public ref float RemainingShields => ref NPC.localAI[2];
        public ref float SecondStageTimer_SpawnEyes => ref NPC.localAI[3];
        public Vector2 LastFirstStageDestination { get; set; } = Vector2.Zero;
        public static int MinionCount() {
			int count = 15;

			if (Main.expertMode) {
				count += 5; 
			}
			return count;
		}
        //boss头像2
		public override void Load() {
			string texture = BossHeadTexture + "_2"; 
			secondStageHeadSlot = Mod.AddBossHeadTexture(texture, -1); 
		}
        		public override void BossHeadSlot(ref int index) {
			int slot = secondStageHeadSlot;
			if (SecondStage && slot != -1) {
				index = slot;
			}
		}

        private int x;
        private int ccs;
        private float i;

        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BoundGoblin];
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
				CustomTexturePath = "Revelation/NPCs/BOSS/虚无混沌/裂隙之眼_图鉴",
				PortraitScale = 0.6f, 
				PortraitPositionYOverride = 0f,
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
		}


		public override void SetDefaults()
		{
			
			NPC.width = 158;
			NPC.height = 164;
			NPC.damage = 60;
			NPC.defense = 35;
			NPC.lifeMax = 12500;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 760f;
			NPC.knockBackResist = .50f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AnimationType = NPCID.BoundGoblin;
			NPC.boss = true;
			Music = MusicLoader.GetMusicSlot("Revelation/Assets/Music/虚无混沌boss战1");
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot) {
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<袭击者令牌>()));
			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<纪念章>(), 10));
			//npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<圣物>()));
			//npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<宠物>(), 4));
			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			//notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<面具>(), 7));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射组织>(), 1, 20, 35));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射心脏>(), 2, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<辐射利齿>(), 1, 1, 3));
            npcLoot.Add(notExpertRule);
        }
        public override void OnKill() {

			NPC.SetEventFlagCleared(ref DownedBossSystem.downedMinionBoss, -1);
            Main.NewText("[c/9370DB:你以为战胜的是我，其实黑暗悄然无息...]");
		}
        public override void BossLoot(ref string name, ref int potionType) 
        {

        }   
        public override bool CanHitPlayer(Player target, ref int cooldownSlot) {
			cooldownSlot = ImmunityCooldownID.Bosses; 
			return true;
		} 
        public override void FindFrame(int frameHeight) {
			int startFrame = 0;
			int finalFrame = 6;

			if (SecondStage) {
				startFrame = 7;
				finalFrame = Main.npcFrameCount[NPC.type] - 1;

				if (NPC.frame.Y < startFrame * frameHeight) {
					NPC.frame.Y = startFrame * frameHeight;
				}
			}

			int frameSpeed = 5;
			NPC.frameCounter += 0.5f;
			NPC.frameCounter += NPC.velocity.Length() / 10f; 
			if (NPC.frameCounter > frameSpeed) {
				NPC.frameCounter = 0;
				NPC.frame.Y += frameHeight;

				if (NPC.frame.Y > finalFrame * frameHeight) {
					NPC.frame.Y = startFrame * frameHeight;
				}
			}
		}
        public override void HitEffect(NPC.HitInfo hit) {
			if (Main.netMode == NetmodeID.Server) {
				return;
			}

			if (NPC.life <= 0) {
				int backGoreType = Mod.Find<ModGore>("尸块1").Type;
				int frontGoreType = Mod.Find<ModGore>("尸块2").Type;

				var entitySource = NPC.GetSource_Death();

				for (int i = 0; i < 2; i++) {
					Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), backGoreType);
					Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), frontGoreType);
				}

				SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
			}
		}
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            int a = (player.Center - NPC.Center).X > 0 ? 1 : -1;
            NPC.spriteDirection = NPC.direction;
            if (a == 1)
            {
                NPC.direction = NPC.spriteDirection = -1;
                NPC.rotation = (player.Center - NPC.Center).ToRotation();
            }
            else
            {
                NPC.direction = NPC.spriteDirection = 1;
                NPC.rotation = (player.Center - NPC.Center).ToRotation() + 3.1415926F;
            }
            int prd = Main.masterMode ?10:Main.expertMode?15:20 ;
            #region 索敌+脱战判断
            if (NPC.target >= 255 || !player.active || player.dead||NPC.target<=0) 
            {
                NPC.TargetClosest();
            }
            
            if (player.dead||player.Distance(NPC.Center)>10000F) 
            {
                NPC.velocity.Y -= 0.3F;
                NPC.EncourageDespawn(10);
            }
            #endregion
            Vector2 tar=player.Center- NPC.Center;
			
			float velMax = 1f;
			float acceleration = 0.011f;
			NPC.TargetClosest(true);
			Vector2 center = NPC.Center;
			float deltaX = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - center.X;
			float deltaY = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - center.Y;
			float distance = (float)Math.Sqrt(((double)deltaX * (double)deltaX) + ((double)deltaY * (double)deltaY));
			NPC.ai[1] += 1f;
			if ((double)NPC.ai[1] > 600.0)
			{
				acceleration *= 10f;
				velMax = 10f;
				if ((double)NPC.ai[1] > 650.0)
				{
					NPC.ai[1] = 0f;
				}
			}
			else if ((double)distance < 250.0)
			{
				NPC.ai[0] += 0.9f;
				if (NPC.ai[0] > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.019f;
				}
				else
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.019f;
				}
				if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
				{
					NPC.velocity.X = NPC.velocity.X + 0.019f;
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X - 0.019f;
				}
				if (NPC.ai[0] > 200f)
				{
					NPC.ai[0] = -200f;
				}
			}
			if ((double)distance > 350.0)
			{
				velMax = 10f;
				acceleration = 5f;
			}
			else if ((double)distance > 300.0)
			{
				velMax = 10f;
				acceleration = 3f;
			}
			else if ((double)distance > 250.0)
			{
				velMax = 1.5f;
				acceleration = 0.1f;
			}
			float stepRatio = velMax / distance;
			float velLimitX = deltaX * stepRatio;
			float velLimitY = deltaY * stepRatio;
			if (Main.player[NPC.target].dead)
			{
				velLimitX = (float)((double)((float)NPC.direction * velMax) / 2.0);
				velLimitY = (float)((double)(-(double)velMax) / 2.0);
			}
			if (NPC.velocity.X < velLimitX)
			{
				NPC.velocity.X = NPC.velocity.X + acceleration;
			}
			else if (NPC.velocity.X > velLimitX)
			{
				NPC.velocity.X = NPC.velocity.X - acceleration;
			}
			if (NPC.velocity.Y < velLimitY)
			{
				NPC.velocity.Y = NPC.velocity.Y + acceleration;
			}
			else if (NPC.velocity.Y > velLimitY)
			{
				NPC.velocity.Y = NPC.velocity.Y - acceleration;
			
			}
			
			                      else  if (Main.rand.Next(1, 2) == 1) 
                        {
                             x = 200;
                        }
                        else 
                        {
                             x = -200;
                        }
                            Vector2 pos = player.Center + new Vector2(0, x);
                        if (NPC.Center.X < pos.X)
                        {
                            NPC.velocity.X += 0.2F;
                        }
                        if (NPC.Center.X > pos.X)
                        {
                            NPC.velocity.X -= 0.2F;
                        }
                        if (NPC.Center.Y < pos.Y)
                        {
                            NPC.velocity.Y += 0.2F;
                        }
                        if (NPC.Center.Y > pos.Y)
                        {
                            NPC.velocity.Y -= 0.2F;
                        }
                         if(NPC.ai[0]<=200)
    {
					 
                        {
                            SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                            int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, tar.RotatedBy(Main.rand.NextFloat(0.05F,-0.05F)), 279, prd, NPC.whoAmI);
                            Main.projectile[j].friendly= false;
                            Main.projectile[j].timeLeft = 120;
                            Main.projectile[j].tileCollide = false;
                            Main.projectile[j].hostile= true;
                        }
                        }
						if (NPC.life<=500)
    {
        NPC.velocity = Vector2.Zero;
		Main.NewText("[c/9370DB:还远着呢...]");

                    #region 靠近
                    
                        ccs = 2;
                        if (NPC.Center.X - player.Center.X < 0)
                        {
                            NPC.velocity.X += NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.X - player.Center.X > 0)
                        {
                            NPC.velocity.X -= NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - player.Center.Y < 0)
                        {
                            NPC.velocity.Y += NPC.velocity.Y < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - player.Center.Y > 0)
                        {
                            NPC.velocity.Y -= NPC.velocity.Y < 0 ? 1F : 0.5F;
                        }
                        if (Math.Abs(NPC.velocity.X) > 20F)
                        {
                            NPC.velocity.X = 20F * Math.Sign(NPC.velocity.X);
                        }
                        if (Math.Abs(NPC.velocity.Y) > 20F)
                        {
                            NPC.velocity.Y = 20F * Math.Sign(NPC.velocity.Y);
                        }
                        if (player.Distance(NPC.Center) < 300)


						  
                        if (NPC.Center.X - pos.X < 0)
                        {
                            NPC.velocity.X += NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.X - pos.X > 0)
                        {
                            NPC.velocity.X -= NPC.velocity.X < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - pos.Y < 0)
                        {
                            NPC.velocity.Y += NPC.velocity.Y < 0 ? 1F : 0.5F;
                        }
                        if (NPC.Center.Y - pos.Y > 0)
                        {
                            NPC.velocity.Y -= NPC.velocity.Y < 0 ? 1F : 0.5F;
                        }
                        if (Math.Abs(NPC.velocity.X) > 20F)
                        {
                            NPC.velocity.X = 20F * Math.Sign(NPC.velocity.X);
                        }
                        if (Math.Abs(NPC.velocity.Y) > 20F)
                        {
                            NPC.velocity.Y = 20F * Math.Sign(NPC.velocity.Y);
                        }
                        if (player.Distance(NPC.Center) < 300)
                        {
                        
                        {
                            for (int i = -3; i < 3; i++)
                            {    if(NPC.ai[0]==400)
    {
        NPC.position=p.position + new Vector2(0,-200);
        NPC.ai[0]=0;
    }
}
                                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                                int j = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, tar.RotatedBy(i * MathHelper.Pi / 36), 104, prd, NPC.whoAmI);
                                Main.projectile[j].friendly = false;
                                Main.projectile[j].hostile = true;
                                Main.projectile[j].timeLeft = 120;
                                Main.projectile[j].tileCollide= false;
							}
						}
						}
	}
		

    internal class p
    {
        internal static Vector2 position;

    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileX;
			int tile = (int)Main.tile[x, y].TileType;
			return (tile == 367) && spawnInfo.SpawnTileX > Main.rockLayer && NPC.downedBoss2 ? 0.07f : 0f;
		}
	}
}
  #endregion