using System;

namespace AOERandomizer.Model
{
    public class TeamPlayer : ModelBase
    {
        private int _teamNumber;
        private string _playerName;

        public TeamPlayer()
        {
            this._teamNumber = 1;
            this._playerName = String.Empty;
        }

        public int TeamNumber
        {
            get { return this._teamNumber; }
            set { this.SetProperty(ref this._teamNumber, value); }
        }

        public string PlayerName
        {
            get { return this._playerName; }
            set { this.SetProperty(ref this._playerName, value); }
        }
    }
}