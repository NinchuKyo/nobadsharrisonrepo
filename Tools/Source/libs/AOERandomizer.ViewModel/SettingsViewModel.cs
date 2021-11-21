using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Commands;
using System.Windows.Forms;
using System.Windows.Input;

namespace AOERandomizer.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Members

        private string? _pathToLogs;
        private string? _pathToData;
        private bool _musicEnabled;
        private bool _sfxEnabled;

        private readonly string? _originalPathToLogs;
        private readonly string? _originalPathToData;
        private readonly bool _originalMusicEnabled;
        private readonly bool _originalSfxEnabled;

        #endregion // Members

        #region Commands

        public ICommand BrowseContentPathCommand { get; set; }
        public ICommand BrowseGrpDPathCommand { get; set; }

        #endregion // Commands

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pathToLogs">Path to logs.</param>
        /// <param name="pathToData">Path to data.</param>
        public SettingsViewModel(string? pathToLogs = "", string? pathToData = "", bool musicEnabled = false, bool sfxEnabled = false)
        {
            this._pathToLogs = pathToLogs;
            this._pathToData = pathToData;
            this._musicEnabled = musicEnabled;
            this._sfxEnabled = sfxEnabled;
            this._originalPathToLogs = pathToLogs;
            this._originalPathToData = pathToData;
            this._originalMusicEnabled = musicEnabled;
            this._originalSfxEnabled = sfxEnabled;

            this.BrowseContentPathCommand = new RelayCommand(CanExecuteBrowseContentPath, ExecuteBrowseContentPath);
            this.BrowseGrpDPathCommand = new RelayCommand(CanExecuteBrowseGrpDPath, ExecuteBrowseGrpDPath);
        }

        private void ExecuteBrowseGrpDPath(object? obj)
        {
            OpenFileDialog browserDlg = new();
            if (DialogResult.OK == browserDlg.ShowDialog())
            {
                this.PathToData = browserDlg.FileName;
            }
        }

        private bool CanExecuteBrowseGrpDPath(object? obj)
        {
            return true;
        }

        private void ExecuteBrowseContentPath(object? obj)
        {
            FolderBrowserDialog browserDlg = new();
            if (DialogResult.OK == browserDlg.ShowDialog())
            {
                this.PathToLogs = browserDlg.SelectedPath;
            }
        }

        private bool CanExecuteBrowseContentPath(object? obj)
        {
            return true;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the path to content data.
        /// </summary>
        public string? PathToLogs
        {
            get => this._pathToLogs;
            set => this.SetProperty(ref this._pathToLogs, value);
        }

        /// <summary>
        /// Gets or sets the path to grp_d file.
        /// </summary>
        public string? PathToData
        {
            get => this._pathToData;
            set => this.SetProperty(ref this._pathToData, value);
        }

        public bool MusicEnabled
        {
            get => this._musicEnabled;
            set => this.SetProperty(ref this._musicEnabled, value);
        }

        public bool SfxEnabled
        {
            get => this._sfxEnabled;
            set => this.SetProperty(ref this._sfxEnabled, value);
        }

        #endregion // Properties

        #region Methods

        public void ResetLogsPath()
        {
            this.PathToLogs = this._originalPathToLogs;
        }

        public void ResetDataPath()
        {
            this.PathToData = this._originalPathToData;
        }

        public void ResetMusicEnabled()
        {
            this.MusicEnabled = this._originalMusicEnabled;
        }

        public void ResetSfxEnabled()
        {
            this.SfxEnabled = this._originalSfxEnabled;
        }

        #endregion // Methods
    }
}