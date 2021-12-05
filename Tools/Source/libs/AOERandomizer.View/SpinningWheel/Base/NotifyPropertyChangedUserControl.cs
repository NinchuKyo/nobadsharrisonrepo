using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace AOERandomizer.View.SpinningWheel.Base
{
    /// <summary>
    /// User control that triggers property changed events.
    /// </summary>
    public class NotifyPropertyChangedUserControl : UserControl, INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invokes our PropertyChanged event.  
        /// </summary>
        /// <param name="propertyName">(optional) The name of the property to change.</param>
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
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
        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }
    }
}