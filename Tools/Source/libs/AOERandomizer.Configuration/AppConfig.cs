using AOERandomizer.Model;

namespace AOERandomizer.Configuration
{
    /// <summary>
    /// Application settings config definition.
    /// </summary>
    public class AppConfig : ModelBase
    {
        #region Members

        private bool _enableMusic;
        private bool _enableSfx;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AppConfig()
        {
            this._enableMusic = false;
            this._enableSfx = false;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a flag indicating whether to enable background music.
        /// </summary>
        public bool EnableMusic
        {
            get { return this._enableMusic; }
            set { this.SetProperty(ref this._enableMusic, value); }
        }

        /// <summary>
        /// Gets or set a flag indicating whether to enable sound effects.
        /// </summary>
        public bool EnableSfx
        {
            get { return this._enableSfx; }
            set { this.SetProperty(ref this._enableSfx, value); }
        }

        #endregion // Properties
    }
}