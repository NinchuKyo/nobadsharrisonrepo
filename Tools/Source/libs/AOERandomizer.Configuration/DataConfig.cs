using AOERandomizer.Model;

namespace AOERandomizer.Configuration
{
    /// <summary>
    /// Application data config definition.
    /// </summary>
    public class DataConfig : ModelBase
    {
        #region Members

        private TeamsPageModel _teamsPageData;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataConfig()
        {
            this._teamsPageData = new();
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Gets or sets the data for the teams page.
        /// </summary>
        public TeamsPageModel TeamsPageData
        {
            get { return this._teamsPageData; }
            set { this.SetProperty(ref this._teamsPageData, value); }
        }

        #endregion // Methods
    }
}