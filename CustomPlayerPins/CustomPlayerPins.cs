using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace CustonPlayerPins
{
    public class CustomPlayerPins : ModSystem
    {
        private ICoreClientAPI capi;
        
        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return forSide == EnumAppSide.Client;
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            // TODO: Implement options screen and saving of colour data locally.
            capi = api;
            base.StartClientSide(api);

            if (!ModSettings.HasBeenInitialised)
                SetDefaults();
            
            api.Input.RegisterHotKey("customisePlayerPins", "Customise Player Pins", GlKeys.M,
                HotkeyType.GUIOrOtherControls, shiftPressed: true);
            api.Input.SetHotKeyHandler("customisePlayerPins", ShowCustomisationDialog);

            var harmony = new Harmony("customplayerpins.patches");
            harmony.PatchAll();
        }

        private void SetDefaults()
        {
            ModSettings.PlayerPinR = 255;
            ModSettings.PlayerPinG = 255;
            ModSettings.PlayerPinB = 255;
            ModSettings.PlayerPinA = 255;
            ModSettings.OthersPinR = 76;
            ModSettings.OthersPinG = 76;
            ModSettings.OthersPinB = 76;
            ModSettings.OthersPinA = 255;
        }

        private bool ShowCustomisationDialog(KeyCombination comb)
        {
            new CustomisePinsDialog(capi, true).TryOpen();
            return true;
        }
    }
}