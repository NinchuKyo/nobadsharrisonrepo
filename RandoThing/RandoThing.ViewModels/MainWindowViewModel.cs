using FroggoBase.ViewModels.Implementation;
using FroggoBase.ViewModels.Interface;

namespace RandoThing.ViewModels
{
    /// <summary>
    /// Viewmodel for the main window.
    /// 
    /// TODO: Contains logic for allowing Duke to choose what to randomize
    ///       (teams, players, civs, maps, etc).
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private IViewModel _currentViewModel;
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindowViewModel()
            : base()
        {
            CurrentViewModel = new TeamsViewViewModel();
        }

        #endregion // Constructors
        
        public IViewModel CurrentViewModel { get => _currentViewModel; set => _currentViewModel = value; }
    }
}