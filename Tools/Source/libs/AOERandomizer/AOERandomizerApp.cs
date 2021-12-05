using AOERandomizer.Configuration;
using AOERandomizer.View.Windows;
using AOERandomizer.ViewModel.Windows;
using FroggoBase;
using System.Windows;
using System.Windows.Threading;

namespace AOERandomizer
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class AOERandomizerApp : FroggoApplication
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizerApp.AOERandomizerApp";
        private const string LoadingMsg = "Loading...";

        #endregion // Constants

        #region Members

        private AppConfig _appSettingsConfig;
        private DataConfig _appDataConfig;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AOERandomizerApp()
        {
            this._appSettingsConfig = new();
            this._appDataConfig = new();
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += AOERandomizerApp_DispatcherUnhandledException;

            SplashScreenWindow splashScreen;
            SplashScreenWindowViewModel splashScreenVm;

            using (ApplicationLog.ProfileCtx(LOG_CTX, "Spinning up splash screen"))
            {
                splashScreen = new();
                splashScreenVm = new()
                {
                    LoadingLabel = LoadingMsg
                };

                splashScreen.DataContext = splashScreenVm;
                this.MainWindow = splashScreen;
                splashScreen.Show();
            }

            // In order to ensure the UI stays responsive, we need to do the work on a different thread
            Task.Factory.StartNew(() =>
            {
                // Before we spin up our main window's viewmodel, first load our configurations / data...
                using (ApplicationLog.ProfileCtx(LOG_CTX, "Loading settings and data configurations"))
                {
                    try
                    {
                        this._appSettingsConfig = ConfigManager.LoadSettingsConfig();
                        this._appDataConfig = ConfigManager.LoadDataConfig();
                    }
                    catch (Exception ex)
                    {
                        ApplicationLog.ExceptionCtx(LOG_CTX, "Unhandled exception while loading configuration files", ex);
                    }
                }

                MainWindowViewModel mainWindowVm = new(splashScreenVm, this._appSettingsConfig, this._appDataConfig);
                mainWindowVm.Load();

                // Since we're not on the UI thread once we're done we need to use the Dispatcher to create and show the main window
                this.Dispatcher.Invoke(() =>
                {
                    using (ApplicationLog.ProfileCtx(LOG_CTX, "Spinning up main window"))
                    {
                        // Initialize the main window, set it as the application main window and close the splash screen
                        MainWindow mainWindow = new();
                        mainWindow.DataContext = mainWindowVm;

                        this.MainWindow = mainWindow;

                        mainWindow.Show();
                        splashScreen.Close();
                    }
                });
            });
        }

        /// <inheritdoc />
        protected override void OnExit(ExitEventArgs e)
        {
            using (ApplicationLog.ProfileCtx(LOG_CTX, "Saving settings and data configurations before shutting down"))
            {
                try
                {
                    ConfigManager.SaveSettingsConfig(this._appSettingsConfig);
                    ConfigManager.SaveDataConfig(this._appDataConfig);
                }
                catch (Exception ex)
                {
                    ApplicationLog.ExceptionCtx(LOG_CTX, "Unhandled exception while saving configuration files", ex);
                }
            }

            // Dispose (flush) the log...
            ApplicationLog.Dispose();

            base.OnExit(e);
        }

        /// <summary>
        /// Triggered when the application experiences an exception that remains unhandled.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void AOERandomizerApp_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ApplicationLog.ExceptionCtx(LOG_CTX, "Unhandled exception occurred in the application", e.Exception);
        }

        #endregion // Methods
    }
}