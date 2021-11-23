using Newtonsoft.Json;
using System.IO;

namespace AOERandomizer.Configuration
{
    /// <summary>
    /// Configuration manager for AOERandomizer.
    /// </summary>
    public static class ConfigManager
    {
        #region Constants

        private const string AppSettingsJsonPath = "appsettings.json";
        private const string AppDataJsonPath = "appdata.json";

        #endregion // Constants

        /// <summary>
        /// Saves the given application settings config to disk (json).
        /// </summary>
        /// <param name="toSave">The configuration to save.</param>
        public static void SaveSettingsConfig(AppConfig toSave)
        {
            File.WriteAllText(AppSettingsJsonPath, JsonConvert.SerializeObject(toSave ?? new AppConfig(), Formatting.Indented));
        }

        /// <summary>
        /// Saves the given appllication data config to disk (json).
        /// </summary>
        /// <param name="toSave">The configuration to save.</param>
        public static void SaveDataConfig(DataConfig toSave)
        {
            File.WriteAllText(AppDataJsonPath, JsonConvert.SerializeObject(toSave ?? new DataConfig(), Formatting.Indented));
        }

        /// <summary>
        /// Loads the application settings config from disk (json).
        /// </summary>
        /// <returns>The application settings config.</returns>
        public static AppConfig LoadSettingsConfig()
        {
            return JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(AppSettingsJsonPath)) ?? new AppConfig();
        }

        /// <summary>
        /// Loads the application data config from disk (json).
        /// </summary>
        /// <returns>The application data config.</returns>
        public static DataConfig LoadDataConfig()
        {
            return JsonConvert.DeserializeObject<DataConfig>(File.ReadAllText(AppDataJsonPath)) ?? new DataConfig();
        }
    }
}