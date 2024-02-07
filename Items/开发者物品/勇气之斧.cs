using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Revelation.Properties.开发者弹幕;
using Revelation.Tiles.建筑;

namespace Revelation.Items.开发者物品
{
    public class 勇气之斧 : ModItem
	{
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
            Item.axe = 40; //x*50斧力
            Item.hammer = 200; //锤力
			Item.damage = 400;//物品的基础伤害
			Item.crit = 100;//物品的暴击率
			Item.DamageType = DamageClass.Melee;//物品的伤害类型
			Item.width = 15;//物品以掉落物形式存在的碰撞箱宽度
			Item.height = 46;//物品以掉落物形式存在的碰撞箱高度
			Item.useTime = 15;//物品一次使用所经历的时间
			Item.shoot = ModContent.ProjectileType<粗暴碎击>();//物品发射的弹幕
			Item.shootSpeed = 16f;//物品发射的弹幕速度
			Item.useAnimation = 15;//物品播放使用动画所经历的时间
			Item.useStyle = ItemUseStyleID.Swing;//使用动作
			Item.knockBack = 6;//物品击退
			Item.value = Item.buyPrice(11,45,14,0);//价值
			Item.rare = -12;//稀有度
			Item.UseSound = SoundID.Item15;//使用时的声音
			Item.autoReuse = true;//自动连发
			Item.noUseGraphic = false;//隐藏物品使用动画
			Item.noMelee = false;//近战判定
			Item.useAmmo = AmmoID.None;//消耗指定弹药
			Item.mana = 0;//为大于零的数时每次使用会消耗魔力值
			Item.scale = 1.5f;//物品作为近战武器时的判定大小
			Item.channel = true;
			Item.useTurn = true;
		}
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Torch, 0, 0, 0, default, 2);
            base.MeleeEffects(player, hitbox);
        }
        
		//public override bool CanUseItem(Player player)
        //{
            //if(!Main.dayTime)
            //{
                //return false;
            //}
	    //}
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile<原科研发区>();
			recipe.Register();
		}
	}
}