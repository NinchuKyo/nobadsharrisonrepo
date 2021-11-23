using AOERandomizer.ViewModel.Base;

namespace AOERandomizer.ViewModel.Navigation
{
    /// <summary>
    /// Navigation manager helper.
    /// </summary>
    public class NavigationViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedVm;

        public ViewModelBase? SelectedVm
        {
            get => this._selectedVm;
            set { this.SetProperty(ref this._selectedVm, value); }
        }
    }
}