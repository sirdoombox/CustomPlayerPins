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
            // TODO: Clear old textures each time the map opens and use custom colours.
            var capi = capiRef(__instance);
            if (ownTextureRef(__instance) == null)
            {
                var surface = new ImageSurface(Format.Argb32, 32, 32);
                var cr = new Context(surface);
                cr.SetSourceRGBA(0.0, 0.0, 0.0, 0.0);
                cr.Paint();
                capi.Gui.Icons.DrawMapPlayer(cr, 0, 0, 32f, 32f, new double[4]
                {
                    1.0,
                    1.0,
                    0.0,
                    1.0
                }, new double[4]{ 1.0, 0, 1.0, 1.0 });
                ownTextureRef(__instance) = new LoadedTexture(capi, capi.Gui.LoadCairoTexture(surface, false), 16, 16);
                cr.Dispose();
                surface.Dispose();
            }
            if (otherTextureRef(__instance) == null)
            {
                var surface = new ImageSurface(Format.Argb32, 32, 32);
                var cr = new Context(surface);
                cr.SetSourceRGBA(0.0, 0.0, 0.0, 0.0);
                cr.Paint();
                capi.Gui.Icons.DrawMapPlayer(cr, 0, 0, 32f, 32f, new double[4]
                {
                    1.0,
                    0,
                    1.0,
                    1.0
                }, new double[4]{ 1.0, 1.0, 0, 1.0 });
                otherTextureRef(__instance) = new LoadedTexture(capi, capi.Gui.LoadCairoTexture(surface, false), 16, 16);
                cr.Dispose();
                surface.Dispose();
            }
        }
    }
}