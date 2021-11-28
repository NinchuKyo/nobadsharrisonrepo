using AOERandomizer.View.SpinningWheel.Base;
using AOERandomizer.View.SpinningWheel.Helpers;
using System.Windows;
using System.Windows.Media;

namespace AOERandomizer.View.SpinningWheel
{
    public sealed partial class PieSlice : NotifyPropertyChangedUserControl
    {
        #region Members

        private SolidColorBrush _pieSlicePathFill;
        private SolidColorBrush _textBlockForeground;
        private double _textBlockRotateAngle;
        private double _textBlockTranslateX;
        private double _textBlockTranslateY;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PieSlice()
        {
            this.InitializeComponent();
        }

        #endregion // Constructors

        #region Dependency Properties

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register("ForegroundColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(PieSlice), null);

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(PieSlice), null);

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(PieSlice), null);

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(PieSlice), null);

        public static readonly DependencyProperty HideLabelProperty =
            DependencyProperty.Register("HideLabel", typeof(bool), typeof(PieSlice), null);

        #endregion // Dependency Properties

        #region Properties

        public string Label
        {
            get { return (string)this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value.ToUpperInvariant()); }
        }

        public Color ForegroundColor
        {
            get { return (Color)this.GetValue(ForegroundColorProperty); }
            set { this.SetValue(ForegroundColorProperty, value); }
        }

        public Color BackgroundColor
        {
            get { return (Color)this.GetValue(BackgroundColorProperty); }
            set { this.SetValue(BackgroundColorProperty, value); }
        }

        public double StartAngle
        {
            get { return (double)this.GetValue(StartAngleProperty); }
            set { this.SetValue(StartAngleProperty, value); }
        }

        public double Angle
        {
            get { return (double)this.GetValue(AngleProperty); }
            set { this.SetValue(AngleProperty, value); }
        }

        public double Radius
        {
            get { return (double)this.GetValue(RadiusProperty); }
            set { this.SetValue(RadiusProperty, value); }
        }

        public bool HideLabel
        {
            get { return (bool)this.GetValue(HideLabelProperty); }
            set { this.SetValue(HideLabelProperty, value); }
        }

        public SolidColorBrush PieSlicePathFill
        {
            get { return this._pieSlicePathFill; }
            set { this.SetProperty(ref this._pieSlicePathFill, value); }
        }

        public SolidColorBrush TextBlockForeground
        {
            get { return this._textBlockForeground; }
            set { this.SetProperty(ref this._textBlockForeground, value); }
        }

        public double TextBlockRotateAngle
        {
            get { return this._textBlockRotateAngle; }
            set { this.SetProperty(ref this._textBlockRotateAngle, value); }
        }

        public double TextBlockTranslateX
        {
            get { return this._textBlockTranslateX; }
            set { this.SetProperty(ref this._textBlockTranslateX, value); }
        }

        public double TextBlockTranslateY
        {
            get { return this._textBlockTranslateY; }
            set { this.SetProperty(ref this._textBlockTranslateY, value); }
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
            this.PieSlicePathFill = new SolidColorBrush(this.BackgroundColor);
            this.TextBlockForeground = new SolidColorBrush(this.ForegroundColor);
            this.TextBlockRotateAngle = this.StartAngle + this.Angle / 2.0;

            Point newPoint = QuadrantHelper.Calculate(4.0 * this.Radius / 5.0, this.StartAngle, this.Angle);
            this.TextBlockTranslateX = newPoint.X;
            this.TextBlockTranslateY = newPoint.Y;
        }

        #endregion // Methods
    }
}