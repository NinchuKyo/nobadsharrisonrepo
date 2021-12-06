﻿using AOERandomizer.Configuration;
using AOERandomizer.Logging;
using AOERandomizer.RandomGeneration;
using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Navigation;
using AOERandomizer.ViewModel.Pages;
using FroggoBase;
using System;
using System.ComponentModel;
using System.Threading;

namespace AOERandomizer.ViewModel.Windows
{
    /// <summary>
    /// Viewmodel for the main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Windows.MainWindowViewModel";
        private const int MaxDoge = 400;

        #endregion // Constants

        #region Members

        private readonly SplashScreenWindowViewModel _splashScreenVm;
        private readonly AppConfig _settingsConfig;
        private readonly DataConfig _dataConfig;

        private readonly ILog _log;

        private readonly NavigationViewModel _navManager;
        private readonly HomePageViewModel _homePageVm;

        private readonly Random _random;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="splashScreenVm">Since this viewmodel is initialized while the splash screen is visible, this is used to update the loading status text label.</param>
        /// <param name="settingsConfig">Settings configuration.</param>
        /// <param name="dataConfig">Data configuration.</param>
        public MainWindowViewModel(SplashScreenWindowViewModel splashScreenVm, AppConfig settingsConfig, DataConfig dataConfig)
        {
            this._splashScreenVm = splashScreenVm;
            this._settingsConfig = settingsConfig;
            this._dataConfig = dataConfig;

            this._log = FroggoApplication.ApplicationLog;

            this._navManager = new();
            this._homePageVm = new(settingsConfig, this._navManager, dataConfig);

            this._random = new(MasterRNG.GetRandomNumberFrom(0, Int32.MaxValue - 1));

            this._navManager.PropertyChanged += this.NavManager_PropertyChanged;
            this._navManager.SelectedVm = this._homePageVm;

            this._log.InfoCtx(LOG_CTX, "MainWindowViewmodel created.");
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the currently selected viewmodel (used for page navigation).
        /// </summary>
        public ViewModelBase? SelectedVm => this._navManager.SelectedVm;

        /// <summary>
        /// Gets the application settings configuration.
        /// </summary>
        public AppConfig AppSettings => this._settingsConfig;

        /// <summary>
        /// Gets the application data configuration.
        /// </summary>
        public DataConfig AppData => this._dataConfig;

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Loads any necessary data or dependencies for the main window.
        /// </summary>
        public void Load()
        {
            using (this._log.ProfileCtx(LOG_CTX, "Loading MainWindowViewModel"))
            {
                // Load the home page viewmodel...
                this._homePageVm.Load();

                // Simulate some work being done (for the lols, and to 'warm up' the RNG)
                int max = this._random.Next(MaxDoge + 1);
                for (int i = 0; i <= max; i++)
                {
                    this._splashScreenVm.LoadingLabel = $"Loaded {i} / {max} Dogecoin";
                    MasterRNG.GetRandomNumberFrom(0, this._random.Next());
                    Thread.Sleep(1);
                }

                this._splashScreenVm.LoadingLabel = "no bnads";
                Thread.Sleep(1500);
            }
        }

        /// <summary>
        /// Triggers when the nav manager selects a new viewmodel to show.  
        /// This will tell our main window when to change the page.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void NavManager_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(e.PropertyName) && e.PropertyName.Equals("SelectedVm"))
            {
                this.OnPropertyChanged(nameof(SelectedVm));
            }
        }

        #endregion // Methods
    }
}