
using Revelation.player;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 迷你鲨的恩赐:ModItem
    {
        public override void SetDefaults() 
        {
            Item.width = 22;
            Item.height = 22;
            Item.accessory= true;
            Item.value = 11451;
            Item.expert= true;
            Item.rare = ItemRarityID.Expert;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Generic)+= 0.2f;
            player.GetModPlayer < 恩赐 >().enc= true;
        }
    }
}
