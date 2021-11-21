using RandoThing.ViewModels;
using System.Windows.Controls;

namespace RandoThing.Views
{
    /// <summary>
    /// Interaction logic for TeamsView.xaml
    /// </summary>
    public partial class TeamsView : UserControl
    {
        public TeamsView()
        {
            InitializeComponent();

            DataContext = new TeamsViewViewModel();
        }
    }
}
