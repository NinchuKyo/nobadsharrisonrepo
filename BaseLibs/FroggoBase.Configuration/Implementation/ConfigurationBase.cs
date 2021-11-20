using FroggoBase.Configuration.Interface;

namespace FroggoBase.Configuration.Implementation
{
    /// <summary>
    /// Base implementation for an application configuration.
    /// 
    /// TODO: Probably will want to make this load configuration from a centralized path containing configs for all
    /// our apps.  ConfigurationBase should use the name of the app to find its associated configuration (lookup).
    /// </summary>
    public class ConfigurationBase : IConfiguration
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfigurationBase()
        {
        }

        #endregion // Constructors
    }
}