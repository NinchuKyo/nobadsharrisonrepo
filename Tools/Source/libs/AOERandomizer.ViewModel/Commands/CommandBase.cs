using System;
using System.Windows.Input;

namespace AOERandomizer.ViewModel.Commands
{
    /// <summary>
    /// Base-bones basic CommandBase with optional functionality.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? param) => true;

        public abstract void Execute(object? param);

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}