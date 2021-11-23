using AOERandomizer.Configuration;
using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Commands;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Extensions;
using AOERandomizer.ViewModel.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Viewmodel for the home page.
    /// </summary>
    public class HomePageViewModel : ViewModelBase
    {
        #region Constants

        private const string MainWindowButtonIconsPath = $"pack://application:,,,/AOERandomizer.Multimedia;component/Resources/Images/MainWindow";

        #endregion // Constants

        #region Members

        private readonly AppConfig _settingsConfig;
        private readonly NavigationViewModel _navVm;

        private readonly ObservableCollection<PageButtonViewModel> _pageButtons;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        public HomePageViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
        {
            this._settingsConfig = settingsConfig;
            this._navVm = navVm;

            this._pageButtons = new ObservableCollection<PageButtonViewModel>();

            this.NavigateCommand = new RelayCommand(CanExecuteNavigateCommand, ExecuteNavigateCommand);
        }

        #endregion // Constructors

        #region Commands

        public ICommand NavigateCommand { get; }

        #endregion // Commands

        #region Properties

        /// <summary>
        /// Gets the application settings config.
        /// </summary>
        public AppConfig AppSettings => this._settingsConfig;

        /// <summary>
        /// Gets the collection of available page (buttons).
        /// </summary>
        public ObservableCollection<PageButtonViewModel> PageButtons => this._pageButtons;

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Loads any necessary data associate with this viewmodel.
        /// </summary>
        public void Load()
        {
            foreach (EPageName pageName in Enum.GetValues(typeof(EPageName)))
            {
                string iconPath = $"{MainWindowButtonIconsPath}/{pageName}.png";
                this._pageButtons.Add(new PageButtonViewModel(pageName, iconPath));
            }
        }

        private bool CanExecuteNavigateCommand(object? param)
        {
            return true;
        }

        private void ExecuteNavigateCommand(object? param)
        {
            if (param is PageButtonViewModel button)
            {
                    switch(button.PageName)
                    {
                        case EPageName.Teams:
                            this._navVm.SelectedVm = new TeamsPageViewModel();
                            break;
                        case EPageName.Civs:
                            this._navVm.SelectedVm = new CivsPageViewModel();
                            break;
                        case EPageName.Maps:
                            this._navVm.SelectedVm = new MapsPageViewModel();
                            break;
                        case EPageName.CoinFlip:
                            this._navVm.SelectedVm = new CoinFlipPageViewModel();
                            break;
                        default:
                            break;
                    }
            }
        }

        #endregion // Methods
    }
}