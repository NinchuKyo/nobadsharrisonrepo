using AOERandomizer.View.Controls;
using System.Windows;

namespace AOERandomizer.View.Pages
{
    /// <summary>
    /// Interaction logic for TeamsPage.xaml.
    /// </summary>
    internal partial class TeamsPage : FroggoPageBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TeamsPage()
        {
            this.InitializeComponent();
        }

        protected void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            base.Button_Click(sender, e);

            if (wheelDemo.IsLoaded)
            {
                this.wheelDemo.Spin(100, 6);
            }
        }

        protected void Auto_Button_Click(object sender, RoutedEventArgs e)
        {
            base.Button_Click(sender, e);

            if (wheelDemo.IsLoaded)
            {
                this.wheelDemo.Spin(100, 6);
            }
        }
    }
}