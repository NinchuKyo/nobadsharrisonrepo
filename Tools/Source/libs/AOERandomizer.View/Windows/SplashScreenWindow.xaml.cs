using System.Windows;
using System.Windows.Input;

namespace AOERandomizer.View.Windows
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SplashScreenWindow()
        {
            this.InitializeComponent();
        }

        #endregion // Constructors

        #region UI Events

        /// <summary>
        /// Triggers when the mouse is pressed anywhere on the window.
        /// </summary>
        /// <param name="sender">The object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SplashWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        #endregion // UI Events
    }
}