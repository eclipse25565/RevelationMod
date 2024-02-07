using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revelation.Menu
{

    public class RevelationModMenu : ModMenu
    {
        public class Cinder
        {
            public int Time;
            public int Lifetime;
            public int IdentityIndex;
            public float Scale;
            public float Depth;
            public Color DrawColor;
            public Vector2 Velocity;
            public Vector2 Center;

            public Cinder(int lifetime, int identity, float depth, Color color, Vector2 startingPosition, Vector2 startingVelocity)
            {
                Lifetime = lifetime;
                IdentityIndex = identity;
                Depth = depth;
                DrawColor = color;
                Center = startingPosition;
                Velocity = startingVelocity;
            }
        }

        public static List<Cinder> Cinders
        {
            get;
            internal set;
        } = new();

		public override string DisplayName => "启示录菜单";

		public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("Revelation/Menu/标题");
        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("Revelation/Menu/空洞像素");
        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("Revelation/Menu/空洞像素");

	    public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullSurfaceBackground>();
        				public override void OnSelected() {
			Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Zombie93); // 选择了这个ModMenu之后播放一个打雷音效

		}
        
					public override int Music => MusicLoader.GetMusicSlot(Mod, "Sound/Music/ZCD/ZCDYY");

	    public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
	    {
		    Texture2D texture = ModContent.Request<Texture2D>("Revelation/Menu/背景").Value;
            Vector2 drawOffset = Vector2.Zero;
            float xScale = (float)Main.screenWidth / texture.Width;
            float yScale = (float)Main.screenHeight / texture.Height;
            float scale = xScale;
            if (xScale != yScale)
            {
                if (yScale > xScale)
                {
                    scale = yScale;
                    drawOffset.X -= (texture.Width * scale - Main.screenWidth) * 0.5f;
                }
                else
                    drawOffset.Y -= (texture.Height * scale - Main.screenHeight) * 0.5f;
            }
		    spriteBatch.Draw(texture, drawOffset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            static Color selectCinderColor()
            {
                return Color.Lerp(Color.Red, Color.Yellow, Main.rand.NextFloat(0.9f));
            }
            //生成粒子
            for (int i = 0; i < 5; i++)
            {
                if (Main.rand.NextBool(4))
                {
                    int lifetime = Main.rand.Next(200, 300);
                    float depth = Main.rand.NextFloat(1.8f, 5f);
                    Vector2 startingPosition = new Vector2(Main.screenWidth * Main.rand.NextFloat(-0.1f, 1.1f), Main.screenHeight * 1.05f);
                    Vector2 startingVelocity = -Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-0.9f, 0.9f)) * 4f;
                    Color cinderColor = selectCinderColor();
                    Cinders.Add(new Cinder(lifetime, Cinders.Count, depth, cinderColor, startingPosition, startingVelocity));
                }
            }
            //更新粒子
            for (int i = 0; i < Cinders.Count; i++)
            {
                Cinders[i].Scale = Utils.GetLerpValue(Cinders[i].Lifetime, Cinders[i].Lifetime / 3, Cinders[i].Time, true);
                Cinders[i].Scale *= MathHelper.Lerp(0.6f, 0.9f, Cinders[i].IdentityIndex % 6f / 6f);
                if (Cinders[i].IdentityIndex % 13 == 12)
                    Cinders[i].Scale *= 2f;

                float flySpeed = MathHelper.Lerp(3.2f, 14f, Cinders[i].IdentityIndex % 21f / 21f);
                Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-0.44f, 0.44f, (float)Math.Sin(Cinders[i].Time / 16f + Cinders[i].IdentityIndex) * 0.5f + 0.5f));
                idealVelocity = (idealVelocity + Vector2.UnitX).SafeNormalize(Vector2.UnitY) * flySpeed;

                float movementInterpolant = MathHelper.Lerp(0.01f, 0.08f, Utils.GetLerpValue(45f, 145f, Cinders[i].Time, true));
                Cinders[i].Velocity = Vector2.Lerp(Cinders[i].Velocity, idealVelocity, movementInterpolant);

                Cinders[i].Time++;
                Cinders[i].Center += Cinders[i].Velocity;
            }
            //删除粒子
			Cinders.RemoveAll(c => c.Time >= c.Lifetime);
            //绘制粒子
            Texture2D cinderTexture = ModContent.Request<Texture2D>("Revelation/Menu/粒子").Value;
            for (int i = 0; i < Cinders.Count; i++)
            {
                Vector2 drawPosition = Cinders[i].Center;
                spriteBatch.Draw(cinderTexture, drawPosition, null, Cinders[i].DrawColor, 0f, cinderTexture.Size() * 0.5f, Cinders[i].Scale, 0, 0f);
            }
            //时间and标题
			drawColor = Color.White;
            Main.time = 27000;
            Main.dayTime = true;
			//绘制标题
            Vector2 drawPos = new Vector2(Main.screenWidth / 2f, 100f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            spriteBatch.Draw(Logo.Value, drawPos, null, drawColor, logoRotation, Logo.Value.Size() * 0.5f, logoScale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            return false;
		}
	}
}