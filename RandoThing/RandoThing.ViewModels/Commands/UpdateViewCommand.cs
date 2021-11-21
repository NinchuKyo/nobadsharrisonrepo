using System;
using System.Windows.Input;

namespace RandoThing.ViewModels.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainWindowViewModel _mainWindowViewModel;

        public UpdateViewCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Teams":
                    _mainWindowViewModel.CurrentViewModel = new TeamsViewViewModel();
                    break;
            }
        }
    }
}
