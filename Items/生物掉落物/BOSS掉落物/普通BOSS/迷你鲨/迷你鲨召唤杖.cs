using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

using Revelation.Properties;
using Revelation.Buff.召唤buff;
using Revelation.Properties.武器弹幕.迷你鲨;

namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 迷你鲨召唤杖 : ModItem 
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
                                                                      // 这允许玩家使用控制器时瞄准整个屏幕任意位置。
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance.
                                                             // 默认值为 1，但支持其他值。请参阅文档获取更多指导信息。
        }
        public override void SetDefaults() 
        {
            Item.width = 60;
            Item.height=29;
            Item.UseSound = SoundID.Item2;
            Item.damage = 7;
            Item.crit = 0;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 3f;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.value = Item.buyPrice(0,10,0,0);
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<迷你鲨召唤物>();
            Item.shootSpeed = 10f;
            Item.mana = 10;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(ModContent.BuffType<迷你鲨buff>(), 114514);
            var projectile = Projectile.NewProjectileDirect(source,Main.MouseWorld,velocity,type,damage,knockback,player.whoAmI);
            projectile.originalDamage = Item.damage;
            return false;
        }
    }
}
