using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace CustonPlayerPins
{
    public class CustomPlayerPins : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return forSide == EnumAppSide.Client;
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            // TODO: Implement options screen and saving of colour data locally.

            base.StartClientSide(api);

            api.Input.RegisterHotKey("customisePlayerPins", "Customise Player Pins", GlKeys.M,
                HotkeyType.GUIOrOtherControls, shiftPressed: true);
            api.Input.SetHotKeyHandler("customisePlayerPins", ShowCustomisationDialog);

            var harmony = new Harmony("customplayerpins.patches");
            harmony.PatchAll();
        }

        private bool ShowCustomisationDialog(KeyCombination t1)
        {
            throw new System.NotImplementedException();
        }
    }
}