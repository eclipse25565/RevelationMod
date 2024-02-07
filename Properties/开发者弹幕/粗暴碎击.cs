using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Revelation.Properties.开发者弹幕
{
    public class 粗暴碎击 : ModProjectile 
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 2000;
        }
        public override void SetDefaults()
        {
        Projectile.width = 32; // 弹幕的碰撞箱宽度
        Projectile.height = 48; // 弹幕的碰撞箱高度
        Projectile.scale = 2f; // 弹幕缩放倍率
        Projectile.ignoreWater = true; // 弹幕是否忽视水
        Projectile.tileCollide = true; // 弹幕撞到物块是否消失
        Projectile.penetrate = 999; // 弹幕的穿透数，
        Projectile.timeLeft = 2000; // 弹幕的存活时间
        Projectile.alpha = 0; // 弹幕的透明度
        Projectile.friendly = true; // 弹幕是否攻击敌方
        Projectile.hostile = false; // 弹幕是否攻击友方和城镇NPC
        Projectile.DamageType = DamageClass.Melee; // 弹幕的伤害类型
        Projectile.aiStyle = 27; // 弹幕使用原版哪种弹幕AI类型
        AIType =  684;// 弹幕模仿原版哪种弹幕的行为
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 2;
        //Projectile.aiStyle = -1; // 不用原版弹幕
        //Projectile.extraUpdates = 0; // 弹幕每帧的额外更新次
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

    }
} 