using AOERandomizer.Multimedia;
using AOERandomizer.ViewModel.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AOERandomizer.View.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml.
    /// </summary>
    public partial class HomePage : UserControl
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HomePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Triggers when the user enters a button with their mouse pointer.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((HomePageViewModel)this.DataContext).AppSettings.EnableSfx)
            {
                AudioHelper.PlayButtonHoverSound();
            }
        }

        /// <summary>
        /// Triggers when the users clicks a button with their mouse.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((HomePageViewModel)this.DataContext).AppSettings.EnableSfx)
            {
                AudioHelper.PlayButtonClickSound();
            }
        }
    }
}