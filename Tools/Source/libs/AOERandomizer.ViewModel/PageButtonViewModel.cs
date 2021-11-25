using AOERandomizer.ViewModel.Base;
using AOERandomizer.ViewModel.Enums;
using AOERandomizer.ViewModel.Extensions;

namespace AOERandomizer.ViewModel
{
    public class PageButtonViewModel : ViewModelBase
    {
        #region Members

        private EPageName _pageName;
        private string _imageUri;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="imageUri">The path to the category's icon.</param>
        internal PageButtonViewModel(EPageName pageName, string imageUri)
        {
            this._pageName = pageName;
            this._imageUri = imageUri;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the page name.
        /// </summary>
        internal EPageName PageName
        {
            get => this._pageName;
            set => this.SetProperty(ref this._pageName, value);
        }

        public string DisplayName
        {
            get => this._pageName.GetDisplayName();
        }

        /// <summary>
        /// Gets or sets the path to the page's icon.
        /// </summary>
        public string ImageUri
        {
            get => this._imageUri;
            set => this.SetProperty(ref this._imageUri, value);
        }

        #endregion // Properties
    }
}