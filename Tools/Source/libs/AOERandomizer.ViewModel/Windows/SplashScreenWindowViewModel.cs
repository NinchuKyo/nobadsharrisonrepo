using AOERandomizer.Logging;
using AOERandomizer.ViewModel.Base;
using FroggoBase;

namespace AOERandomizer.ViewModel.Windows
{
    /// <summary>
    /// Viewmodel for the splash screen window.
    /// </summary>
    public class SplashScreenWindowViewModel : ViewModelBase
    {
        #region Constants

        private const string LOG_CTX = "AOERandomizer.ViewModel.Windows.SplashScreenWindowViewModel";

        #endregion // Constants

        #region Members

        private string _loadingLabel;
        private readonly ILog? _log;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Constructs a new instance of a splash screen view model.
        /// </summary>
        /// <param name="initialText">(optional) The initial text to display in the splash screen label.</param>
        public SplashScreenWindowViewModel(string initialText = "")
        {
            this._log = FroggoApplication.ApplicationLog;
            this._loadingLabel = initialText;

            this._log.InfoCtx(LOG_CTX, "SplashScreenWindowViewModel created.");
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the label shown at the bottom of the splash window.
        /// </summary>
        public string LoadingLabel
        {
            get => this._loadingLabel;
            set => this.SetProperty(ref this._loadingLabel, value);
        }

        #endregion // Properties
    }
}