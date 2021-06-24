using System.Linq;
using Cairo;
using Vintagestory.API.Client;
using Vintagestory.GameContent;

namespace CustomPlayerPins
{
    // TODO Implement reusable GUI which swaps out Self/Others
    public class CustomisePinsDialog : GuiDialog
    {
        public override string ToggleKeyCombinationCode => null;

        private readonly bool _isPlayer;
        private bool _isSetup;
        private readonly string _displayStr;

        public CustomisePinsDialog(ICoreClientAPI capi, bool isPlayer) : base(capi)
        {
            _isPlayer = isPlayer;
            _displayStr = isPlayer ? "Player" : "Others";
        }

        private void RefreshValues()
        {
            if (!IsOpened()) return;
            SetRgbSliderValue("sliderR", _isPlayer ? ModSettings.PlayerPinR : ModSettings.OthersPinR);
            SetRgbSliderValue("sliderG", _isPlayer ? ModSettings.PlayerPinG : ModSettings.OthersPinG);
            SetRgbSliderValue("sliderB", _isPlayer ? ModSettings.PlayerPinB : ModSettings.OthersPinB);
            SetRgbSliderValue("sliderA", _isPlayer ? ModSettings.PlayerPinA : ModSettings.OthersPinA);
            SingleComposer.GetSlider("sliderScale")
                .SetValues(_isPlayer ? ModSettings.PlayerPinScale : ModSettings.OthersPinScale, -5, 20, 1);
            foreach (var mapLayer in capi.ModLoader.GetModSystem<WorldMapManager>().MapLayers)
            {
                if(mapLayer is PlayerMapLayer playerMapLayer)
                    playerMapLayer.OnMapOpenedClient();
            }
        }
        
        public override bool TryOpen()
        {
            if (!_isSetup) SetupDialog();
            
            var success = base.TryOpen();
            if (!success) return false;

            CustomPlayerPinsMod.Instance.CurrentDialog = this;
            RefreshValues();

            return true;
        }
        
        protected void SetupDialog()
        {
            _isSetup = true;

            var dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.RightBottom)
                .WithFixedAlignmentOffset(-GuiStyle.DialogToScreenPadding, -GuiStyle.DialogToScreenPadding);

            const int switchSize = 20;
            const int switchPadding = 10;
            const double sliderWidth = 200.0;
            var font = CairoFont.WhiteSmallText();

            var sliderBounds = ElementBounds.Fixed(160, GuiStyle.TitleBarHeight, switchSize, switchSize);
            var textBounds = ElementBounds.Fixed(0, GuiStyle.TitleBarHeight + 1.0, 150, switchSize);

            var bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
            bgBounds.BothSizing = ElementSizing.FitToChildren;

            var composer = capi.Gui.CreateCompo($"customise{_displayStr}Pins", dialogBounds)
                .AddShadedDialogBG(bgBounds)
                .AddDialogTitleBar($"Customise {_displayStr} Pins", () => TryClose())
                .BeginChildElements(bgBounds);
            
            composer.AddStaticText($"{_displayStr} R", font, textBounds);
            composer.AddHoverText($"The R component of the {_displayStr.ToLower()}'s RGBA pin colour.", font, 260, textBounds);
            composer.AddSlider(OnRChanged, sliderBounds.FlatCopy().WithFixedWidth(sliderWidth), "sliderR");
            
            textBounds = textBounds.BelowCopy(fixedDeltaY: switchPadding);
            sliderBounds = sliderBounds.BelowCopy(fixedDeltaY: switchPadding);
            composer.AddStaticText($"{_displayStr} G", font, textBounds);
            composer.AddHoverText($"The G component of the {_displayStr.ToLower()}'s RGBA pin colour.", font, 260, textBounds);
            composer.AddSlider(OnGChanged, sliderBounds.FlatCopy().WithFixedWidth(sliderWidth), "sliderG");
            
            textBounds = textBounds.BelowCopy(fixedDeltaY: switchPadding);
            sliderBounds = sliderBounds.BelowCopy(fixedDeltaY: switchPadding);
            composer.AddStaticText($"{_displayStr} B", font, textBounds);
            composer.AddHoverText($"The B component of the {_displayStr.ToLower()}'s RGBA pin colour.", font, 260, textBounds);
            composer.AddSlider(OnBChanged, sliderBounds.FlatCopy().WithFixedWidth(sliderWidth), "sliderB");
            
            textBounds = textBounds.BelowCopy(fixedDeltaY: switchPadding);
            sliderBounds = sliderBounds.BelowCopy(fixedDeltaY: switchPadding);
            composer.AddStaticText($"{_displayStr} A", font, textBounds);
            composer.AddHoverText($"The A component of the {_displayStr.ToLower()}'s RGBA pin colour.", font, 260, textBounds);
            composer.AddSlider(OnAChanged, sliderBounds.FlatCopy().WithFixedWidth(sliderWidth), "sliderA");
            
            textBounds = textBounds.BelowCopy(fixedDeltaY: switchPadding);
            sliderBounds = sliderBounds.BelowCopy(fixedDeltaY: switchPadding);
            composer.AddStaticText($"{_displayStr} Scale", font, textBounds);
            composer.AddHoverText($"The scale adjustment to apply to the {_displayStr.ToLower()}'s pin scale.", font, 260, textBounds);
            composer.AddSlider(OnScaleChanged, sliderBounds.FlatCopy().WithFixedWidth(sliderWidth), "sliderScale");
            
            // Doesn't seem to be a reasonable way to persist the context to draw dynamic content.
            // composer.AddDynamicCustomDraw(textBounds.FlatCopy().WithFixedWidth(textBounds.fixedWidth + sliderWidth + 10),
            //     (cr, surface, bounds) =>
            //     {
            //         cr.SetSourceRGBA(1,1,1,1);
            //         cr.Rectangle(0, 0, surface.Width, surface.Height);
            //         cr.Fill();
            //     });

            SingleComposer = composer.EndChildElements().Compose();
        }

        private void SetRgbSliderValue(string name, int value)
        {
            SingleComposer.GetSlider(name).SetValues(value, 0, 255, 1);
        }

        private bool OnRChanged(int r)
        {
            if (_isPlayer) ModSettings.PlayerPinR = r;
            else ModSettings.OthersPinR = r;
            RefreshValues();
            return true;
        }

        private bool OnGChanged(int g)
        {
            if (_isPlayer) ModSettings.PlayerPinG = g;
            else ModSettings.OthersPinG = g;
            RefreshValues();
            return true;
        }

        private bool OnBChanged(int b)
        {
            if (_isPlayer) ModSettings.PlayerPinB = b;
            else ModSettings.OthersPinB = b;
            RefreshValues();
            return true;
        }

        private bool OnAChanged(int a)
        {
            if (_isPlayer) ModSettings.PlayerPinA = a;
            else ModSettings.OthersPinA = a;
            RefreshValues();
            return true;
        }

        private bool OnScaleChanged(int s)
        {
            if (_isPlayer) ModSettings.PlayerPinScale = s;
            else ModSettings.OthersPinScale = s;
            RefreshValues();
            return true;
        }
    }
}