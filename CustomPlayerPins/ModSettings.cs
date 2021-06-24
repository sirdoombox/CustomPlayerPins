using Vintagestory.Client.NoObf;

namespace CustonPlayerPins
{
    public static class ModSettings
    {
        public static bool HasBeenInitialised => ClientSettings.Inst.Int.Exists("customplayerpins_playerPinR");
        
        public static int PlayerPinR
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_playerPinR");
            set => ClientSettings.Inst.Int["customplayerpins_playerPinR"] = value;
        }

        public static int PlayerPinG
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_playerPinG");
            set => ClientSettings.Inst.Int["customplayerpins_playerPinG"] = value;
        }

        public static int PlayerPinB
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_playerPinB");
            set => ClientSettings.Inst.Int["customplayerpins_playerPinB"] = value;
        }

        public static int PlayerPinA
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_playerPinA");
            set => ClientSettings.Inst.Int["customplayerpins_playerPinA"] = value;
        }

        public static int PlayerPinScale
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_playerPinScale");
            set => ClientSettings.Inst.Int["customplayerpins_playerPinScale"] = value;
        }

        public static int OthersPinR
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_othersPinR");
            set => ClientSettings.Inst.Int["customplayerpins_othersPinR"] = value;
        }

        public static int OthersPinG
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_othersPinG");
            set => ClientSettings.Inst.Int["customplayerpins_othersPinG"] = value;
        }

        public static int OthersPinB
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_othersPinB");
            set => ClientSettings.Inst.Int["customplayerpins_othersPinB"] = value;
        }

        public static int OthersPinA
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_othersPinA");
            set => ClientSettings.Inst.Int["customplayerpins_othersPinA"] = value;
        }

        public static int OthersPinScale
        {
            get => ClientSettings.Inst.GetIntSetting("customplayerpins_othersPinScale");
            set => ClientSettings.Inst.Int["customplayerpins_othersPinScale"] = value;
        }
    }
}