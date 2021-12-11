using AOERandomizer.View.SpinningWheel.Base;
using AOERandomizer.View.SpinningWheel.Helpers;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AOERandomizer.View.SpinningWheel
{
    /// <summary>
    /// Interaction logic for PieSlice.xaml.
    /// </summary>
    public sealed partial class PieSlice : NotifyPropertyChangedUserControl
    {
        #region Constants

        private const string FaceIconsPath = $"pack://application:,,,/AOERandomizer.Multimedia;component/Resources/Images/Faces";

        #endregion // Constants

        #region Members

        private Brush _pieSlicePathFill;
        private Brush _textBlockForeground;
        private double _textBlockRotateAngle;
        private double _textBlockTranslateX;
        private double _textBlockTranslateY;
        private double _faceImageRotateAngle;
        private double _faceImageTranslateX;
        private double _faceImageTranslateY;
        private ImageSource? _faceImage;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PieSlice()
        {
            this._pieSlicePathFill = new SolidColorBrush(Colors.Transparent);
            this._textBlockForeground = new SolidColorBrush(Colors.Transparent);

            this.InitializeComponent();
        }

        #endregion // Constructors

        #region Dependency Properties

        /// <summary>
        /// Background colour dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(PieSlice), null);

        /// <summary>
        /// Foreground colour dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register("ForegroundColor", typeof(Color), typeof(PieSlice), null);

        /// <summary>
        /// Label dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(PieSlice), null);

        /// <summary>
        /// Start angle dependency property.
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(PieSlice), null);

        /// <summary>
        /// Angle dependency property.
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(PieSlice), null);

        /// <summary>
        /// Radius dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(PieSlice), null);

        /// <summary>
        /// Hide label dependency property.
        /// </summary>
        public static readonly DependencyProperty HideLabelProperty =
            DependencyProperty.Register("HideLabel", typeof(bool), typeof(PieSlice), null);

        #endregion // Dependency Properties

        #region Properties

        /// <summary>
        /// Gets or sets the label for this pie slice.
        /// </summary>
        public string Label
        {
            get { return (string)this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value.ToUpperInvariant()); }
        }

        /// <summary>
        /// Gets or sets the foreground colour of this pie slice.
        /// </summary>
        public Color ForegroundColor
        {
            get { return (Color)this.GetValue(ForegroundColorProperty); }
            set { this.SetValue(ForegroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background colour of this pie slice.
        /// </summary>
        public Color BackgroundColor
        {
            get { return (Color)this.GetValue(BackgroundColorProperty); }
            set { this.SetValue(BackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the start angle of this pie slice.
        /// </summary>
        public double StartAngle
        {
            get { return (double)this.GetValue(StartAngleProperty); }
            set { this.SetValue(StartAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the angle of this pie slice.
        /// </summary>
        public double Angle
        {
            get { return (double)this.GetValue(AngleProperty); }
            set { this.SetValue(AngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of this pie slice.
        /// </summary>
        public double Radius
        {
            get { return (double)this.GetValue(RadiusProperty); }
            set { this.SetValue(RadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether to hide the label on this pie slice.
        /// </summary>
        public bool HideLabel
        {
            get { return (bool)this.GetValue(HideLabelProperty); }
            set { this.SetValue(HideLabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the fill brush for this pie slice (associated with background colour).
        /// </summary>
        public Brush PieSlicePathFill
        {
            get { return this._pieSlicePathFill; }
            set { this.SetProperty(ref this._pieSlicePathFill, value); }
        }

        /// <summary>
        /// Gets or sets the colour brush for this pie slice's label (associated with foreground colour).
        /// </summary>
        public Brush TextBlockForeground
        {
            get { return this._textBlockForeground; }
            set { this.SetProperty(ref this._textBlockForeground, value); }
        }

        /// <summary>
        /// Gets or sets the text block rotate angle.
        /// </summary>
        public double TextBlockRotateAngle
        {
            get { return this._textBlockRotateAngle; }
            set { this.SetProperty(ref this._textBlockRotateAngle, value); }
        }

        /// <summary>
        /// Gets or sets the text block translate X value.
        /// </summary>
        public double TextBlockTranslateX
        {
            get { return this._textBlockTranslateX; }
            set { this.SetProperty(ref this._textBlockTranslateX, value); }
        }

        /// <summary>
        /// Gets or sets the text block translate Y value.
        /// </summary>
        public double TextBlockTranslateY
        {
            get { return this._textBlockTranslateY; }
            set { this.SetProperty(ref this._textBlockTranslateY, value); }
        }

        /// <summary>
        /// Gets or sets the text block rotate angle.
        /// </summary>
        public double FaceImageRotateAngle
        {
            get { return this._faceImageRotateAngle; }
            set { this.SetProperty(ref this._faceImageRotateAngle, value); }
        }

        /// <summary>
        /// Gets or sets the text block translate X value.
        /// </summary>
        public double FaceImageTranslateX
        {
            get { return this._faceImageTranslateX; }
            set { this.SetProperty(ref this._faceImageTranslateX, value); }
        }

        /// <summary>
        /// Gets or sets the text block translate Y value.
        /// </summary>
        public double FaceImageTranslateY
        {
            get { return this._faceImageTranslateY; }
            set { this.SetProperty(ref this._faceImageTranslateY, value); }
        }

        public ImageSource? FaceImage
        {
            get { return this._faceImage; }
            set { this.SetProperty(ref this._faceImage, value); }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Triggered when this pie slice has finished loading.
        /// </summary>
        /// <param name="sender">Object that fired the event.</param>
        /// <param name="routedEventArgs">Event arguments.</param>
        private void PieSliceControl_Loaded(object sender, RoutedEventArgs e)
        {
            double halfAngle = this.StartAngle + (this.Angle / 2.0);

            this.TextBlockForeground = new SolidColorBrush(this.ForegroundColor);
            this.TextBlockRotateAngle = halfAngle;
            this.FaceImageRotateAngle = halfAngle;

            Point textboxPoint = QuadrantHelper.Calculate(0.9 * this.Radius, halfAngle);
            this.TextBlockTranslateX = textboxPoint.X;
            this.TextBlockTranslateY = textboxPoint.Y;

            Point imgPoint = QuadrantHelper.Calculate(0.65 * this.Radius, halfAngle);
            this.FaceImageTranslateX = imgPoint.X;
            this.FaceImageTranslateY = imgPoint.Y;

            if (!String.IsNullOrWhiteSpace(this.Label))
            {
                try
                {
                    string faceIcon = $"{FaceIconsPath}/{this.Label}_face.png";
                    BitmapImage faceImg = new(new(faceIcon));
                    this.FaceImage = faceImg;
                }
                catch (Exception)
                {
                    string faceIcon = $"{FaceIconsPath}/unknown_face.png";
                    BitmapImage faceImg = new(new(faceIcon));
                    this.FaceImage = faceImg;
                }
            }

            this.PieSlicePathFill = new SolidColorBrush(this.BackgroundColor);
        }

        #endregion // Methods
    }
}