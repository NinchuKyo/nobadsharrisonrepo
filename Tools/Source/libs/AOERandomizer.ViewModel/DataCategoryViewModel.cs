using AOERandomizer.ViewModel.Base;

namespace AOERandomizer.ViewModel
{
    public class DataCategoryViewModel : ViewModelBase
    {
        #region Members

        private string _categoryName;
        private string _imageUri;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="imageUri">The path to the category's icon.</param>
        public DataCategoryViewModel(string categoryName, string imageUri)
        {
            this._categoryName = categoryName;
            this._imageUri = imageUri;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string CategoryName
        {
            get => this._categoryName;
            set => this.SetProperty(ref this._categoryName, value);
        }

        /// <summary>
        /// Gets or sets the path to the category's icon.
        /// </summary>
        public string ImageUri
        {
            get => this._imageUri;
            set => this.SetProperty(ref this._imageUri, value);
        }

        #endregion // Properties
    }
}