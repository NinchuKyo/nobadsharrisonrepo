using FroggoBase.Views;
using RandoThing.ViewModels;

namespace RandoThing.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : FroggoMainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}