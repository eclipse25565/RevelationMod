using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Revelation.Tiles.建筑;
using Revelation.Items.材料.神械;

namespace Revelation.Items.套装.辐射
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Head)]
	public class 辐射战铠头 : ModItem
	{
		public override void SetStaticDefaults() {

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;

			// If your head equipment should draw hair while drawn, use one of the following:
			// ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
			// ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
			// ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
			// ArmorIDs.Head.Sets.DrawBackHair[Item.headSlot] = true;
			// ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true; 
		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 20); // How many coins the item is worth
			Item.rare =ItemRarityID.Green; // The rarity of the item
			Item.defense = 16; // The amount of defense the item will give when equipped
		}
		
				public override void UpdateEquip(Player player) {
				player.GetDamage(DamageClass.Generic) += 0.03f; // 单独增益
					player.moveSpeed += 0.06f; // Increase the movement speed of the player
					player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ModContent.ItemType<辐射战铠盔甲>() && legs.type == ModContent.ItemType<辐射战铠裤腿>();//装备ID

		}	public override void UpdateArmorSet(Player player) {
			player.setBonus = "纳米能量充斥着你的身体\n获得额外12%全能伤害 给予5防御力以及50额外生命值上限"; // This is the setbonus tooltip
			player.GetDamage(DamageClass.Generic) += 0.12f; // 套装奖励增益
            player.statDefense += 5;
			player.statLifeMax2 +=60;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<能量晶体>(15);
                  recipe.AddIngredient<合金甲板>(25);
			recipe.AddTile<合金加工站>();
			recipe.Register();
		}
	}
}
