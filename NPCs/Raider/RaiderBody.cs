using Microsoft.Xna.Framework;
using Revelation.NPCs.BOSS.衰竭辐射;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.Raider
{
    internal class RaiderBody : ModNPC
    {
        public override string Texture => "Revelation/NPCs/BOSS/衰竭辐射/ExampleWormBody";
        public static int Damage => 40;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 30;
            NPC.scale = 2.0f;
            NPC.lifeMax = RaiderHead.Life;
            NPC.defense = 0;
            NPC.knockBackResist = 0.0f;
            NPC.damage = Damage;
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.HitSound = new SoundStyle("Revelation/Sound/BOSS音效/袭击者/袭击者受击2");

            Music = RaiderHead.BackgroundMusic;
        }

        private int head => (int)NPC.ai[0];
        private NPC headNPC => Main.npc[head];

        private int following => (int)NPC.ai[1];
        private NPC followingNPC => Main.npc[following];

        private bool isStage2
        {
            get
            {
                return (int)NPC.ai[3] != 0;
            }
            set
            {
                NPC.ai[3] = value ? 1 : 0;
            }
        }

        public override void AI()
        {
            if (!NPC.HasValidTarget)
            {
                NPC.TargetClosest();
            }

            bool shouldDespawn = false;
            if (head > 0 && following > 0 && followingNPC.active)
            {
                if (headNPC.active && headNPC.life > 0)
                {
                    NPC.realLife = head;
                }
                else
                {
                    shouldDespawn = true;
                }
            }
            else
            {
                shouldDespawn = true;
            }

            if (shouldDespawn)
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
                NPC.checkDead();
                return;
            }


            if (!isStage2 && (int)Main.npc[head].ai[3] != 0)
            {
                NPC.defense += 15;
                NPC.damage += 20;
                isStage2 = true;
                NPC.netUpdate = true;
            }

            // 二阶段身体产生弹幕
            if (isStage2 && (Main.expertMode || Main.masterMode))
            {
                int probability = 100 + (int)Math.Log(Math.Max(NPC.life, 0.05), 1.00840819);
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.HasValidTarget && Main.rand.NextBool(probability))
                {
                    var velocity = Vector2.Zero;
                    var projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center,
                        velocity, ModContent.ProjectileType<RaiderProjectile>(), 20, 0.0f);
                    Main.projectile[projectile].netUpdate = true;
                }
            }

            var delta = followingNPC.Center - NPC.Center;
            var dist = delta.Length();
            float speed;
            if (dist >= (NPC.height + followingNPC.height) / 2)
            {
                speed = followingNPC.velocity.Length();
            }
            else
            {
                speed = 0.01f;
            }
            NPC.velocity = speed * delta.SafeNormalize(Vector2.UnitX);
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}
