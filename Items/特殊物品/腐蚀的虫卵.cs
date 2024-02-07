using Revelation.NPCs.BOSS;
using Revelation.NPCs.BOSS.Raider;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.特殊物品
{
    public class 腐蚀的虫卵:ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 50;
            Item.value = 114514;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<RaiderHead>());// && player.ZoneDirtLayerHeight;
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {

                var roar = new SoundStyle ("Revelation/Sound/BOSS音效/袭击者/袭击者叫声1");
                roar.Volume = 0.9f;
                SoundEngine . PlaySound(roar,player.position);
                int type = ModContent.NPCType<RaiderHead>();
                    NPC.SpawnOnPlayer(player.whoAmI, type);//生成Boss
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.Register();
        }
    }
}