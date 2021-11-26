using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace AOERandomizer.View.Converters
{
    /// <summary>
    /// Boolean converter class (for easier bool-visibility xaml bindings).
    /// </summary>
    /// <typeparam name="T">Generic type, but Visibility will mostly be used.</typeparam>
    internal class BooleanConverter<T> : IValueConverter
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="trueValue">The value to return when the bool is true.</param>
        /// <param name="falseValue">The value to return when the bool is false.</param>
        public BooleanConverter(T trueValue, T falseValue)
        {
            this.True = trueValue;
            this.False = falseValue;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the value when the bool is true.
        /// </summary>
        public T True { get; set; }

        /// <summary>
        /// Gets or sets the value when the bool is false.
        /// </summary>
        public T False { get; set; }

        #endregion // Properties

        #region Methods

        /// <inheritdoc />
        public virtual object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool val && val ? True : False;
        }

        /// <inheritdoc />
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T val && EqualityComparer<T>.Default.Equals(val, True);
        }

        #endregion // Methods
    }
}
