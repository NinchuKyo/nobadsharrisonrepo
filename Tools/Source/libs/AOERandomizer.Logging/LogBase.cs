using AOERandomizer.Logging.Enums;
using System;
using System.Globalization;
using System.IO;

namespace AOERandomizer.Logging
{
    /// <summary>
    /// Base log implementation.
    /// </summary>
    public class LogBase : ILog
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.Logging.LogBase";

        private const string DateFormat = "yy/MM/dd hh:mm:ss tt";
        private const string TextExtension = ".txt";

        #endregion // Constants

        #region Members

        private StreamWriter _textWriter;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="path">Log file path.</param>
        public LogBase(string path)
        {
            this.LogPath = path;

            this._textWriter = File.AppendText(Path.Combine(this.LogPath, $"log{TextExtension}"));

            this.InfoCtx(LOG_CTX, "Log initialized");
        }

        #endregion // Constructors

        #region Properties

        /// <inheritdoc />
        public string LogPath { get; }

        #endregion // Properties

        #region Methods

        /// <inheritdoc />
        public LogProfile ProfileCtx(string context, string message)
        {
            return new LogProfile(this, context, message);
        }

        /// <inheritdoc />
        public void InfoCtx(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);
            this.WriteToConsole(timestamp, LogLevel.Info, ConsoleColor.Green, context, message);
            this.WriteToStream(timestamp, "Info", context, message);
        }

        /// <inheritdoc />
        public void ProfileMsg(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);
            this.WriteToConsole(timestamp, LogLevel.Profile, ConsoleColor.Cyan, context, message);
            this.WriteToStream(timestamp, "Prof", context, message);
        }

        /// <inheritdoc />
        public void WarningCtx(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);
            this.WriteToConsole(timestamp, LogLevel.Warning, ConsoleColor.Yellow, context, message);
            this.WriteToStream(timestamp, "Warn", context, message);
        }

        /// <inheritdoc />
        public void ErrorCtx(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);
            this.WriteToConsole(timestamp, LogLevel.Error, ConsoleColor.Red, context, message);
            this.WriteToStream(timestamp, "Error", context, message);
        }

        /// <inheritdoc />
        public void ExceptionCtx(string context, string message, Exception ex)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);
            string exceptionMsg = $"{message}{Environment.NewLine}{Environment.NewLine}{ex}{Environment.NewLine}";
            this.WriteToConsole(timestamp, LogLevel.Exception, ConsoleColor.DarkRed, context, exceptionMsg);
            this.WriteToStream(timestamp, "Exception", context, exceptionMsg);
        }

        /// <summary>
        /// Writes the given log message to the console.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="level">The log level.</param>
        /// <param name="colour">The console text colour for the log level.</param>
        /// <param name="context">The log context.</param>
        /// <param name="message">The log message.</param>
        private void WriteToConsole(string timestamp, LogLevel level, ConsoleColor colour, string context, string message)
        {
            Console.Write($"{timestamp} [");

            Console.ForegroundColor = colour;

            switch (level)
            {
                case LogLevel.Info:
                    Console.Write("Info");
                    break;
                case LogLevel.Profile:
                    Console.Write("Prof");
                    break;
                case LogLevel.Warning:
                    Console.Write("Warn");
                    break;
                case LogLevel.Error:
                    Console.Write("Error");
                    break;
                case LogLevel.Exception:
                    Console.Write("Exception");
                    break;
                default:
                    break;
            }

            Console.ResetColor();

            if (level == LogLevel.Warning || level == LogLevel.Error || level == LogLevel.Exception)
            {
                Console.Write($"]: Ctx: {context} - ");
                Console.ForegroundColor = colour;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"]: {message}");
            }
        }

        /// <summary>
        /// Writes the given log message to the given stream.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="context">The log context.</param>
        /// <param name="message">The log message.</param>
        private void WriteToStream(string timestamp, string logLevel, string context, string message)
        {
            this._textWriter.WriteLine($"{timestamp} [{logLevel}]: Ctx: {context} - {message}");
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.InfoCtx(LOG_CTX, "Log disposing");

            this._textWriter.Flush();
            this._textWriter.Close();
            this._textWriter.Dispose();
        }

        #endregion // Methods
    }
}