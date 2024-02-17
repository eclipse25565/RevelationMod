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
            Ref<Effect> filterRef1 =
                new Ref<Effect>(this.Assets.Request<Effect>(
                    "Effects/Blindness", AssetRequestMode.ImmediateLoad).Value);
            Filters.Scene["RevelationBlindness"] = new Filter(new ScreenShaderData(filterRef1, "Blindness"), EffectPriority.High);

            Ref<Effect> filterRef2 =
                new Ref<Effect>(this.Assets.Request<Effect>(
                    "Effects/RaiderBlindness", AssetRequestMode.ImmediateLoad).Value);
            Filters.Scene["RaiderBlindness"] = new Filter(new ScreenShaderData(filterRef2, "Blindness"), EffectPriority.High);
        }
    }
}