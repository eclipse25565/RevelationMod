using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Revelation.NPCs
{
    /*
     * 使用了NPC.localAI[0]判断是否生成了尾巴
     */
    internal abstract class WormLikeHead : ModNPC
    {
        protected abstract int BodyType { get; }
        protected abstract int TailType { get; }

        protected bool SpawnedTail {
            get => (int)NPC.localAI[0] == 1;
            set
            {
                NPC.localAI[0] = value ? 1 : 0;
            }
        }

        // 包括尾巴但不包括头的长度
        protected abstract int SegmentLength { get; }

        public override void PostAI()
        {
            HeadAI();
        }

        private void HeadAI()
        {
            if(!SpawnedTail)
            {
                SpawnedTail = true;
                AI_SpawnTail();
            }

            if(!NPC.HasValidTarget)
            {
                NPC.TargetClosest();
            }

            UpdateRotation();
        }

        protected void UpdateRotation()
        {
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.570795f;
        }

        private void AI_SpawnTail()
        {
            var len = SegmentLength;
            var pos = NPC.Center;
            int last = NPC.whoAmI;
            for(int i = 0; i < len - 1; ++i) {
                var segment = NPC.NewNPC(NPC.GetSource_FromAI(), (int)pos.X, (int)pos.Y, BodyType, 0, NPC.whoAmI, last);
                Main.npc[segment].realLife = NPC.whoAmI;
                Main.npc[segment].netUpdate = true;
                last = segment;
            }
            var tail = NPC.NewNPC(NPC.GetSource_FromAI(), (int)pos.X, (int)pos.Y, TailType, 0, NPC.whoAmI, last);
            Main.npc[tail].realLife = NPC.whoAmI;
            Main.npc[tail].netUpdate = true;
        }
    }

    /*
     * 使用了NPC.ai[0]储存头的id，使用了NPC.ai[1]储存了上一段的id
     */
    internal abstract class WormLikeBody : ModNPC
    {
        protected int Head => (int)NPC.ai[0];
        protected NPC HeadNPC => Main.npc[Head];
        protected int Following => (int)NPC.ai[1];
        protected NPC FollowingNPC => Main.npc[Following];

        public override void AI()
        {
            if (!NPC.HasValidTarget)
            {
                NPC.TargetClosest();
            }

            bool shouldDespawn = false;
            if(Head <= 0 || Following <= 0 || !HeadNPC.active || !FollowingNPC.active || HeadNPC.life <= 0)
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

            var delta = FollowingNPC.Center - NPC.Center;
            float speed;
            if(delta.Length() >= (NPC.height + FollowingNPC.height) * 0.5f)
            {
                speed = FollowingNPC.velocity.Length();
            }
            else
            {
                speed = 0.01f;
            }
            NPC.velocity = delta.SafeNormalize(Vector2.UnitX) * speed;
            UpdateRotation();
        }

        protected void UpdateRotation()
        {
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.570795f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        protected float crowdProtectionFactor = 0.0f;
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if(
                this.crowdProtectionFactor > 0.0f && 
                Vector2.Distance(FollowingNPC.Center, NPC.Center) < NPC.height / this.crowdProtectionFactor)
            {
                modifiers.SetMaxDamage(1);
            }
        }
    }

    /*
     * 见WormLikeBody
     */
    internal abstract class WormLikeTail : WormLikeBody
    {

    }
}
