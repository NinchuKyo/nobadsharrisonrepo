using AOERandomizer.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AOERandomizer.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members

        private readonly MediaPlayer _buttonMouseOverSound;
        private readonly MediaPlayer _buttonClickSound;
        private readonly MediaPlayer _backgroundMusic;

        private readonly Uri _buttonMouseOverUri;
        private readonly Uri _buttonClickUri;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            this._buttonMouseOverSound = new MediaPlayer
            {
                Volume = 0.1
            };

            this._buttonMouseOverUri = new Uri(Environment.CurrentDirectory + @"\Resources\Media\Sound\SFX\button_mouse_over.wav", UriKind.Relative);

            this._buttonClickSound = new MediaPlayer
            {
                Volume = 0.1
            };

            this._buttonClickUri = new Uri(Environment.CurrentDirectory + @"\Resources\Media\Sound\SFX\button_click.wav", UriKind.Relative);

            this._backgroundMusic = new MediaPlayer
            {
                Volume = 0.25
            };

            this._backgroundMusic.MediaEnded += new EventHandler(BackgroundMusic_MediaEnded);

            this._backgroundMusic.Open(new Uri(Environment.CurrentDirectory + @"\Resources\Media\Sound\Music\background_music.wav", UriKind.Relative));

            this.InitializeComponent();

            this.backgroundVideo.Source = new Uri(Environment.CurrentDirectory + @"\Resources\Media\Video\background_video.mp4", UriKind.Relative);
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

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            this._backgroundMusic.IsMuted = true;
            ((MainWindowViewModel)this.DataContext).IsBackgroundMusicMuted = true;
            ((MainWindowViewModel)this.DataContext).IsBackgroundMusicUnmuted = false;
        }

        private void UnmuteButton_Click(object sender, RoutedEventArgs e)
        {
            this._backgroundMusic.IsMuted = false;
            ((MainWindowViewModel)this.DataContext).IsBackgroundMusicMuted = false;
            ((MainWindowViewModel)this.DataContext).IsBackgroundMusicUnmuted = true;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new();
            settingsWindow.DataContext = ((MainWindowViewModel)this.DataContext).Settings;
            bool? result = settingsWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                this._backgroundMusic.IsMuted = !((MainWindowViewModel)this.DataContext).Settings.MusicEnabled;
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((MainWindowViewModel)this.DataContext).Settings.SfxEnabled)
            {
                this._buttonMouseOverSound.Open(this._buttonMouseOverUri);
                this._buttonMouseOverSound.Play();
            }
        }

        private void BackgroundMusic_MediaEnded(object? sender, EventArgs e)
        {
            this._backgroundMusic.Position = TimeSpan.Zero;
            this._backgroundMusic.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((MainWindowViewModel)this.DataContext).Settings.SfxEnabled)
            {
                this._buttonClickSound.Open(this._buttonClickUri);
                this._buttonClickSound.Play();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._backgroundMusic.IsMuted = !((MainWindowViewModel)this.DataContext).Settings.MusicEnabled;
            this._backgroundMusic.Play();
        }

        private void BackgroundVideo_Loaded(object sender, RoutedEventArgs e)
        {
            this.backgroundVideo.Play();
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.backgroundVideo.Position = TimeSpan.FromSeconds(0);
        }

        #endregion // UI Events
    }
}