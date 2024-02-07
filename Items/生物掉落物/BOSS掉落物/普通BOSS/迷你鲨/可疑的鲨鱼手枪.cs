using Revelation.NPCs.BOSS.普通BOSS.迷你鲨;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 可疑的鲨鱼手枪:ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 20;
            Item.value = 114514;
            Item.rare = 8;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<迷你鲨NPC>()); 
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {

                SoundEngine.PlaySound(SoundID.Roar, player.position);//播放吼叫音效
                int type = ModContent.NPCType<迷你鲨NPC>();
                    NPC.SpawnOnPlayer(player.whoAmI, type);//生成Boss
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(97, 20);
            recipe.AddIngredient(319, 5);
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
