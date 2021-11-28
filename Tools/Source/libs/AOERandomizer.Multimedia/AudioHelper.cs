using AOERandomizer.Logging;
using AOERandomizer.RandomGeneration;
using FroggoBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private const string SfxFolder = @"Resources\Media\Sound\SFX\";
        private const string MusicFolder = @"Resources\Media\Sound\Music\";

        private const string MouseOverSoundPath = $"{SfxFolder}button_mouse_over.wav";
        private const string ButtonClickSoundPath = $"{SfxFolder}button_click.wav";
        private const string ButtonWololoSoundPath = $"{SfxFolder}wololo.wav";

        #endregion // Constants

        #region Members

        private static readonly ILog? Log;

        private static readonly MediaPlayer ButtonMouseOverSound;
        private static readonly MediaPlayer ButtonClickSound;
        private static readonly MediaPlayer ButtonWololoSound;
        private static readonly MediaPlayer BackgroundMusic;

        private static readonly Uri ButtonMouseOverUri;
        private static readonly Uri ButtonClickUri;
        private static readonly Uri ButtonWololoUri;

        private static readonly List<string> Songs;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default static constructor.
        /// </summary>
        static AudioHelper()
        {
            Log = FroggoApplication.ApplicationLog;

            using (Log.ProfileCtx(LOG_CTX, "Initializing audio helper"))
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

                ButtonWololoUri = new Uri(Path.Combine(Environment.CurrentDirectory, ButtonWololoSoundPath), UriKind.Relative);
                ButtonWololoSound = new MediaPlayer
                {
                    Volume = 0.5
                };

                BackgroundMusic = new MediaPlayer
                {
                    Volume = 0.25
                };

                try
                {
                    Songs = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, MusicFolder), "*.wav").ToList();
                    BackgroundMusic.MediaEnded += new EventHandler(BackgroundMusic_MediaEnded);

                    // Pick a random song to play first...
                    if (Songs.Any())
                    {
                        string song = Songs[MasterRNG.GetRandomNumberFrom(0, Songs.Count - 1)];
                        BackgroundMusic.Open(new Uri(song, UriKind.Relative));
                        Songs.Remove(song);
                    }
                    else
                    {
                        Log.WarningCtx(LOG_CTX, $"No songs were found in the music folder '{MusicFolder}'");
                    }
                }
                catch (Exception ex)
                {
                    Log.ExceptionCtx(LOG_CTX, $"Could not open background music", ex);
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
            if (MasterRNG.GetRandomNumberFrom(0, 1000) < 55)
            {
                TryPlaySound(ButtonWololoSound, ButtonWololoUri);
            }
            else
            {
                TryPlaySound(ButtonClickSound, ButtonClickUri);
            }
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
        public static void ToggleBackgroundMusicMute(bool mute)
        {
            if (mute && BackgroundMusic.CanPause)
            {
                Log.InfoCtx(LOG_CTX, $"Pausing background music");
                BackgroundMusic.Pause();
            }
            else
            {
                Log.InfoCtx(LOG_CTX, $"Resuming background music");
                BackgroundMusic.Play();
            }
        }

        /// <summary>
        /// Triggeres when the song playing in the background finishes playing.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private static void BackgroundMusic_MediaEnded(object? sender, EventArgs e)
        {
            Log.InfoCtx(LOG_CTX, $"Background music ended - picking next song");
            BackgroundMusic.Position = TimeSpan.Zero;

            // Pick a random song to play first...
            if (!Songs.Any())
            {
                Log.WarningCtx(LOG_CTX, $"Ran out of songs to loop through - reshuffling");

                try
                {
                    Songs.AddRange(Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, MusicFolder), "*.wav"));
                }
                catch (Exception ex)
                {
                    Log.ExceptionCtx(LOG_CTX, $"Could not open background music", ex);
                }

            }

            string song = Songs[MasterRNG.GetRandomNumberFrom(0, Songs.Count - 1)];
            BackgroundMusic.Open(new Uri(song, UriKind.Relative));
            Songs.Remove(song);

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
                Log.ExceptionCtx(LOG_CTX, $"Could not open sound effect '{uri.LocalPath}'", ex);
            }
        }

        #endregion // Methods
    }
}