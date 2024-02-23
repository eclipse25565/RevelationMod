using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Revelation.Items.材料.哀霜;
using Revelation.Tiles.建筑;
using Revelation.Projectiles.武器弹幕.哀霜;

namespace Revelation.Items.武器.远程.未来武器.哀霜
{
	public class 阿尔特之霜 : ModItem
	{
		public override void SetStaticDefaults() {

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			// 普遍属性
			Item.width = 80; // 物品的碰撞箱宽度(像素)
			Item.height = 27; // 物品的碰撞箱高度(像素)
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Cyan; // 物品稀有度

			// 使用属性
			Item.useTime = 13; // 物品实际使用一次所需时间 (帧) (60帧=1秒)
			Item.useAnimation = 13; // 物品动画播放一次所需时间 (帧) (60帧=1秒)
			Item.useStyle = ItemUseStyleID.Shoot; // 物品的使用类型
			Item.autoReuse = true; // 这个物品默认能不能自动挥舞
			
			// 物品被使用时播放的声音
			Item.UseSound = new SoundStyle($"{nameof(Revelation)}/Sound/物品音效/武器/哀霜/阿尔特之霜枪声") {
				Volume = 0.9f,
				PitchVariance = 0.2f,
				MaxInstances = 3,
			};

			// 武器属性
			Item.DamageType = DamageClass.Ranged; // 伤害类型设置为远程
			Item.damage = 50; // 物品基础伤害，注意: 射出的射弹伤害=武器伤害+弹药伤害
			Item.knockBack = 1f; // 物品基础击退，注意: 射出的射弹击退=武器击退+弹药击退
			Item.noMelee = true; // 让这个物品的使用动画不会造成伤害 (指拿枪杆子打人)

			// 枪属性
			Item.shoot = ModContent.ProjectileType<阿尔特之霜弹幕>(); // 出于某种原因，原版枪的 Item.shoot 都是这么设置的，实际射弹基于弹药以及 Shoot() 相关代码
			Item.shootSpeed = 7f; // 射弹的速度 (像素/帧) (比如这里是每帧16像素，也就是960像素每秒，即60物块每秒)
			Item.useAmmo = AmmoID.Bullet; // 物品使用的弹药类型ID，用 AmmoID.XX 来选一个原版弹药类型，这里是所有子弹的意思

		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(source,new Vector2(player.Center.X-10f,player.Center.Y-10f),velocity, ModContent.ProjectileType<阿尔特之霜弹幕>(), damage,knockback);
			return false; 
        }
        // 通过这个重写函数修改武器持握在玩家手上时的位置 (让他握着枪柄而不是反重力悬空)
        public override Vector2? HoldoutOffset() {
    // X坐标往里移动10像素，Y坐标向上移动5像素
    return new Vector2(-40, -8);
}


		
		// 这里写的是合成配方，合成配方在 Content/ExampleRecipes.cs 有更详尽的介绍
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<沉哀锭>(20)
				.AddTile<合金加工站>()
				.Register();
				
		}


    }
}