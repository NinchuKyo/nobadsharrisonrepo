using AOERandomizer.Multimedia;
using AOERandomizer.ViewModel.Windows;
using FroggoBase;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace AOERandomizer.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.View.Windows.MainWindow";

        private const string BackgroundVideoPath = @"Resources\Media\Video\background_video.mp4";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            // Hacky solution - reference the video player in the XAML - must be done after initializing UI components
            this.backgroundVideo.Source = new Uri(Path.Combine(Environment.CurrentDirectory, BackgroundVideoPath), UriKind.Relative);
        }

        #endregion // Constructors

        #region UI Events

        /// <summary>
        /// Triggers when the mouse is pressed anywhere on the window.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
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
            this.Button_Click(sender, e);
            SystemCommands.MinimizeWindow(this);
        }

        /// <summary>
        /// Triggers when the close button is clicked.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Button_Click(sender, e);
            SystemCommands.CloseWindow(this);
        }

        /// <summary>
        /// Triggers when the user enters a button with their mouse pointer.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((MainWindowViewModel)this.DataContext).AppSettings.EnableSfx)
            {
                AudioHelper.PlayButtonHoverSound();
            }
        }

        /// <summary>
        /// Triggers when the users clicks a button with their mouse.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((MainWindowViewModel)this.DataContext).AppSettings.EnableSfx)
            {
                AudioHelper.PlayButtonClickSound();
            }
        }

        /// <summary>
        /// Triggers when the main window is finished loading.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AudioHelper.ToggleBackgroundMusicMute(!((MainWindowViewModel)DataContext).AppSettings.EnableMusic);
            AudioHelper.PlayBackgroundMusic();
        }

        /// <summary>
        /// Triggers when the background video has finished loading and is ready to play.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void BackgroundVideo_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.backgroundVideo.Play();
            }
            catch (Exception ex)
            {
                FroggoApplication.ApplicationLog.ExceptionCtx(LOG_CTX, "Could not play background video", ex);
            }
        }

        /// <summary>
        /// Triggeres when the video playing in the background finishes playing.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.backgroundVideo.Position = TimeSpan.FromSeconds(0);
        }

        #endregion // UI Events
    }
}