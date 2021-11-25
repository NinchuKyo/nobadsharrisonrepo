using AOERandomizer.Configuration;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Navigation;
using System;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Viewmodel for the home page.
    /// </summary>
    public class HomePageViewModel : PageBaseViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        public HomePageViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
            : base(settingsConfig, navVm)
        {
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Loads any necessary data associate with this viewmodel.
        /// </summary>
        public override void Load()
        {
            foreach (EPageName pageName in Enum.GetValues(typeof(EPageName)))
            {
                // Skip home button
                if (pageName != EPageName.Home)
                {
                    string iconPath = $"{ButtonIconsPath}/{pageName}.png";
                    this._pageButtons.Add(new PageButtonViewModel(pageName, iconPath));
                }
            }
        }

        protected override void ExecuteNavigateCommand(object? param)
        {
            if (param is PageButtonViewModel button)
            {
                    switch(button.PageName)
                    {
                        case EPageName.Teams:
                            TeamsPageViewModel teamPageVm = new(this.SettingsConfig, this._navVm);
                            teamPageVm.Load();
                            this._navVm.SelectedVm = teamPageVm;
                            break;
                        case EPageName.Civs:
                            CivsPageViewModel civsPageVm = new(this.SettingsConfig, this._navVm);
                            civsPageVm.Load();
                            this._navVm.SelectedVm = civsPageVm;
                            break;
                        case EPageName.Maps:
                            MapsPageViewModel mapsPageVm = new(this.SettingsConfig, this._navVm);
                            mapsPageVm.Load();
                            this._navVm.SelectedVm = mapsPageVm;
                            break;
                        case EPageName.CoinFlip:
                            CoinFlipPageViewModel coinFlipPageVm = new(this.SettingsConfig, this._navVm);
                            coinFlipPageVm.Load();
                            this._navVm.SelectedVm = coinFlipPageVm;
                            break;
                        default:
                            break;
                    }
            }
        }

        #endregion // Methods
    }
}