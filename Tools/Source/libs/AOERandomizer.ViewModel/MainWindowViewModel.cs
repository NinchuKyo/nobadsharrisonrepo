using AOERandomizer.Utility;
using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Commands;
using AOERandomizer.ViewModel.Enums;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace AOERandomizer.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Members

        private readonly SplashScreenViewModel _splashScreenVm;
        private readonly SettingsViewModel _settingsWindowVm;
        private readonly ObservableCollection<DataCategoryViewModel> _dataCategories;

        private bool _backgroundMusicMuted;
        private bool _backgroundMusicUnmuted;

        #endregion // Members

        #region Commands

        public ICommand SettingsCommand { get; set; }

        #endregion // Commands

        #region Constructors

        public MainWindowViewModel(SplashScreenViewModel splashScreenVm, SettingsViewModel settingsWindowVm)
        {
            this._splashScreenVm = splashScreenVm;
            this._settingsWindowVm = settingsWindowVm;
            this._dataCategories = new ObservableCollection<DataCategoryViewModel>();

            this._backgroundMusicMuted = false;
            this._backgroundMusicUnmuted = true;

            this.SettingsCommand = new RelayCommand(CanExecuteSettingsCommand, ExecuteSettingsCommand);
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public SettingsViewModel Settings => this._settingsWindowVm;

        /// <summary>
        /// Gets the collection of available data categories.
        /// </summary>
        public ObservableCollection<DataCategoryViewModel> DataCategories => this._dataCategories;

        public bool IsBackgroundMusicMuted
        {
            get => this._backgroundMusicMuted;
            set => this.SetProperty(ref this._backgroundMusicMuted, value);
        }

        public bool IsBackgroundMusicUnmuted
        {
            get => this._backgroundMusicUnmuted;
            set => this.SetProperty(ref this._backgroundMusicUnmuted, value);
        }

        #endregion // Properties

        #region Methods

        public void Load()
        {
            foreach (EDataCategory dataCategory in Enum.GetValues(typeof(EDataCategory)))
            {
                string iconPath = $"pack://application:,,,/AOERandomizer.Multimedia;component/Resources/Images/MainWindow/{dataCategory}.png";
                this._dataCategories.Add(new DataCategoryViewModel(dataCategory.GetDisplayName(), iconPath));
            }

            // Simulate some work being done
            int max = 1000;
            for (int i = 0; i <= max; i++)
            {
                this._splashScreenVm.LoadingLabel = $"Loaded {i} / {max} Dogecoin";
                Thread.Sleep(1);
            }

            this._splashScreenVm.LoadingLabel = "no bnads";
            Thread.Sleep(1500);
        }

        private void ExecuteSettingsCommand(object? obj)
        {
        }

        private bool CanExecuteSettingsCommand(object? obj)
        {
            return true;
        }

        #endregion // Methods
    }
}