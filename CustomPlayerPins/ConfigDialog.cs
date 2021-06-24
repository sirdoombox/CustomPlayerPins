using Vintagestory.API.Client;

namespace CustomPlayerPins
{
    public class ConfigDialog : GuiDialog
    {
        public override string ToggleKeyCombinationCode => "customisePlayerPins";

        private bool _isSetup;

        public ConfigDialog(ICoreClientAPI capi) : base(capi)
        {
        }

        protected void SetupDialog()
        {
            _isSetup = true;

            var dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.RightBottom)
                .WithFixedAlignmentOffset(-GuiStyle.DialogToScreenPadding, -GuiStyle.DialogToScreenPadding);

            var font = CairoFont.WhiteSmallText();

            var buttonBounds = ElementBounds.Fixed(0, GuiStyle.TitleBarHeight, 250, 15);

            var bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
            bgBounds.BothSizing = ElementSizing.FitToChildren;

            var composer = capi.Gui.CreateCompo("customPlayerPinsConfigure", dialogBounds)
                .AddShadedDialogBG(bgBounds)
                .AddDialogTitleBar("Custom Player Pins Config", OnTitleBarCloseClicked)
                .BeginChildElements(bgBounds);
            composer.AddSmallButton("Customise Player Pins", () => new CustomisePinsDialog(capi, true).TryOpen(),
                buttonBounds);
            composer.AddSmallButton("Customise Others Pins", () => new CustomisePinsDialog(capi, false).TryOpen(),
                buttonBounds.BelowCopy(fixedDeltaY: 10));
            SingleComposer = composer.EndChildElements().Compose();
        }

        public override bool TryOpen()
        {
            if (!_isSetup) SetupDialog();

            var success = base.TryOpen();
            if (!success) return false;

            CustomPlayerPinsMod.Instance.CurrentDialog = this;

            return true;
        }

        private void OnTitleBarCloseClicked() => TryClose();
    }
}