using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AOERandomizer.Model
{
    /// <summary>
    /// Team results model.
    /// </summary>
    public class TeamResults : ModelBase
    {
        #region Members

        private ObservableCollection<TeamPlayer> _teams;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TeamResults()
        {
            this._teams = new();
            this._teams.CollectionChanged += TeamsCollectionChanged;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the teams.
        /// </summary>
        public ObservableCollection<TeamPlayer> Teams
        {
            get { return this._teams; }
            set { this.SetProperty(ref this._teams, value); }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Triggers when the teams collection is changed.
        /// </summary>
        /// <param name="sender">Object that fired the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TeamsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged(nameof(Teams));
        }

        #endregion // Methods
    }
}