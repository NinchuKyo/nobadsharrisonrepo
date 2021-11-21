using FroggoBase.ViewModels.Interface;
using System.ComponentModel;

namespace FroggoBase.ViewModels.Implementation
{
    /// <summary>
    /// Base implementation for an application viewmodel.
    /// 
    /// TODO: NotifyPropertyChanged
    /// TODO2: (possibly optional) Make another 'generics' ViewModelBase<T> where T : IModel
    ///        Allows us to more easily apply a model to a viewmodel.
    /// </summary>
    public class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewModelBase()
        {
        }

        #endregion // Constructors

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}