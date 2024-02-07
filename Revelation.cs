using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ModLoader;

namespace Revelation
{
    public class Revelation : Mod
    {
        public static object Sound { get; internal set; }

        internal class Buff
        {
        }

        public override void Load()
        {
            Ref<Effect> filterRef =
                new Ref<Effect>(this.Assets.Request<Effect>(
                    "Effects/Blindness", AssetRequestMode.ImmediateLoad).Value);
            Filters.Scene["RevelationBlindness"] = new Filter(new ScreenShaderData(filterRef, "Blindness"), EffectPriority.High);
        }
    }
}