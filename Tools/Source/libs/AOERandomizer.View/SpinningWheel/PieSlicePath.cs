using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AOERandomizer.View.SpinningWheel
{
    public class PieSlicePath : Shape
    {
        #region Members

        private bool _isLoaded;
        private Geometry _geometry;

        #endregion // Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PieSlicePath()
        {
            this._isLoaded = false;
            this._geometry = new PathGeometry();

            this.Loaded += PieSlicePath_Loaded;
        }

        #endregion // Constructors

        #region Dependency Properties

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(PieSlicePath),
                new PropertyMetadata(default(double), (s, e) => { PieSlicePath_DependencyPropertyChanged(s as PieSlicePath); }));

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(PieSlicePath),
                new PropertyMetadata(default(double), (s, e) => { PieSlicePath_DependencyPropertyChanged(s as PieSlicePath); }));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(PieSlicePath),
                new PropertyMetadata(default(double), (s, e) => { PieSlicePath_DependencyPropertyChanged(s as PieSlicePath); }));

        #endregion // Dependency Properties

        #region Properties

        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        public double StartAngle
        {
            get { return (double)this.GetValue(StartAngleProperty); }
            set { this.SetValue(StartAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        public double Angle
        {
            get { return (double)this.GetValue(AngleProperty); }
            set { this.SetValue(AngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        public double Radius
        {
            get { return (double)this.GetValue(RadiusProperty); }
            set { this.SetValue(RadiusProperty, value); }
        }

        /// <inheritdoc />
        protected override Geometry DefiningGeometry => this._geometry;

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Triggers when this pie slice path has loaded.
        /// </summary>
        /// <param name="sender">Object that fired the event.</param>
        /// <param name="e">Event arguments.</param>
        private void PieSlicePath_Loaded(object sender, RoutedEventArgs e)
        {
            this._isLoaded = true;
            this.Redraw();
        }

        /// <summary>
        /// Triggered when the pie slice changes.
        /// </summary>
        /// <param name="pieSlice">The changed pie slice.</param>
        private static void PieSlicePath_DependencyPropertyChanged(PieSlicePath pieSlice)
        {
            if (pieSlice._isLoaded)
            {
                pieSlice.Redraw();
            }
        }

        /// <summary>
        /// Redraws this pie slice.
        /// </summary>
        private void Redraw()
        {
            Debug.Assert(this.GetValue(StartAngleProperty) != DependencyProperty.UnsetValue);
            Debug.Assert(this.GetValue(RadiusProperty) != DependencyProperty.UnsetValue);
            Debug.Assert(this.GetValue(AngleProperty) != DependencyProperty.UnsetValue);

            this.Width = 2.0 * (this.Radius + this.StrokeThickness);
            this.Height = 2.0 * (this.Radius + this.StrokeThickness);

            double endAngle = this.StartAngle + this.Angle;

            double arcX = this.Radius + Math.Sin(endAngle * Math.PI / 180.0) * this.Radius;
            double arcY = this.Radius - Math.Cos(endAngle * Math.PI / 180.0) * this.Radius;

            double lineX = this.Radius + Math.Sin(this.StartAngle * Math.PI / 180.0) * this.Radius;
            double lineY = this.Radius - Math.Cos(this.StartAngle * Math.PI / 180.0) * this.Radius;

            PathFigure figure = new()
            {
                StartPoint = new Point(this.Radius, this.Radius),
                IsClosed = true
            };

            LineSegment line = new()
            {
                Point = new Point(lineX, lineY)
            };

            ArcSegment arc = new()
            {
                IsLargeArc = this.Angle >= 180.0,
                Point = new Point(arcX, arcY),
                Size = new Size(this.Radius, this.Radius),
                SweepDirection = SweepDirection.Clockwise
            };

            figure.Segments.Add(line);
            figure.Segments.Add(arc);

            this._geometry = new PathGeometry
            {
                Figures = { figure }
            };

            this.InvalidateArrange();
        }

        #endregion // Methods
    }
}