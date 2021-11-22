using AOERandomizer.Configuration;
using AOERandomizer.View.Windows;
using AOERandomizer.ViewModel;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace AOERandomizer
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        #region Constants

        private const string SettingsFile = "AOERandomizerSettings.json";
        private const string LoadingMsg = "Loading...";

        #endregion // Constants

        #region Methods

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the splash screen, set it as the application main window and show it
            SplashScreenWindow splashScreen = new();
            SplashScreenViewModel splashScreenVm = new()
            {
                LoadingLabel = LoadingMsg
            };

            splashScreen.DataContext = splashScreenVm;
            this.MainWindow = splashScreen;
            splashScreen.Show();

            // In order to ensure the UI stays responsive, we need to do the work on a different thread
            Task.Factory.StartNew(() =>
            {
                SettingsViewModel? settingsWindowVm = null;
                AOERandomizerSettings? settings = null;

                string pathToSettings = Path.Combine(Directory.GetCurrentDirectory(), SettingsFile);
                bool validSettings = File.Exists(pathToSettings);

                if (validSettings)
                {
                    settings = JsonConvert.DeserializeObject<AOERandomizerSettings>(File.ReadAllText(pathToSettings));

                    if (settings != null)
                    {
                        if (!Directory.Exists(settings.PathToLogs) || !File.Exists(settings.PathToData))
                        {
                            validSettings = false;
                        }
                    }
                }

                if (!validSettings)
                {
                    bool cancelled = false;

                    do
                    {
                        SettingsViewModel settingsVm = new(
                            settings?.PathToLogs ?? String.Empty,
                            settings?.PathToData ?? String.Empty,
                            settings?.MusicEnabled ?? true,
                            settings?.SfxEnabled ?? true);

                        this.Dispatcher.Invoke(() =>
                        {
                            SettingsWindow settingsWindow = new();
                            settingsWindow.DataContext = settingsVm;

                            bool? result = settingsWindow.ShowDialog();
                            if (result.HasValue && result.Value)
                            {
                                settingsWindowVm = (SettingsViewModel)settingsWindow.DataContext;

                                if (!String.IsNullOrWhiteSpace(settingsWindowVm.PathToLogs) && !String.IsNullOrWhiteSpace(settingsWindowVm.PathToData))
                                {
                                    settings = new AOERandomizerSettings() { PathToLogs = settingsWindowVm.PathToLogs, PathToData = settingsWindowVm.PathToData };
                                    File.WriteAllText(pathToSettings, JsonConvert.SerializeObject(settings, Formatting.Indented));
                                }
                            }
                            else
                            {
                                cancelled = true;
                            }
                        });
                    }
                    while (!File.Exists(pathToSettings) && !cancelled);
                }
                else
                {
                    settings = JsonConvert.DeserializeObject<AOERandomizerSettings>(File.ReadAllText(pathToSettings));

                    if (settings != null)
                    {
                        settingsWindowVm = new SettingsViewModel(settings.PathToLogs, settings.PathToData, settings.MusicEnabled, settings.SfxEnabled);
                    }
                }

                if (
                    settingsWindowVm != null &&
                    !String.IsNullOrWhiteSpace(settingsWindowVm.PathToLogs) && Directory.Exists(settingsWindowVm.PathToLogs) &&
                    !String.IsNullOrWhiteSpace(settingsWindowVm.PathToData) && File.Exists(settingsWindowVm.PathToData))
                {
                    MainWindowViewModel mainWindowVm = new(splashScreenVm, settingsWindowVm);
                    mainWindowVm.Load();

                    // Since we're not on the UI thread once we're done we need to use the Dispatcher to create and show the main window
                    this.Dispatcher.Invoke(() =>
                    {
                        // Initialize the main window, set it as the application main window and close the splash screen
                        MainWindow mainWindow = new();
                        mainWindow.Closing += new CancelEventHandler(OnMainWindowClosing);
                        mainWindow.DataContext = mainWindowVm;

                        this.MainWindow = mainWindow;

                        mainWindow.Show();
                        splashScreen.Close();
                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        splashScreen.Close();
                    });
                }
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
            if (this.MainWindow != null && ((MainWindowViewModel)this.MainWindow.DataContext).Settings != null)
            {
                string pathToSettings = Path.Combine(Directory.GetCurrentDirectory(), SettingsFile);

                if (!File.Exists(pathToSettings))
                {
                    File.Create(pathToSettings);
                }

                SettingsViewModel settingsWindowVm = ((MainWindowViewModel)this.MainWindow.DataContext).Settings;
                AOERandomizerSettings settings = new()
                {
                    PathToLogs = settingsWindowVm.PathToLogs,
                    PathToData = settingsWindowVm.PathToData,
                    MusicEnabled = settingsWindowVm.MusicEnabled,
                    SfxEnabled = settingsWindowVm.SfxEnabled
                };

                File.WriteAllText(pathToSettings, JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
        }

        #endregion // Methods
    }
}