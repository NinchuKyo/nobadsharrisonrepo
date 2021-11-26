using System.Windows;

namespace AOERandomizer.View.Converters
{
    /// <summary>
    /// Reverable boolean to visibility converter (for easier xaml binding).
    /// </summary>
    internal sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}