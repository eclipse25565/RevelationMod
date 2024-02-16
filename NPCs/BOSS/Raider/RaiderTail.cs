using Terraria;


namespace Revelation.NPCs.BOSS.Raider
{
    internal class RaiderTail : RaiderBody
    {
        protected override int Damage => 40;
        protected override int Defense => 3;

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
                PortalDelta = 0.0f;
                HeadNPC.netUpdate = true;
            }
        }
    }
}
