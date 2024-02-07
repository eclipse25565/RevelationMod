using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.NPCs.BOSS.Raider
{
    internal class BugEgg : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[ModContent.NPCType<BugEgg>()] = 7;
        }

        private int life = 900;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 26;
            NPC.height = 32;
            NPC.scale = 2.0f;
            NPC.lifeMax = 800;
            NPC.damage = 40;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.npcSlots = 0.8f;
            NPC.knockBackResist = 0.0f;
        }

        private enum State
        {
            Unknown,
            Falling,
            OnGround
        }

        private State state = State.Unknown;

        public override void AI()
        {
            NPC.frameCounter += 0.1;

            switch (state)
            {
                case State.Unknown:
                    state = State.Falling;
                    break;
                case State.Falling:
                    if (Math.Abs(NPC.velocity.Y) < 0.001f)
                    {
                        state = State.OnGround;
                        NPC.position.Y += 52;
                        NPC.height = 12;
                        break;
                    }
                    NPC.frameCounter %= 4;
                    NPC.frame.Y = 48 * (int)NPC.frameCounter;
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) - 1.57f;
                    break;
                case State.OnGround:
                    NPC.frameCounter %= 3;
                    NPC.frame.Y = 48 * ((int)NPC.frameCounter + 4);
                    var acc = Math.Clamp(-NPC.velocity.X, -0.7f, 0.7f);
                    NPC.velocity.X += acc;
                    NPC.rotation = 0.0f;
                    break;
            }

            if(--life == 1)
            {
                var pos = NPC.Center;
                SoundEngine.PlaySound(SoundID.NPCDeath1, pos);
                NPC.HitEffect();
                if(Main.netMode != NetmodeID.MultiplayerClient)
                {
                    var npc = NPC.NewNPC(NPC.GetSource_FromAI(), (int)pos.X, (int)pos.Y, ModContent.NPCType<RaidingDestroyerHead>());
                    Main.npc[npc].netUpdate = true;
                }
                NPC.life = 0;
                NPC.checkDead();
                NPC.active = false;
                NPC.netUpdate = true;
            }
        }
    }
}
