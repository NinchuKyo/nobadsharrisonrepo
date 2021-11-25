using System.Windows;
using System.Windows.Controls;

namespace AOERandomizer.View.Controls
{
    /// <summary>
    /// Base class for a button with both an image and text
    /// (custom style for this button in ResourceDictionaries/ButtonStyles.xaml).
    /// </summary>
    internal class FroggoImageButton : Button
    {
        /// <summary>
        /// Default static constructor.
        /// </summary>
        static FroggoImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FroggoImageButton), new FrameworkPropertyMetadata(typeof(FroggoImageButton)));
        }
    }
}