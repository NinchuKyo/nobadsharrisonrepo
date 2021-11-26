using System.Collections.ObjectModel;

namespace AOERandomizer.Model
{
    public class TeamResults : ModelBase
    {
        private ObservableCollection<TeamPlayer> _teams;

        public TeamResults()
        {
            this._teams = new ObservableCollection<TeamPlayer>();
            this._teams.CollectionChanged += TeamsCollectionChanged;
        }

        private void TeamsCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged(nameof(Teams));
        }

        public ObservableCollection<TeamPlayer> Teams
        {
            get { return this._teams; }
            set { this.SetProperty(ref this._teams, value); }
        }
    }
}