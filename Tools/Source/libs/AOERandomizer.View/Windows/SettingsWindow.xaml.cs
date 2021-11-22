using AOERandomizer.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace AOERandomizer.View.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Triggers when the mouse is pressed anywhere on the window.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Triggers when the minimize button is clicked.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        /// <summary>
        /// Triggers when the close button is clicked.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        /// <summary>
        /// Triggers when the ok button is clicked.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsViewModel settingsVm = (SettingsViewModel)this.DataContext;

            if (!Directory.Exists(settingsVm.PathToLogs))
            {
                MessageBox.Show($"Content data path '{settingsVm.PathToLogs}' does not exist!", "ERROR: Content path not found", MessageBoxButton.OK, MessageBoxImage.Error);
                settingsVm.ResetLogsPath();
                return;
            }

            if (!File.Exists(settingsVm.PathToData))
            {
                MessageBox.Show($"Grp_d.xml path '{settingsVm.PathToData}' does not exist!", "ERROR: Grp_d.xml path not found", MessageBoxButton.OK, MessageBoxImage.Error);
                settingsVm.ResetDataPath();
                return;
            }

            this.DialogResult = true;
        }

        /// <summary>
        /// Triggers when the cancel button is clicked.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsViewModel settingsVm = (SettingsViewModel)this.DataContext;

            settingsVm.ResetLogsPath();
            settingsVm.ResetDataPath();
            settingsVm.ResetMusicEnabled();
            settingsVm.ResetSfxEnabled();

            this.DialogResult = false;
        }
    }
}