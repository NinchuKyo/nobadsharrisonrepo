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
        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static FroggoApplication()
        {
            string? processPath = Environment.ProcessPath;
            if (processPath == null)
            {
                throw new ApplicationException("Environment cannot determine process path (this should never happen).");
            }

            string? processDirectory = Path.GetDirectoryName(processPath);
            if (processDirectory == null)
            {
                throw new ApplicationException("Cannot determine process directory (this should never happen).");
            }

            string logPath = Path.Combine(processDirectory, "Logs");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-");
            string app = typeof(FroggoApplication).Name;
            string filename = $"{timestamp}_{app}";

            ApplicationLog = LogFactory.CreateLog(Path.Combine(logPath, filename));
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets the application log.
        /// </summary>
        public static ILog ApplicationLog { get; }

        #endregion // Properties
    }
}