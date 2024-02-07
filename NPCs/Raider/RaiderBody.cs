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

        public static void CommonAI(ModNPC body) {
            if (!body.NPC.HasValidTarget)
            {
                body.NPC.TargetClosest();
            }

            var head = (int)body.NPC.ai[0];
            bool shouldDespawn = false;
            if (head > 0)
            {
                if (Main.npc[head].life > 0)
                {
                    body.NPC.realLife = head;
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
                body.NPC.life = 0;
                body.NPC.HitEffect();
                body.NPC.active = false;
                body.NPC.checkDead();
                return;
            }


            if ((int)body.NPC.ai[3] == 0 && (int)Main.npc[head].ai[3] != 0)
            {
                body.NPC.defense += 15;
                body.NPC.damage += 20;
                body.NPC.ai[3] = 1;
                body.NPC.netUpdate = true;
            }

            if ((int)body.NPC.ai[3] != 0)
            {
                int probability = 100 + (int)Math.Log(Math.Max(body.NPC.life, 0.05), 1.00840819);
                if(Main.netMode != NetmodeID.MultiplayerClient && body.NPC.HasValidTarget && Main.rand.NextBool(probability))
                {
                    var velocity = Vector2.Zero;//(target.Center - body.NPC.Center).SafeNormalize(Vector2.UnitX) * 5.5f;
                    var projectile = Projectile.NewProjectile(body.NPC.GetSource_FromAI(), body.NPC.Center, 
                        velocity, ModContent.ProjectileType<RaiderProjectile>(), 20, 0.0f);
                    Main.projectile[projectile].netUpdate = true;
                }
            }

            var last = Main.npc[(int)body.NPC.ai[1]];
            var delta = last.Center - body.NPC.Center;
            var dist = delta.Length();
            float speed;
            if (dist >= (body.NPC.height + last.height) / 2)
            {
                speed = last.velocity.Length();
            }
            else
            {
                speed = 0.0001f;
            }
            body.NPC.velocity = speed * delta.SafeNormalize(Vector2.UnitX);
            body.NPC.rotation = (float)Math.Atan2((double)body.NPC.velocity.Y, (double)body.NPC.velocity.X) + 1.57f;
        }
        public override void AI()
        {
            CommonAI(this);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}
