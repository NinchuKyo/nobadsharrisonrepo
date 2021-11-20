using FroggoBase.Logging.Interface;

namespace FroggoBase.Logging.Implementation
{
    /// <summary>
    /// Base implementation for an application log.
    /// 
    /// TODO: Give the ability for LogBase to target either a stream and/or a file for output.
    /// By default, logs should be:
    /// - 1 Log file per thread/process, per application
    /// - Placed in a centralized 'Logs' folder, with subfolders containing the application names
    /// -- Each session of logs should be placed in a timestamped folder (where the folder name contains the timestamp)
    /// </summary>
    public class LogBase : ILog
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LogBase()
        {
        }

        #endregion // Constructors
    }
}