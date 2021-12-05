using AOERandomizer.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;

namespace AOERandomizer.Model
{
    /// <summary>
    /// Data model for the teams page.
    /// </summary>
    public class TeamsPageModel : ModelBase
    {
        #region Members

        private ObservableCollection<string> _players;
        private TeamConfig _teamConfig;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TeamsPageModel()
        {
            this._players = new();
            this._teamConfig = TeamConfig.TwoVsTwo;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the players list.
        /// </summary>
        public ObservableCollection<string> Players
        {
            get { return this._players; }
            set { this.SetProperty(ref this._players, value); }
        }

        /// <summary>
        /// Gets or sets the team layout configuration.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TeamConfig TeamLayout
        {
            get { return this._teamConfig; }
            set { this.SetProperty(ref this._teamConfig, value); }
        }

        #endregion // Properties
    }
}