using AOERandomizer.ViewModel.Base;

namespace AOERandomizer.ViewModel.Windows
{
    public class SplashScreenWindowViewModel : ViewModelBase
    {
        #region Members

        private string _loadingLabel;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Constructs a new instance of a splash screen view model.
        /// </summary>
        /// <param name="initialText">(optional) The initial text to display in the splash screen label.</param>
        public SplashScreenWindowViewModel(string initialText = "")
        {
            this._loadingLabel = initialText;
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