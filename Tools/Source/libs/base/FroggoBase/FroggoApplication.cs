using AOERandomizer.Logging;
using System;
using System.IO;
using System.Windows;

namespace FroggoBase
{
    /// <summary>
    /// Base application class.
    /// </summary>
    public class FroggoApplication : Application
    {
        #region Constants

        private static readonly string LogPath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), "Logs");

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static FroggoApplication()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-");
            string app = typeof(FroggoApplication).Name;
            string filename = $"{timestamp}_{app}";

            ApplicationLog = LogFactory.CreateLog(Path.Combine(LogPath, filename));
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets the application log.
        /// </summary>
        public static ILog? ApplicationLog { get; }

        #endregion // Properties
    }
}