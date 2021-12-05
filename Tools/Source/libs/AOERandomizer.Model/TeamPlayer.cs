using System;

namespace AOERandomizer.Model
{
    /// <summary>
    /// Team player model - contains a player name and team number.
    /// </summary>
    public class TeamPlayer : ModelBase
    {
        #region Members

        private int _teamNumber;
        private string _playerName;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TeamPlayer()
        {
            this._teamNumber = 1;
            this._playerName = String.Empty;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the team number.
        /// </summary>
        public int TeamNumber
        {
            get { return this._teamNumber; }
            set { this.SetProperty(ref this._teamNumber, value); }
        }

        /// <summary>
        /// Gets or sets the player name.
        /// </summary>
        public string PlayerName
        {
            get { return this._playerName; }
            set { this.SetProperty(ref this._playerName, value); }
        }

        #endregion // Properties
    }
}