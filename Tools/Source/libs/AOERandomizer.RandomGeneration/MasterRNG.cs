using AOERandomizer.Logging;
using FroggoBase;
using System;
using System.Security.Cryptography;

namespace AOERandomizer.RandomGeneration
{
    /// <summary>
    /// Wrapper for System.Security.Cryptography random number generator
    /// (this is surprisingly much easier to use now, since the CryptoServiceProvider crap was made obsolete - now we have nice static methods to use).
    /// </summary>
    public static class MasterRNG
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.Random.MasterRNG";

        #endregion // Constants

        #region Members

        private static readonly ILog Log = FroggoApplication.ApplicationLog;

        #endregion // Members

        #region Methods

        /// <summary>
        /// Restricted to positive integers.
        /// </summary>
        /// <param name="first">Inclusive lower bound.</param>
        /// <param name="toInclusive">Inclusive upper bound.</param>
        /// <returns>The random integer.</returns>
        public static int GetRandomNumberFrom(int fromInclusive = 0, int toInclusive = Int32.MaxValue - 1)
        {
            int result;

            // Hacky - but whatever: avoid overflows and numbers less than 0, etc.
            if (fromInclusive < 0 || fromInclusive >= Int32.MaxValue || toInclusive < 0 || toInclusive >= Int32.MaxValue)
            {
                result = RandomNumberGenerator.GetInt32(0, Int32.MaxValue);
            }
            else
            {
                result = RandomNumberGenerator.GetInt32(fromInclusive, toInclusive + 1);
            }

            Log.DebugCtx(LOG_CTX, $"Generated random number between {fromInclusive} and {toInclusive} = {result}");

            return result;
        }

        /// <summary>
        /// Gets a random double between 0 (inclusive) and 1 (exclusive).
        /// </summary>
        /// <returns>Random doble between [0, 1).</returns>
        public static double GetRandomDouble()
        {
            return GetRandomDoubleFromZeroTo();
        }

        /// <summary>
        /// Gets a random double between 0 (inclusive) and the given maximum (exclusive).
        /// </summary>
        /// <param name="exclusive">The max (exclusive).</param>
        /// <returns>Random double between [0, exclusive).</returns>
        public static double GetRandomDoubleFromZeroTo(double exclusive = 1)
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(8);
            ulong shifted = BitConverter.ToUInt64(bytes, 0) >> 11;
            double val = shifted / (double)(1UL << 53);

            return val * exclusive;
        }

        #endregion // Methods
    }
}