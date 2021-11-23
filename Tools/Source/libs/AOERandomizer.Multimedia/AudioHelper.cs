using FroggoBase;
using System;
using System.IO;
using System.Windows.Media;

namespace AOERandomizer.Multimedia
{
    /// <summary>
    /// Audio helper utility.
    /// </summary>
    public static class AudioHelper
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.Multimedia.AudioHelper";

        private const string MouseOverSoundPath = @"Resources\Media\Sound\SFX\button_mouse_over.wav";
        private const string ButtonClickSoundPath = @"Resources\Media\Sound\SFX\button_click.wav";
        private const string BackgroundMusicPath = @"Resources\Media\Sound\Music\background_music.wav";

        #endregion // Constants

        #region Members

        private static readonly MediaPlayer ButtonMouseOverSound;
        private static readonly MediaPlayer ButtonClickSound;
        private static readonly MediaPlayer BackgroundMusic;

        private static readonly Uri ButtonMouseOverUri;
        private static readonly Uri ButtonClickUri;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default static constructor.
        /// </summary>
        static AudioHelper()
        {
            using (FroggoApplication.ApplicationLog.ProfileCtx(LOG_CTX, "Initializing audio helper"))
            {
                ButtonMouseOverUri = new Uri(Path.Combine(Environment.CurrentDirectory, MouseOverSoundPath), UriKind.Relative);
                ButtonMouseOverSound = new MediaPlayer
                {
                    Volume = 0.1
                };

                ButtonClickUri = new Uri(Path.Combine(Environment.CurrentDirectory, ButtonClickSoundPath), UriKind.Relative);
                ButtonClickSound = new MediaPlayer
                {
                    Volume = 0.1
                };

                BackgroundMusic = new MediaPlayer
                {
                    Volume = 0.25
                };

                try
                {
                    BackgroundMusic.MediaEnded += new EventHandler(BackgroundMusic_MediaEnded);
                    BackgroundMusic.Open(new Uri(Path.Combine(Environment.CurrentDirectory, BackgroundMusicPath), UriKind.Relative));
                }
                catch (Exception ex)
                {
                    FroggoApplication.ApplicationLog.ExceptionCtx(LOG_CTX, $"Could not open background music", ex);
                }
            }
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Plays the button hover sound.
        /// </summary>
        public static void PlayButtonHoverSound()
        {
            TryPlaySound(ButtonMouseOverSound, ButtonMouseOverUri);
        }

        /// <summary>
        /// Plays the button click sound.
        /// </summary>
        public static void PlayButtonClickSound()
        {
            TryPlaySound(ButtonClickSound, ButtonClickUri);
        }

        /// <summary>
        /// Plays the background music.
        /// </summary>
        public static void PlayBackgroundMusic()
        {
            BackgroundMusic.Play();
        }

        /// <summary>
        /// Mutes/unmutes the background music.
        /// </summary>
        /// <param name="mute"></param>
        public static void SetIsBackgroundMusicMuted(bool mute)
        {
            string muteMsg = mute ? "Muting" : "Unmuting";
            FroggoApplication.ApplicationLog.InfoCtx(LOG_CTX, $"{muteMsg} background music");

            BackgroundMusic.IsMuted = mute;
        }

        /// <summary>
        /// Triggeres when the song playing in the background finishes playing.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private static void BackgroundMusic_MediaEnded(object? sender, EventArgs e)
        {
            FroggoApplication.ApplicationLog.InfoCtx(LOG_CTX, $"Background music ended - restarting");
            BackgroundMusic.Position = TimeSpan.Zero;
            BackgroundMusic.Play();
        }

        /// <summary>
        /// Attempts to play the given sound.
        /// </summary>
        /// <param name="player">The media player for the sound.</param>
        /// <param name="uri">The sound uri.</param>
        private static void TryPlaySound(MediaPlayer player, Uri uri)
        {
            try
            {
                player.Open(uri);
                player.Play();
            }
            catch (Exception ex)
            {
                FroggoApplication.ApplicationLog.ExceptionCtx(LOG_CTX, $"Could not open sound effect '{uri.LocalPath}'", ex);
            }
        }

        #endregion // Methods
    }
}