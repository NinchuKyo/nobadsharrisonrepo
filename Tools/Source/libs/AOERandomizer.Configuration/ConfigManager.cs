using AOERandomizer.Logging;
using FroggoBase;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AOERandomizer.Configuration
{
    /// <summary>
    /// Configuration manager for AOERandomizer.
    /// </summary>
    public static class ConfigManager
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.Configuration.ConfigManager";

        private const string AppSettingsJsonPath = "appsettings.json";
        private const string AppDataJsonPath = "appdata.json";

        #endregion // Constants

        #region Members

        private static readonly ILog? Log;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default static constructor.
        /// </summary>
        static ConfigManager()
        {
            Log = FroggoApplication.ApplicationLog;
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Saves the given application settings config to disk (json).
        /// </summary>
        /// <param name="toSave">The configuration to save.</param>
        public static void SaveSettingsConfig(AppConfig toSave)
        {
            Log.InfoCtx(LOG_CTX, $"Saving application settings to '{AppSettingsJsonPath}'");

            try
            {
                File.WriteAllText(AppSettingsJsonPath, JsonConvert.SerializeObject(toSave, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Log.ExceptionCtx(LOG_CTX, $"Failed to save application settings to {AppSettingsJsonPath}'", ex);
            }

            Log.InfoCtx(LOG_CTX, $"Saved application settings to '{AppSettingsJsonPath}'");
        }

        /// <summary>
        /// Saves the given appllication data config to disk (json).
        /// </summary>
        /// <param name="toSave">The configuration to save.</param>
        public static void SaveDataConfig(DataConfig toSave)
        {
            Log.InfoCtx(LOG_CTX, $"Saving application data to '{AppDataJsonPath}'");

            try
            {
                File.WriteAllText(AppDataJsonPath, JsonConvert.SerializeObject(toSave ?? new DataConfig(), Formatting.Indented));
            }
            catch (Exception ex)
            {
                Log.ExceptionCtx(LOG_CTX, $"Failed to save application data to {AppDataJsonPath}'", ex);
            }

            Log.InfoCtx(LOG_CTX, $"Saved application data to '{AppDataJsonPath}'");
        }

        /// <summary>
        /// Loads the application settings config from disk (json).
        /// </summary>
        /// <returns>The application settings config.</returns>
        public static AppConfig LoadSettingsConfig()
        {
            Log.InfoCtx(LOG_CTX, $"Loading application settings from '{AppSettingsJsonPath}'");
            AppConfig? settings = null;

            try
            {
                settings = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(AppSettingsJsonPath));
                if (settings == null)
                {
                    Log.WarningCtx(LOG_CTX, "Settings were loaded but not set - loading default settings");
                    settings = new AppConfig();
                }

                Log.InfoCtx(LOG_CTX, $"Application settings loaded");
            }
            catch (Exception ex)
            {
                Log.ExceptionCtx(LOG_CTX, $"Failed to load application settings from {AppSettingsJsonPath}', loading default settings", ex);
                settings = new AppConfig();
            }

            return settings;
        }

        /// <summary>
        /// Loads the application data config from disk (json).
        /// </summary>
        /// <returns>The application data config.</returns>
        public static DataConfig LoadDataConfig()
        {
            Log.InfoCtx(LOG_CTX, $"Loading application data from '{AppSettingsJsonPath}'");
            DataConfig? settings = null;

            try
            {
                settings = JsonConvert.DeserializeObject<DataConfig>(File.ReadAllText(AppDataJsonPath));
                if (settings == null)
                {
                    Log.WarningCtx(LOG_CTX, "Data was loaded but not set - loading default data");
                    settings = new DataConfig();
                }

                Log.InfoCtx(LOG_CTX, $"Application data loaded");
            }
            catch (Exception ex)
            {
                Log.ExceptionCtx(LOG_CTX, $"Failed to load application data from {AppSettingsJsonPath}', loading default data", ex);
                settings = new DataConfig();
            }

            return settings;
        }

        #endregion // Methods
    }
}