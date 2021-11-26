using System;

namespace AOERandomizer.Logging
{
    /// <summary>
    /// Interface for a log.
    /// </summary>
    public interface ILog : IDisposable
    {
        /// <summary>
        /// Gets the log filepath.
        /// </summary>
        string LogPath { get; }

        /// <summary>
        /// Returns a disposable instance of LogProfile, used for performance timing.
        /// </summary>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        /// <returns>Disposable LogProfile instance.</returns>
        LogProfile ProfileCtx(string context, string message);

        /// <summary>
        /// Logs an info message with context.
        /// </summary>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        void InfoCtx(string context, string message);

        /// <summary>
        /// Logs a debug message with context.
        /// </summary>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        void DebugCtx(string context, string message);

        /// <summary>
        /// Logs a profile message with context.
        /// </summary>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        void ProfileMsg(string context, string message);

        /// <summary>
        /// Logs a warning message with context.
        /// </summary>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        void WarningCtx(string context, string message);

        /// <summary>
        /// Logs an error message with context.
        /// </summary>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        void ErrorCtx(string context, string message);

        /// <summary>
        /// Logs an exception message with context.
        /// </summary>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        /// <param name="ex">The exception to log.</param>
        void ExceptionCtx(string context, string message, Exception ex);
    }
}