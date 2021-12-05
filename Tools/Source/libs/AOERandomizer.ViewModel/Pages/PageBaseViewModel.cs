using AOERandomizer.Configuration;
using AOERandomizer.Logging;
using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Commands;
using AOERandomizer.ViewModel.Navigation;
using FroggoBase;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Base viewmodel for a page.
    /// </summary>
    public abstract class PageBaseViewModel : ViewModelBase
    {
        #region Constants

        protected const string ButtonIconsPath = $"pack://application:,,,/AOERandomizer.Multimedia;component/Resources/Images/PageButtons";

        #endregion // Constants

        #region Members

        protected readonly NavigationViewModel _navVm;
        protected readonly ILog _log;
        protected readonly ObservableCollection<PageButtonViewModel> _pageButtons;

        #endregion // Members

        #region Commands

        /// <summary>
        /// Gets or sets the navigate command.
        /// </summary>
        public ICommand NavigateCommand { get; }

        #endregion // Commands

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        /// <param name="dataConfig">Application data config.</param>
        internal PageBaseViewModel(AppConfig settingsConfig, NavigationViewModel navVm, DataConfig dataConfig)
        {
            this.SettingsConfig = settingsConfig;
            this._navVm = navVm;
            this.DataConfig = dataConfig;

            this._log = FroggoApplication.ApplicationLog;
            this._pageButtons = new();

            this.NavigateCommand = new RelayCommand(CanExecuteNavigateCommand, ExecuteNavigateCommand);
        }

        /// <summary>
        /// Executes the navigate command.
        /// </summary>
        /// <param name="param">Command parameter.</param>
        protected abstract void ExecuteNavigateCommand(object? param);

        /// <summary>
        /// Returns a flag indicating whether the navigate command can be executed.
        /// </summary>
        /// <param name="param">Command parameter.</param>
        /// <returns>Flag indicating whether the navigate command can execute.</returns>
        protected virtual bool CanExecuteNavigateCommand(object? param)
        {
            return true;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the application settings configuration.
        /// </summary>
        public AppConfig SettingsConfig { get; private set; }

        /// <summary>
        /// Gets or sets the application data configuration.
        /// </summary>
        public DataConfig DataConfig { get; private set; }

        /// <summary>
        /// Gets the page navigation buttons.
        /// </summary>
        public ObservableCollection<PageButtonViewModel> PageButtons => this._pageButtons;

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Loads the necessary resources required for the associated page to run.
        /// </summary>
        public abstract void Load();

        #endregion // Methods
    }
}