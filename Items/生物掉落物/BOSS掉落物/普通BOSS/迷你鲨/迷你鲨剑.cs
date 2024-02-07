using Revelation.Properties;
using Revelation.Properties.武器弹幕.迷你鲨;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 迷你鲨剑:ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 22;
            Item.value = 10000;
            Item.knockBack = 5f;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shootSpeed= 12F;
            Item.shoot = ModContent.ProjectileType<迷你鲨弹幕>();
        }
    }
}
