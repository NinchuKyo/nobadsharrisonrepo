using System;
using System.Windows.Input;

namespace AOERandomizer.ViewModel.Commands
{
    /// <summary>
    /// Relay command class to wrap view-to-viewmodel event triggers.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Members

        private readonly Predicate<object?> _canExecute;
        private readonly Action<object?> _execute;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="canExecute">Used to determine if this command can be executed.</param>
        /// <param name="execute">Used to execute the command.</param>
        public RelayCommand(Predicate<object?> canExecute, Action<object?> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        #endregion // Constructors

        #region Events

        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion // Events

        #region Methods

        /// <inheritdoc />
        public bool CanExecute(object? parameter)
        {
            return this._canExecute(parameter);
        }

        /// <inheritdoc />
        public void Execute(object? parameter)
        {
            this._execute(parameter);
        }

        #endregion // Methods
    }
}