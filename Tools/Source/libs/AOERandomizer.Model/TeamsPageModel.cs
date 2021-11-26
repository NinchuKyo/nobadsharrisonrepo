using AOERandomizer.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;

namespace AOERandomizer.Model
{
    public class TeamsPageModel : ModelBase
    {
        private ObservableCollection<string> _players;
        private TeamConfig _teamConfig;

        public TeamsPageModel()
        {
            this._players = new ObservableCollection<string>();
            this._teamConfig = TeamConfig.TwoVsTwo;
        }

        public ObservableCollection<string> Players
        {
            get { return this._players; }
            set { this.SetProperty(ref this._players, value); }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TeamConfig TeamLayout
        {
            get { return this._teamConfig; }
            set { this.SetProperty(ref this._teamConfig, value); }
        }
    }
}