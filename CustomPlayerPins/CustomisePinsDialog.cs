using Vintagestory.API.Client;

namespace CustonPlayerPins
{
    // TODO Implement reusable GUI which swaps out Self/Others
    public class CustomisePinsDialog : AdvancedOptionsDialog
    {
        public override string ToggleKeyCombinationCode => "customisePlayerPins";
        protected override string DialogKey => "customisePinsDialog";
        protected override string DialogTitle { get; }
    
        private readonly bool isPlayer;
        
        public CustomisePinsDialog(ICoreClientAPI capi, bool isPlayer) : base(capi)
        {
            this.isPlayer = isPlayer;
            var displayStr = isPlayer ? "Player" : "Others";
            DialogTitle = $"Customise {displayStr} Pins";
            RegisterOption(new ConfigOption
            {
                SliderKey = "sliderR",
                Text = $"{displayStr} R",
                Tooltip = $"The R component of the {displayStr.ToLower()}'s RGBA pin colour.",
                SlideAction = OnRChanged
            });
            RegisterOption(new ConfigOption
            {
                SliderKey = "sliderG",
                Text = $"{displayStr} G",
                Tooltip = $"The G component of the {displayStr.ToLower()}'s RGBA pin colour.",
                SlideAction = OnGChanged
            });
            RegisterOption(new ConfigOption
            {
                SliderKey = "sliderB",
                Text = $"{displayStr} B",
                Tooltip = $"The B component of the {displayStr.ToLower()}'s RGBA pin colour.",
                SlideAction = OnBChanged
            });
            RegisterOption(new ConfigOption
            {
                SliderKey = "sliderA",
                Text = $"{displayStr} A",
                Tooltip = $"The A component of the {displayStr.ToLower()}'s RGBA pin colour.",
                SlideAction = OnAChanged
            });
        }

        protected override void RefreshValues()
        {
            if (!IsOpened()) return;
            SetRgbSliderValue("sliderR", isPlayer ? ModSettings.PlayerPinR : ModSettings.OthersPinR);
            SetRgbSliderValue("sliderG", isPlayer ? ModSettings.PlayerPinG : ModSettings.OthersPinG);
            SetRgbSliderValue("sliderB", isPlayer ? ModSettings.PlayerPinB : ModSettings.OthersPinB);
            SetRgbSliderValue("sliderA", isPlayer ? ModSettings.PlayerPinA : ModSettings.OthersPinA);
        }

        private void SetRgbSliderValue(string name, int value)
        {
            SingleComposer.GetSlider(name).SetValues(value, 0, 255, 1);
        }

        private bool OnRChanged(int r)
        {
            if (isPlayer) ModSettings.PlayerPinR = r;
            else ModSettings.OthersPinR = r;
            return true;
        }

        private bool OnGChanged(int g)
        {
            if (isPlayer) ModSettings.PlayerPinG = g;
            else ModSettings.OthersPinG = g;
            return true;
        }

        private bool OnBChanged(int b)
        {
            if (isPlayer) ModSettings.PlayerPinB = b;
            else ModSettings.OthersPinB = b;
            return true;
        }

        private bool OnAChanged(int a)
        {
            if (isPlayer) ModSettings.PlayerPinA = a;
            else ModSettings.OthersPinA = a;
            return true;
        }
    }
}