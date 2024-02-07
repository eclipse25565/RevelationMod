using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Revelation.Items.生物掉落物.BOSS掉落物.普通BOSS.迷你鲨
{
    public class 迷你鲨弓:ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 22;
            Item.value = 10000;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.useTurn= true;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = 1;
            Item.shootSpeed = 8F;
        }

    }
}
