using AOERandomizer.Configuration;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Navigation;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Viewmodel for the maps page.
    /// </summary>
    public class MapsPageViewModel : PageBaseViewModel
    {
        public MapsPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
            : base(settingsConfig, navVm)
        {
        }

        public override void Load()
        {
            // Add civs button
            string iconPath = $"{ButtonIconsPath}/{EPageName.Civs}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Civs, iconPath));

            // Add home button
            iconPath = $"{ButtonIconsPath}/{EPageName.Home}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Home, iconPath));

            // TODO: Add summary button
        }

        protected override void ExecuteNavigateCommand(object? param)
        {
            if (param is PageButtonViewModel button)
            {
                switch (button.PageName)
                {
                    case EPageName.Civs:
                        CivsPageViewModel civsPageVm = new(this.SettingsConfig, this._navVm);
                        civsPageVm.Load();
                        this._navVm.SelectedVm = civsPageVm;
                        break;
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