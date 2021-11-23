using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Commands;
using System;

namespace AOERandomizer.ViewModel.Navigation
{
    public class NavigateCommand<T> : CommandBase
        where T : ViewModelBase
    {
        private readonly NavigationViewModel _navVm;
        private readonly Func<T> _createPageVm;

        public NavigateCommand(NavigationViewModel navVm, Func<T> createPageVm) 
        {
            this._navVm = navVm;
            this._createPageVm = createPageVm;
        }

        public override void Execute(object? parameter)
        {
            this._navVm.SelectedVm = this._createPageVm();
        }
    }
}