using Cairo;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.GameContent;

namespace CustonPlayerPins
{
    [HarmonyPatch(typeof(PlayerMapLayer), "OnMapOpenedClient")]
    public class MapPinPatch
    {
        static AccessTools.FieldRef<PlayerMapLayer, ICoreClientAPI> capiRef =
            AccessTools.FieldRefAccess<PlayerMapLayer, ICoreClientAPI>("capi");

        static AccessTools.FieldRef<PlayerMapLayer, LoadedTexture> ownTextureRef =
            AccessTools.FieldRefAccess<PlayerMapLayer, LoadedTexture>("ownTexture");


        static AccessTools.FieldRef<PlayerMapLayer, LoadedTexture> otherTextureRef =
            AccessTools.FieldRefAccess<PlayerMapLayer, LoadedTexture>("otherTexture");

        static void Prefix(PlayerMapLayer __instance)
        {
            var capi = capiRef(__instance);

            var selfRgba = new[]
            {
                ModSettings.PlayerPinR / 255d,
                ModSettings.PlayerPinG / 225d,
                ModSettings.PlayerPinB / 255d,
                ModSettings.PlayerPinA / 255d
            };
            var surfaceSelf = new ImageSurface(Format.Argb32, 32, 32);
            var crSelf = new Context(surfaceSelf);
            crSelf.SetSourceRGBA(0.0, 0.0, 0.0, 0.0);
            crSelf.Paint();
            capi.Gui.Icons.DrawMapPlayer(crSelf, 0, 0, 32f, 32f, selfRgba, selfRgba);
            ownTextureRef(__instance) = new LoadedTexture(capi, capi.Gui.LoadCairoTexture(surfaceSelf, false), 16, 16);
            crSelf.Dispose();
            surfaceSelf.Dispose();

            var otherRgba = new[]
            {
                ModSettings.OthersPinR / 255d,
                ModSettings.OthersPinG / 255d,
                ModSettings.OthersPinB / 255d,
                ModSettings.OthersPinA / 255d
            };
            var surfaceOther = new ImageSurface(Format.Argb32, 32, 32);
            var crOther = new Context(surfaceOther);
            crOther.SetSourceRGBA(0.0, 0.0, 0.0, 0.0);
            crOther.Paint();
            capi.Gui.Icons.DrawMapPlayer(crOther, 0, 0, 32f, 32f, otherRgba, otherRgba);
            otherTextureRef(__instance) = new LoadedTexture(capi, capi.Gui.LoadCairoTexture(surfaceOther, false), 16, 16);
            crOther.Dispose();
            surfaceOther.Dispose();
        }
    }
}