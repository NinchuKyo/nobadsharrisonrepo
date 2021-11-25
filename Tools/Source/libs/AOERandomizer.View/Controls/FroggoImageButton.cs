using AOERandomizer.Multimedia;
using AOERandomizer.ViewModel.Pages;
using System.Windows;
using System.Windows.Controls;

namespace AOERandomizer.View.Controls
{
    public class FroggoImageButton : Button
    {
        static FroggoImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FroggoImageButton), new FrameworkPropertyMetadata(typeof(FroggoImageButton)));
        }
    }
}