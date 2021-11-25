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
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        public TeamsPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
            : base(settingsConfig, navVm)
        {
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        public override void Load()
        {
            // Add home button
            string iconPath = $"{ButtonIconsPath}/{EPageName.Home}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Home, iconPath));

            // Add civs button
            iconPath = $"{ButtonIconsPath}/{EPageName.Civs}.png";
            this._pageButtons.Add(new PageButtonViewModel(EPageName.Civs, iconPath));
        }

        /// <inheritdoc />
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
                    case EPageName.Civs:
                        CivsPageViewModel civsPageVm = new(this.SettingsConfig, this._navVm);
                        civsPageVm.Load();
                        this._navVm.SelectedVm = civsPageVm;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion // Methods
    }
}