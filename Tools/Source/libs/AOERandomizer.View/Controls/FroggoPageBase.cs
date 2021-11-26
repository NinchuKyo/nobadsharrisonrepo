using AOERandomizer.Multimedia;
using AOERandomizer.ViewModel.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AOERandomizer.View.Controls
{
    /// <summary>
    /// Base page class for navigation.
    /// </summary>
    internal class FroggoPageBase : UserControl
    {
        /// <summary>
        /// Triggers when the user enters a button with their mouse pointer.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((PageBaseViewModel)this.DataContext).SettingsConfig.EnableSfx)
            {
                AudioHelper.PlayButtonHoverSound();
            }
        }

        /// <summary>
        /// Triggers when the users clicks a button with their mouse.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((PageBaseViewModel)this.DataContext).SettingsConfig.EnableSfx)
            {
                AudioHelper.PlayButtonClickSound();
            }
        }
    }
}