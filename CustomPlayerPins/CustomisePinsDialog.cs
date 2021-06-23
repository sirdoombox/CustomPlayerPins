using Vintagestory.API.Client;

namespace CustonPlayerPins
{
    // TODO Implement reusable GUI which swaps out Self/Others
    public class CustomisePinsDialog : AdvancedOptionsDialog
    {
        public override string ToggleKeyCombinationCode => "customisePlayerPins";
        protected override string DialogKey => "customisePinsDialog";
        protected override string DialogTitle => "Customise Player Pins";
        
        public CustomisePinsDialog(ICoreClientAPI capi) : base(capi)
        {
            RegisterOption(new ConfigOption
            {
                SliderKey = "playerR",
                Text = "Player R",
                Tooltip = "The R component of the players RGBA pin colour.",
                SlideAction = OnPlayerRChanged
            });
            RegisterOption(new ConfigOption
            {
                SliderKey = "playerG",
                Text = "Player G",
                Tooltip = "The G component of the players RGBA pin colour.",
                SlideAction = OnPlayerGChanged
            });
            RegisterOption(new ConfigOption
            {
                SliderKey = "playerB",
                Text = "Player B",
                Tooltip = "The B component of the players RGBA pin colour.",
                SlideAction = OnPlayerBChanged
            });
            RegisterOption(new ConfigOption
            {
                SliderKey = "playerA",
                Text = "Player A",
                Tooltip = "The A component of the players RGBA pin colour.",
                SlideAction = OnPlayerAChanged
            });
        }

        private bool OnPlayerBChanged(int t1)
        {
            throw new System.NotImplementedException();
        }

        private bool OnPlayerGChanged(int t1)
        {
            throw new System.NotImplementedException();
        }

        private bool OnPlayerRChanged(int t1)
        {
            throw new System.NotImplementedException();
        }

        protected override void RefreshValues()
        {
            
        }

        private void OnTitleBarCloseClicked()
        {
            TryClose();
        }
    }
}