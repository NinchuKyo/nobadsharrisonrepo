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

        private static readonly ILog? Log = FroggoApplication.ApplicationLog;

        #endregion // Members

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

            result = RandomNumberGenerator.GetInt32(fromInclusive, toInclusive + 1);

            Log.DebugCtx(LOG_CTX, $"Generated random number between {fromInclusive} and {toInclusive} = {result}");

            return result;
        }
    }
}