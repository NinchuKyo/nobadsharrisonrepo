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
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Pages.MapsPageViewModel";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        public MapsPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
            : base(settingsConfig, navVm)
        {
            this._log.InfoCtx(LOG_CTX, "MapsPageViewModel created");
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        public override void Load()
        {
            using (this._log.ProfileCtx(LOG_CTX, "Loading MapsPageViewModel"))
            {
                // Add civs button
                string iconPath = $"{ButtonIconsPath}/{EPageName.Civs}.png";
                this._pageButtons.Add(new PageButtonViewModel(EPageName.Civs, iconPath));

                // Add home button
                iconPath = $"{ButtonIconsPath}/{EPageName.Home}.png";
                this._pageButtons.Add(new PageButtonViewModel(EPageName.Home, iconPath));

                // TODO: Add summary button
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
                        this._log.WarningCtx(LOG_CTX, $"Page {button.PageName} not supported for navigation");
                        break;
                }
            }
        }

        #endregion // Methods
    }
}