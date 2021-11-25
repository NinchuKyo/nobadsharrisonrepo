using AOERandomizer.Configuration;
using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Commands;
using AOERandomizer.ViewModel.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AOERandomizer.ViewModel.Pages
{
    public abstract class PageBaseViewModel : ViewModelBase
    {
        #region Constants

        protected const string ButtonIconsPath = $"pack://application:,,,/AOERandomizer.Multimedia;component/Resources/Images/Buttons";

        #endregion // Constants

        #region Members

        protected readonly NavigationViewModel _navVm;
        protected readonly ObservableCollection<PageButtonViewModel> _pageButtons;

        #endregion // Members

        #region Commands

        public ICommand NavigateCommand { get; }

        #endregion // Commands

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        internal PageBaseViewModel(AppConfig settingsConfig, NavigationViewModel navVm)
        {
            this.SettingsConfig = settingsConfig;
            this._navVm = navVm;

            this.NavigateCommand = new RelayCommand(CanExecuteNavigateCommand, ExecuteNavigateCommand);
            this._pageButtons = new ObservableCollection<PageButtonViewModel>();
        }

        protected abstract void ExecuteNavigateCommand(object? param);

        protected virtual bool CanExecuteNavigateCommand(object? param)
        {
            return true;
        }

        #endregion // Constructors

        #region Properties

        public AppConfig SettingsConfig { get; private set; }

        public ObservableCollection<PageButtonViewModel> PageButtons => this._pageButtons;

        #endregion // Properties

        #region Methods

        public abstract void Load();

        #endregion // Methods
    }
}