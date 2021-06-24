using Cairo;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.GameContent;

namespace CustomPlayerPins
{
    [HarmonyPatch(typeof(PlayerMapLayer), "OnMapOpenedClient")]
    public class MapPinPatch
    {
        private static readonly AccessTools.FieldRef<PlayerMapLayer, ICoreClientAPI> CapiRef =
            AccessTools.FieldRefAccess<PlayerMapLayer, ICoreClientAPI>("capi");

        private static readonly AccessTools.FieldRef<PlayerMapLayer, LoadedTexture> OwnTextureRef =
            AccessTools.FieldRefAccess<PlayerMapLayer, LoadedTexture>("ownTexture");

        private static readonly AccessTools.FieldRef<PlayerMapLayer, LoadedTexture> OtherTextureRef =
            AccessTools.FieldRefAccess<PlayerMapLayer, LoadedTexture>("otherTexture");


        private static void Prefix(PlayerMapLayer __instance)
        {
            var capi = CapiRef(__instance);
            var selfRgba = new[]
            {
                ModSettings.PlayerPinR / 255d,
                ModSettings.PlayerPinG / 225d,
                ModSettings.PlayerPinB / 255d,
                ModSettings.PlayerPinA / 255d
            };
            var selfOutline = new[]
            {
                0d, 0d, 0d, ModSettings.PlayerPinA / 255d
            };

            var surfaceSelf = new ImageSurface(Format.Argb32, 32 + ModSettings.PlayerPinScale,
                32 + ModSettings.PlayerPinScale);
            var crSelf = new Context(surfaceSelf);
            crSelf.SetSourceRGBA(0.0, 0.0, 0.0, 0.0);
            crSelf.Paint();
            capi.Gui.Icons.DrawMapPlayer(crSelf, 0, 0, 32f + ModSettings.PlayerPinScale,
                32f + ModSettings.PlayerPinScale, selfOutline, selfRgba);
            OwnTextureRef(__instance) =
                new LoadedTexture(capi, capi.Gui.LoadCairoTexture(surfaceSelf, false),
                    16 + ModSettings.PlayerPinScale,
                    16 + ModSettings.PlayerPinScale);
            crSelf.Dispose();
            surfaceSelf.Dispose();

            var otherRgba = new[]
            {
                ModSettings.OthersPinR / 255d,
                ModSettings.OthersPinG / 255d,
                ModSettings.OthersPinB / 255d,
                ModSettings.OthersPinA / 255d
            };
            var otherOutline = new[]
            {
                0d, 0d, 0d, ModSettings.OthersPinA / 255d
            };
            var surfaceOther = new ImageSurface(Format.Argb32, 32 + ModSettings.OthersPinScale,
                32 + ModSettings.OthersPinScale);
            var crOther = new Context(surfaceOther);
            crOther.SetSourceRGBA(0.0, 0.0, 0.0, 0.0);
            crOther.Paint();
            capi.Gui.Icons.DrawMapPlayer(crOther, 0, 0, 32f + ModSettings.OthersPinScale,
                32f + ModSettings.OthersPinScale, otherOutline, otherRgba);
            OtherTextureRef(__instance) =
                new LoadedTexture(capi, capi.Gui.LoadCairoTexture(surfaceOther, false),
                    16 + ModSettings.OthersPinScale,
                    16 + ModSettings.OthersPinScale);
            crOther.Dispose();
            surfaceOther.Dispose();
        }
    }
}