using AOERandomizer.ViewModel.Base;

namespace AOERandomizer.ViewModel.Navigation
{
    /// <summary>
    /// Navigation manager helper.
    /// </summary>
    public class NavigationViewModel : ViewModelBase
    {
        #region Members

        private ViewModelBase? _selectedVm;

        #endregion // Members

        #region Properties

        /// <summary>
        /// Gets or sets the currently selected viewmodel (dictates which page to display).
        /// </summary>
        internal ViewModelBase? SelectedVm
        {
            get => this._selectedVm;
            set { this.SetProperty(ref this._selectedVm, value); }
        }

        #endregion // Properties
    }
}