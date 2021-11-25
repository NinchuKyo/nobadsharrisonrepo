using AOERandomizer.Configuration;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Navigation;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Viewmodel for the coin flip page.
    /// </summary>
    public class CoinFlipPageViewModel : PageBaseViewModel
    {
        public CoinFlipPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
            : base(settingsConfig, navVm)
        {
        }

        public override void Load()
        {
            // Add home button
            string iconPath = $"{ButtonIconsPath}/{EPageName.Home}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Home, iconPath));
        }

        protected override void ExecuteNavigateCommand(object? param)
        {
            if (param is PageButtonViewModel button)
            {
                switch (button.PageName)
                {
                    case EPageName.Home:
                        HomePageViewModel homePageVm = new(this.SettingsConfig, this._navVm);
                        homePageVm.Load();
                        this._navVm.SelectedVm = homePageVm;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}