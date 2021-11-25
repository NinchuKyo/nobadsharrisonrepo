using AOERandomizer.Configuration;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Navigation;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Viewmodel for the teams page.
    /// </summary>
    public class TeamsPageViewModel : PageBaseViewModel
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Pages.TeamsPageViewModel";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        public TeamsPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
            : base(settingsConfig, navVm)
        {
            this._log.InfoCtx(LOG_CTX, "TeamsPageViewModel created");
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        public override void Load()
        {
            using (this._log.ProfileCtx(LOG_CTX, "Loading TeamsPageViewModel"))
            {
                // Add home button
                string iconPath = $"{ButtonIconsPath}/{EPageName.Home}.png";
                this._pageButtons.Add(new PageButtonViewModel(EPageName.Home, iconPath));

                // Add civs button
                iconPath = $"{ButtonIconsPath}/{EPageName.Civs}.png";
                this._pageButtons.Add(new PageButtonViewModel(EPageName.Civs, iconPath));
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
                    case EPageName.Home:
                        HomePageViewModel homePageVm = new(this.SettingsConfig, this._navVm);
                        homePageVm.Load();
                        this._navVm.SelectedVm = homePageVm;
                        break;
                    case EPageName.Civs:
                        CivsPageViewModel civsPageVm = new(this.SettingsConfig, this._navVm);
                        civsPageVm.Load();
                        this._navVm.SelectedVm = civsPageVm;
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