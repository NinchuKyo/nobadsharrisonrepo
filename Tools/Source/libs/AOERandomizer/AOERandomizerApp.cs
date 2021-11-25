using AOERandomizer.Configuration;
using AOERandomizer.View.Windows;
using AOERandomizer.ViewModel.Windows;
using FroggoBase;
using System.ComponentModel;
using System.Windows;

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

        private AppConfig? _appSettingsConfig;
        private DataConfig? _appDataConfig;

        #endregion // Members

        #region Methods

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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
                        mainWindow.Closing += new CancelEventHandler(this.OnMainWindowClosing);
                        mainWindow.DataContext = mainWindowVm;

                        this.MainWindow = mainWindow;

                        mainWindow.Show();
                        splashScreen.Close();
                    }
                });
            });
        }

        /// <summary>
        /// Triggered when the main window is closing, but hasn't closed yet.
        /// We want to take this opportunity to save our settings before exiting.
        /// </summary>
        /// <param name="sender">Object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainWindowClosing(object? sender, CancelEventArgs e)
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
        }

        #endregion // Methods
    }
}