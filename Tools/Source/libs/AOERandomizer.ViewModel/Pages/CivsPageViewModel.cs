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
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Pages.CivsPageViewModel";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        /// <param name="dataConfig">Application data config.</param>
        public CivsPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm, DataConfig dataConfig)
            : base(settingsConfig, navVm, dataConfig)
        {
            this._log.InfoCtx(LOG_CTX, "CivsPageViewModel created");
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        public override void Load()
        {
            using (this._log.ProfileCtx(LOG_CTX, "Loading CivsPageViewModel"))
            {
                // Add teams button
                string iconPath = $"{ButtonIconsPath}/{PageName.Teams}.png";
                this._pageButtons.Add(new(PageName.Teams, iconPath));

                // Add home button
                iconPath = $"{ButtonIconsPath}/{PageName.Home}.png";
                this._pageButtons.Add(new(PageName.Home, iconPath));

                // Add maps button
                iconPath = $"{ButtonIconsPath}/{PageName.Maps}.png";
                this._pageButtons.Add(new(PageName.Maps, iconPath));
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
                    case PageName.Home:
                        HomePageViewModel homePageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                        homePageVm.Load();
                        this._navVm.SelectedVm = homePageVm;
                        break;
                    case PageName.Maps:
                        MapsPageViewModel mapsPageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                        mapsPageVm.Load();
                        this._navVm.SelectedVm = mapsPageVm;
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