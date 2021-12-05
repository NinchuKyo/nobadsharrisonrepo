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
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Pages.HomePageViewModel";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        /// <param name="dataConfig">Application data config.</param>
        public HomePageViewModel(AppConfig settingsConfig, NavigationViewModel navVm, DataConfig dataConfig)
            : base(settingsConfig, navVm, dataConfig)
        {
            this._log.InfoCtx(LOG_CTX, "HomePageViewModel created");
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        public override void Load()
        {
            using (this._log.ProfileCtx(LOG_CTX, "Loading HomePageViewModel"))
            {
                foreach (PageName pageName in Enum.GetValues(typeof(PageName)))
                {
                    // Skip home button
                    if (pageName != PageName.Home)
                    {
                        string iconPath = $"{ButtonIconsPath}/{pageName}.png";
                        this._pageButtons.Add(new(pageName, iconPath));
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override void ExecuteNavigateCommand(object? param)
        {
            if (param is PageButtonViewModel button)
            {
                this._log.InfoCtx(LOG_CTX, $"Navigating to {button.PageName} page");

                switch (button.PageName)
                    {
                        case PageName.Teams:
                            TeamsPageViewModel teamPageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                            teamPageVm.Load();
                            this._navVm.SelectedVm = teamPageVm;
                            break;
                        case PageName.Civs:
                            CivsPageViewModel civsPageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                            civsPageVm.Load();
                            this._navVm.SelectedVm = civsPageVm;
                            break;
                        case PageName.Maps:
                            MapsPageViewModel mapsPageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                            mapsPageVm.Load();
                            this._navVm.SelectedVm = mapsPageVm;
                            break;
                        case PageName.CoinFlip:
                            CoinFlipPageViewModel coinFlipPageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                            coinFlipPageVm.Load();
                            this._navVm.SelectedVm = coinFlipPageVm;
                            break;
                        default:
                            this._log.WarningCtx(LOG_CTX, $"Page {button.PageName} not supported for navigation");
                            break;
                    }
            }
        }

        #endregion // Methods
    }
}