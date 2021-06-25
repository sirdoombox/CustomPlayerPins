using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace CustomPlayerPins
{
    public class CustomPlayerPinsMod : ModSystem
    {
        public static CustomPlayerPinsMod Instance { get; private set; }
        
        private ICoreClientAPI _capi;
        public ConfigDialog ConfigDialog;
        public GuiDialog CurrentDialog;

        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return forSide == EnumAppSide.Client;
        }

        public override void StartPre(ICoreAPI _)
        {
            Instance = this;
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            // TODO: Implement options screen and saving of colour data locally.
            _capi = api;
            base.StartClientSide(api);

            if (!ModSettings.HasBeenInitialised)
                SetDefaults();

            api.Input.RegisterHotKey("customisePlayerPins", "Customise Player Pins", GlKeys.M,
                HotkeyType.GUIOrOtherControls, shiftPressed: true);
            api.Input.SetHotKeyHandler("customisePlayerPins", ShowCustomisationDialog);

            var harmony = new Harmony("customplayerpins.patches");
            harmony.PatchAll(Assembly.GetAssembly(typeof(CustomPlayerPinsMod)));
        }

        private void SetDefaults()
        {
            ModSettings.PlayerPinR = 255;
            ModSettings.PlayerPinG = 255;
            ModSettings.PlayerPinB = 255;
            ModSettings.PlayerPinA = 255;
            ModSettings.PlayerPinScale = 0;
            ModSettings.OthersPinR = 76;
            ModSettings.OthersPinG = 76;
            ModSettings.OthersPinB = 76;
            ModSettings.OthersPinA = 255;
            ModSettings.OthersPinScale = 0;
        }

        private bool ShowCustomisationDialog(KeyCombination comb)
        {
            if (ConfigDialog is null)
                ConfigDialog = new ConfigDialog(_capi);

            if (CurrentDialog != null && CurrentDialog.IsOpened())
            {
                CurrentDialog.TryClose();
                return true;
            }

            ConfigDialog.TryOpen();
            return true;
        }

        public override double ExecuteOrder() => 10000d;
    }
}