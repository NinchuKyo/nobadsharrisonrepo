using System;
using System.Diagnostics;
using System.Text;

namespace AOERandomizer.Logging
{
    /// <summary>
    /// Profiler for measuring code performance.
    /// </summary>
    public class LogProfile : IDisposable
    {
        #region Members

        private readonly ILog _log;
        private readonly string _context;
        private readonly string _message;
        private readonly Stopwatch _stopwatch;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="log">Logger.</param>
        /// <param name="context">Log context.</param>
        /// <param name="message">Log message.</param>
        internal LogProfile(ILog log, string context, string message)
        {
            this._log = log;
            this._context = context;
            this._message = message;

            this._log.ProfileMsg(this._context, $"'{this._message}'");
            this._stopwatch = Stopwatch.StartNew();
        }

        #endregion // Constructors

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            this._stopwatch.Stop();

            string timeString = GetTimeString(this._stopwatch.Elapsed);
            this._log.ProfileMsg(this._context, $"'{this._message}' took {timeString}");

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets a human-readable time string in the simplest form.
        /// </summary>
        /// <param name="timespan">The timespan to convert.</param>
        /// <returns>The human-readable time string.</returns>
        private static string GetTimeString(TimeSpan timespan)
        {
            StringBuilder builder = new();

            if (timespan.Hours > 0)
            {
                builder.Append($"{timespan.Hours} h ");
            }

            if (timespan.Minutes > 0)
            {
                builder.Append($"{timespan.Minutes} m ");
            }

            if (timespan.Seconds > 0)
            {
                double time = (double)timespan.Seconds + ((double)timespan.Milliseconds / 1000.0);
                builder.Append($"{time:0.##} s ");
            }

            if (builder.Length == 0)
            {
                builder.Append($"{timespan.TotalMilliseconds:0.##} ms ");
            }

            return builder.ToString().Trim();
        }

        #endregion // Methods
    }
}