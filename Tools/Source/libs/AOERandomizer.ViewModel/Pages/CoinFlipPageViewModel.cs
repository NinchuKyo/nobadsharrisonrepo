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
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Pages.CoinFlipPageViewModel";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        /// <param name="dataConfig">Application data config.</param>
        public CoinFlipPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm, DataConfig dataConfig)
            : base(settingsConfig, navVm, dataConfig)
        {
            this._log.InfoCtx(LOG_CTX, "CoinFlipPageViewModel created");
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        public override void Load()
        {
            using (this._log.ProfileCtx(LOG_CTX, "Loading CoinFlipPageViewModel"))
            {
                // Add home button
                string iconPath = $"{ButtonIconsPath}/{PageName.Home}.png";
                this._pageButtons.Add(new(PageName.Home, iconPath));
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
                    case PageName.Home:
                        HomePageViewModel homePageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                        homePageVm.Load();
                        this._navVm.SelectedVm = homePageVm;
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