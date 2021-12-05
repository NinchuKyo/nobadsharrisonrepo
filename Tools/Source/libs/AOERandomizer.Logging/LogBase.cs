using AOERandomizer.Logging.Enums;
using System;
using System.Globalization;
using System.IO;

namespace AOERandomizer.Logging
{
    /// <summary>
    /// Base log implementation.
    /// </summary>
    internal class LogBase : ILog
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.Logging.LogBase";
        private const string DateFormat = "yy/MM/dd hh:mm:ss tt";
        private const string TextExtension = ".txt";

        #endregion // Constants

        #region Members

        private readonly StreamWriter _textWriter;

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
            return new(this, context, message);
        }

        /// <inheritdoc />
        public void InfoCtx(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);

            WriteToConsole(timestamp, LogLevel.Info, ConsoleColor.Green, context, message);
            WriteToStream(this._textWriter, timestamp, "Info", context, message);
        }

        /// <inheritdoc />
        public void DebugCtx(string context, string message)
        {
#if DEBUG
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);

            WriteToConsole(timestamp, LogLevel.Debug, ConsoleColor.Magenta, context, message);
            WriteToStream(this._textWriter, timestamp, "Debug", context, message);
#endif
        }

        /// <inheritdoc />
        public void ProfileMsg(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);

            WriteToConsole(timestamp, LogLevel.Profile, ConsoleColor.Cyan, context, message);
            WriteToStream(this._textWriter, timestamp, "Prof", context, message);
        }

        /// <inheritdoc />
        public void WarningCtx(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);

            WriteToConsole(timestamp, LogLevel.Warning, ConsoleColor.Yellow, context, message);
            WriteToStream(this._textWriter, timestamp, "Warn", context, message);
        }

        /// <inheritdoc />
        public void ErrorCtx(string context, string message)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);

            WriteToConsole(timestamp, LogLevel.Error, ConsoleColor.Red, context, message);
            WriteToStream(this._textWriter, timestamp, "Error", context, message);
        }

        /// <inheritdoc />
        public void ExceptionCtx(string context, string message, Exception ex)
        {
            string timestamp = DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture);
            string exceptionMsg = $"{message}{Environment.NewLine}{Environment.NewLine}{ex}{Environment.NewLine}";

            WriteToConsole(timestamp, LogLevel.Exception, ConsoleColor.DarkRed, context, exceptionMsg);
            WriteToStream(this._textWriter, timestamp, "Exception", context, exceptionMsg);
        }

        /// <summary>
        /// Writes the given log message to the console.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="level">The log level.</param>
        /// <param name="colour">The console text colour for the log level.</param>
        /// <param name="context">The log context.</param>
        /// <param name="message">The log message.</param>
        private static void WriteToConsole(string timestamp, LogLevel level, ConsoleColor colour, string context, string message)
        {
            Console.Write($"{timestamp} [");

            Console.ForegroundColor = colour;

            switch (level)
            {
                case LogLevel.Debug:
                    Console.Write("Debug");
                    break;
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
        /// <param name="textWriter">The stream to write to.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="context">The log context.</param>
        /// <param name="message">The log message.</param>
        private static void WriteToStream(StreamWriter textWriter, string timestamp, string logLevel, string context, string message)
        {
            textWriter.WriteLine($"{timestamp} [{logLevel}]: Ctx: {context} - {message}");
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