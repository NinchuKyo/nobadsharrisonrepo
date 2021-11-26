using AOERandomizer.Model;

namespace AOERandomizer.Configuration
{
    /// <summary>
    /// Application data config definition.
    /// </summary>
    public class DataConfig : ModelBase
    {
        private TeamsPageModel _teamsPageData;

        public DataConfig()
        {
            this._teamsPageData = new TeamsPageModel();
        }

        public TeamsPageModel TeamsPageData
        {
            get { return this._teamsPageData; }
            set { this.SetProperty(ref this._teamsPageData, value); }
        }
    }
}