using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AOERandomizer.ViewModel.Base
{
    /// <summary>
    /// Base implementation for an application viewmodel.
    /// 
    /// TODO: NotifyPropertyChanged
    /// TODO2: (possibly optional) Make another 'generics' ViewModelBase<T> where T : IModel
    ///        Allows us to more easily apply a model to a viewmodel.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Events

        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion // Events

        #region Methods

        /// <summary>
        /// Invokes our PropertyChanged event.  
        /// </summary>
        /// <param name="propertyName">(optional) The name of the property to change.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Determines whether the incoming new value for the property is different or not.
        /// If it is, we update the value and trigger our PropertyChanged event, and return true.
        /// Otherwise, we do nothing and return false.
        /// </summary>
        /// <typeparam name="T">The property data type.</typeparam>
        /// <param name="storage">The existing property value.</param>
        /// <param name="value">The potentially new property value.</param>
        /// <param name="propertyName">(optional) The name of the property being set.</param>
        /// <returns>True if the property was updated to a new value, false if the property was not changed.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        #endregion // Methods
    }
}