using Microsoft.Xna.Framework;
using Terraria;


namespace Revelation.NPCs.BOSS.Raider
{
    internal class RaiderTail : RaiderBody
    {
        protected override int Damage => 24;
        protected override int Defense => 0;

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 32;
            NPC.height = 36;
            NPC.scale = 2.0f;
        }

        public override void AI()
        {
            base.AI();
            if(EnteredPortal)
            {
                HeadNPC.PortalDelta = Vector2.Zero;
                HeadNPC.NPC.netUpdate = true;
            }
        }
    }
}
