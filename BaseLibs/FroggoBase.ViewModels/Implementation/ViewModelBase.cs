using FroggoBase.ViewModels.Interface;

namespace FroggoBase.ViewModels.Implementation
{
    /// <summary>
    /// Base implementation for an application viewmodel.
    /// 
    /// TODO: NotifyPropertyChanged
    /// TODO2: (possibly optional) Make another 'generics' ViewModelBase<T> where T : IModel
    ///        Allows us to more easily apply a model to a viewmodel.
    /// </summary>
    public class ViewModelBase : IViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewModelBase()
        {
        }

        #endregion // Constructors
    }
}