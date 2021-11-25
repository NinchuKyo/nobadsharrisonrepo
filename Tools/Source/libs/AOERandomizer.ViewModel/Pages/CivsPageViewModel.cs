using AOERandomizer.Configuration;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Navigation;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Viewmodel for the civs page.
    /// </summary>
    public class CivsPageViewModel : PageBaseViewModel
    {
        public CivsPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm) 
            : base(settingsConfig, navVm)
        {
        }

        public override void Load()
        {
            // Add teams button
            string iconPath = $"{ButtonIconsPath}/{EPageName.Teams}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Teams, iconPath));

            // Add home button
            iconPath = $"{ButtonIconsPath}/{EPageName.Home}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Home, iconPath));

            // Add maps button
            iconPath = $"{ButtonIconsPath}/{EPageName.Maps}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Maps, iconPath));
        }

        protected override void ExecuteNavigateCommand(object? param)
        {
            if (param is PageButtonViewModel button)
            {
                switch (button.PageName)
                {
                    case EPageName.Teams:
                        TeamsPageViewModel teamPageVm = new(this.SettingsConfig, this._navVm);
                        teamPageVm.Load();
                        this._navVm.SelectedVm = teamPageVm;
                        break;
                    case EPageName.Home:
                        HomePageViewModel homePageVm = new(this.SettingsConfig, this._navVm);
                        homePageVm.Load();
                        this._navVm.SelectedVm = homePageVm;
                        break;
                    case EPageName.Maps:
                        MapsPageViewModel mapsPageVm = new(this.SettingsConfig, this._navVm);
                        mapsPageVm.Load();
                        this._navVm.SelectedVm = mapsPageVm;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}