using AOERandomizer.Configuration;
using AOERandomizer.Model.Enums;
using AOERandomizer.ViewModel.Commands;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Extensions;
using AOERandomizer.ViewModel.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AOERandomizer.ViewModel.Pages
{
    /// <summary>
    /// Viewmodel for the teams page.
    /// </summary>
    public class TeamsPageViewModel : PageBaseViewModel
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Pages.TeamsPageViewModel";

        #endregion // Constants

        #region Members

        private string _playerNameText;
        private bool _isSpinning;

        #endregion // Members

        #region Commands

        /// <summary>
        /// Gets or sets the add player command.
        /// </summary>
        public ICommand AddPlayerCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove player command.
        /// </summary>
        public ICommand RemovePlayerCommand { get; set; }

        /// <summary>
        /// Gets or sets the selected team config changed command.
        /// </summary>
        public ICommand SelectedTeamConfigChangedCommand { get; set; }


        #endregion // Commands

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="settingsConfig">Application settings config.</param>
        /// <param name="navVm">Viewmodel in charge of navigation.</param>
        /// <param name="dataConfig">Application data config.</param>
        public TeamsPageViewModel(AppConfig settingsConfig, NavigationViewModel navVm, DataConfig dataConfig)
            : base(settingsConfig, navVm, dataConfig)
        {
            this._playerNameText = String.Empty;
            this._isSpinning = false;

            this.AddPlayerCommand = new RelayCommand(CanExecuteAddPlayerCommand, ExecuteAddPlayerCommand);
            this.RemovePlayerCommand = new RelayCommand(CanExecuteRemovePlayerCommand, ExecuteRemovePlayerCommand);
            this.SelectedTeamConfigChangedCommand = new RelayCommand(CanExecuteSelectedTeamConfigChangedCommand, ExecuteSelectedTeamConfigChangedCommand);

            this._log.InfoCtx(LOG_CTX, "TeamsPageViewModel created");
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the players list.
        /// </summary>
        public ObservableCollection<string> Players => this.DataConfig.TeamsPageData.Players;

        /// <summary>
        /// Gets or sets the player name text.
        /// </summary>
        public string PlayerNameText
        {
            get { return this._playerNameText; }
            set { this.SetProperty(ref this._playerNameText, value); }
        }

        public string SelectedTeamConfig
        {
            get { return this.DataConfig.TeamsPageData.TeamLayout.GetDisplayName(); }
        }

        #endregion // Properties

        #region Methods

        /// <inheritdoc />
        public override void Load()
        {
            using (this._log.ProfileCtx(LOG_CTX, "Loading TeamsPageViewModel"))
            {
                // Add home button
                string iconPath = $"{ButtonIconsPath}/{EPageName.Home}.png";
                this._pageButtons.Add(new PageButtonViewModel(EPageName.Home, iconPath));

                // Add civs button
                iconPath = $"{ButtonIconsPath}/{EPageName.Civs}.png";
                this._pageButtons.Add(new PageButtonViewModel(EPageName.Civs, iconPath));
            }
        }

        /// <inheritdoc />
        protected override void ExecuteNavigateCommand(object? param)
        {
            if (param is PageButtonViewModel button)
            {
                this._log.InfoCtx(LOG_CTX, $"Navigating to {button.PageName} page");

                switch (button.PageName)
                {
                    case EPageName.Home:
                        HomePageViewModel homePageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                        homePageVm.Load();
                        this._navVm.SelectedVm = homePageVm;
                        break;
                    case EPageName.Civs:
                        CivsPageViewModel civsPageVm = new(this.SettingsConfig, this._navVm, this.DataConfig);
                        civsPageVm.Load();
                        this._navVm.SelectedVm = civsPageVm;
                        break;
                    default:
                        this._log.WarningCtx(LOG_CTX, $"Page {button.PageName} not supported for navigation");
                        break;
                }
            }
        }

        /// <summary>
        /// Returns a flag indicating whether the add player command can be executed.
        /// </summary>
        /// <param name="param">Command parameter.</param>
        /// <returns>Flag indicating whether the add player command can execute.</returns>
        private bool CanExecuteAddPlayerCommand(object? param)
        {
            if (this.Players.Count >= 8 || String.IsNullOrWhiteSpace(this.PlayerNameText))
            {
                return false;
            }

            foreach (string player in this.Players)
            {
                if (player.ToLower().Equals(this.PlayerNameText?.ToLower()))
                {
                    return false;

                }
            }

            return true;
        }

        /// <summary>
        /// Executes the add player command.
        /// </summary>
        /// <param name="param">Command parameter.</param>
        private void ExecuteAddPlayerCommand(object? param)
        {
            if (this.Players.Count < 8 && !String.IsNullOrWhiteSpace(this.PlayerNameText))
            {
                foreach (string player in this.Players)
                {
                    if (player.ToLower().Equals(this.PlayerNameText.ToLower()))
                    {
                        return;
                    }
                }

                this.DataConfig.TeamsPageData.Players.Add(this.PlayerNameText);
                this.PlayerNameText = String.Empty;
            }
        }

        /// <summary>
        /// Returns a flag indicating whether the remove player command can be executed.
        /// </summary>
        /// <param name="param">Command parameter.</param>
        /// <returns>Flag indicating whether the remove player command can execute.</returns>
        private bool CanExecuteRemovePlayerCommand(object? param)
        {
            return this.Players.Count > 0;
        }

        /// <summary>
        /// Executes the remove player command.
        /// </summary>
        /// <param name="param">Command parameter.</param>
        private void ExecuteRemovePlayerCommand(object? param)
        {
            if (this.Players.Count > 0 && param is string toRemove)
            {
                this.Players.Remove(toRemove);
            }
        }

        private bool CanExecuteSelectedTeamConfigChangedCommand(object? obj)
        {
            return !this._isSpinning;
        }

        private void ExecuteSelectedTeamConfigChangedCommand(object? obj)
        {
            if (!this._isSpinning)
            {
                if (obj is string teamConfigString)
                {
                    foreach (TeamConfig config in Enum.GetValues(typeof(TeamConfig)))
                    {
                        if (config.GetDisplayName().Equals(teamConfigString))
                        {
                            this.DataConfig.TeamsPageData.TeamLayout = config;
                        }
                    }
                }
            }
        }

        #endregion // Methods
    }
}