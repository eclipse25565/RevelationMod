using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Revelation.Properties;
using Revelation.Properties.武器弹幕.迷你鲨;

namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 迷你鲨法杖:ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.DamageType = DamageClass.Magic;
            Item.crit = 22;
            Item.value = 10000;
            Item.mana = 4;
            Item.useTime = 7;
            Item.knockBack = 5f;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<迷你鲨弹幕>();
            Item.shootSpeed = 8F;
        }

    }
}
