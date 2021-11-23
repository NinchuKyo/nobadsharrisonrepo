using System.IO;

namespace AOERandomizer.Logging
{
    /// <summary>
    /// Helper utility for creating application logs.
    /// </summary>
    public static class LogFactory
    {
        /// <summary>
        /// Create a new logger instance to be saved to the given directory.
        /// </summary>
        /// <param name="folder">Path to save the log files.</param>
        /// <returns>The logger instance.</returns>
        public static ILog CreateLog(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // TODO: Should probably make different log types for each target...combine them for now..
            return new LogBase(folder);
        }
    }
}