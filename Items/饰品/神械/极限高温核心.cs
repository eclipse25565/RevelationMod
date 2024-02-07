using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Revelation.Buff.负面减益;

namespace Revelation.Items.饰品.神械
{
    public class 极限高温核心 : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 59; // Influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.value = 35000; // The cost of the item in copper coins. (1 = 1 copper, 100 = 1 silver, 1000 = 1 gold, 10000 = 1 platinum)
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.rare = -12;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.placeStyle = 0;
			Item.accessory = true;
			Item.defense = 5;
			}
			public override void UpdateAccessory(Player player, bool hideVisual) {
	
				if (player.statDefense <= 55)
				{
    player.statLifeMax2 += 100;
	player.statDefense +=20;
				}

				 if (player.statDefense >= 70)
				{
    player.statLifeMax2 += 200;
				}

				 if (player.statLifeMax2 >= 699)
			    {
			player.AddBuff(ModContent.BuffType<极限系统过载>(),20);
			player.AddBuff(39,20);
			    }

	player.GetDamage(DamageClass.Generic) += 0.22f; // 单独增益
	player.GetCritChance(DamageClass.Generic) += 10; // 单独增益
	player.noKnockback = true;//防击退
	player.lifeRegen +=5;
	player.statDefense += (int)MathHelper.Lerp(0,40, Terraria.Utils.GetLerpValue(player.statLifeMax,50,player.statLife));
	base.UpdateEquip(player);//生命值越低防御力越高最高50防御力
			}
			public override void AddRecipes() {
			CreateRecipe()//铁砧Anvil 工作台WorkBenches 熔炉Furnaces 工匠作坊TinkerersWorkbench
			.Register();

			}
	}
}


