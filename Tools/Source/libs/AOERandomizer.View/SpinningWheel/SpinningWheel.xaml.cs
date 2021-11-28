using AOERandomizer.RandomGeneration;
using AOERandomizer.View.SpinningWheel.Base;
using AOERandomizer.View.SpinningWheel.Extensions;
using AOERandomizer.View.SpinningWheel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AOERandomizer.View.SpinningWheel
{
    /// <summary>
    /// Interaction logic for SpinningWheel.xaml
    /// </summary>
    public partial class SpinningWheel : NotifyPropertyChangedUserControl
    {
        #region Members

        private readonly ObservableCollection<PieSlice> _pieSlices;
        private readonly ObservableCollection<string> _slices;

        private readonly Storyboard _storyBoard;
        private readonly DoubleAnimation _doubleAnimation;

        private Color _backgroundColor;
        private Color _foregroundColor;
        private double _angle;
        private double _size;
        private bool _hideLabels;
        private PieSlice _selectedItem;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SpinningWheel()
        {
            this._pieSlices = new();
            this._slices = new();

            this.InitializeComponent();

            this._storyBoard = (Storyboard)this.Resources["storyBoard"];
            this._doubleAnimation = (DoubleAnimation)this._storyBoard.Children.First();
            this._doubleAnimation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };

            this.SetValue(SlicesDependencyProperty, this._slices);

            this._pieSlices.CollectionChanged += PieSlices_CollectionChanged;
        }

        #endregion // Constructors

        #region Dependency Properties

        public static readonly DependencyProperty SlicesDependencyProperty =
            DependencyProperty.Register("Slices", typeof(ObservableCollection<string>), typeof(SpinningWheel),
                new PropertyMetadata(default(ObservableCollection<string>), (s, e) => { DependencyPropertyChanged(s as SpinningWheel, e); }));

        #endregion // Dependency Properties

        #region Properties

        public Color BackgroundColor
        {
            get { return this._backgroundColor; }
            set { this.SetProperty(ref this._backgroundColor, value); }
        }

        public Color ForegroundColor
        {
            get { return this._foregroundColor; }
            set { this.SetProperty(ref this._foregroundColor, value); }
        }

        public double Angle
        {
            get { return this._angle; }
            set { this.SetProperty(ref this._angle, value); }
        }

        public double Size
        {
            get { return this._size; }
            set
            { 
                this.Height = value;
                this.Width = value;
                this.SetProperty(ref this._size, value);
            }
        }

        public ObservableCollection<string> Slices
        {
            get { return (ObservableCollection<string>)this.GetValue(SlicesDependencyProperty); }
            set
            {
                ObservableCollection<string> upper = new(value.Select(x => x.ToUpperInvariant()));
                this.SetValue(SlicesDependencyProperty, upper);
            }
        }

        public bool HideLabels
        {
            get { return this._hideLabels; }
            set
            {
                foreach (PieSlice pieSlice in this._pieSlices)
                {
                    pieSlice.HideLabel = value;
                }

                this.SetProperty(ref this._hideLabels, value);
            }
        }

        private PieSlice SelectedItem
        {
            get { return this._selectedItem; }
            set
            {
                if (value != null)
                {
                    this.SetProperty(ref this._selectedItem, value);
                }
            }
        }

        public string SelectedItemValue => this.SelectedItem?.Label;

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Spins the wheel randomly.
        /// </summary>
        /// <param name="maxSpins">Maximum no. of spins or revolutions.</param>
        /// <param name="durationInSec">Spin duration in Second. [-1 denotes random duration]</param>
        public void Spin(int maxSpins = 5, int durationInSec = -1)
        {
            if (this.SelectedItem == null)
            {
                if (this._pieSlices.Any())
                {
                    this.SelectedItem = this._pieSlices.First();
                }
            }

            if (this.SelectedItem != null && this._pieSlices.Any())
            {
                int steps = MasterRNG.GetRandomNumberFrom(this._pieSlices.Count, this._pieSlices.Count * maxSpins);
                this.SpinTo(steps, durationInSec);
            }
        }

        private void SpinTo(int itemIndex, int durationInSec = -1)
        {
            double angleFromYAxis = 360.0 - this.Angle;

            this.SelectedItem = this._pieSlices
                .SingleOrDefault(p => p.StartAngle <= angleFromYAxis && (p.StartAngle + p.Angle) > angleFromYAxis);

            int count = this._pieSlices.Count;
            int currIndex = this._pieSlices.IndexOf(this.SelectedItem);
            int fullSpin = itemIndex / count;
            int steps = currIndex - (itemIndex % count);

            if (steps < 0)
            {
                steps = count + steps;
            }

            double startAngle = this.SelectedItem.StartAngle + this.SelectedItem.Angle / 2.0;
            double finalAngle = startAngle + fullSpin * 360.0 + steps * 360.0 / count;

            this._doubleAnimation.From = startAngle;
            this._doubleAnimation.To = finalAngle;
            if (durationInSec > 0)
            {
                this._doubleAnimation.Duration = new Duration(new TimeSpan(0, 0, durationInSec));
            }
            else
            {
                this._doubleAnimation.Duration = new Duration(new TimeSpan(0, 0, MasterRNG.GetRandomNumberFrom(3, 6)));
            }

            this._storyBoard.Begin();
            this._storyBoard.Completed += this.StoryBoard_Completed;

            this.Angle = ((int)finalAngle) % 360.0;

        }

        private void StoryBoard_Completed(object sender, object e)
        {
            double angleFromYAxis = 360.0 - this.Angle;
            this.SelectedItem = this._pieSlices
                .SingleOrDefault(p => p.StartAngle <= angleFromYAxis && (p.StartAngle + p.Angle) > angleFromYAxis);
        }

        private void Draw()
        {
            this._pieSlices.Clear();

            gridRotateTransform.CenterX = this.RenderSize.Width / 2.0;
            gridRotateTransform.CenterY = this.RenderSize.Height / 2.0;

            double startAngle = 0.0;
            Color color = this.BackgroundColor;

            if (this.Slices != null)
            {
                foreach (string slice in this.Slices)
                {
                    double sliceSize = 360.0 / this.Slices.Count;

                    PieSlice pieSlice = new()
                    {
                        StartAngle = startAngle,
                        Angle = sliceSize,
                        Radius = this.Size / 2.0,
                        BackgroundColor = color,
                        Label = slice,
                        ForegroundColor = this.ForegroundColor,
                        HideLabel = this.HideLabels,
                    };

                    this._pieSlices.Add(pieSlice);

                    startAngle += sliceSize;
                    color = color.GetRandomAssColor();
                }
            }
        }

        private void LayoutRoot_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            this._storyBoard.Stop();
            this.Angle = QuadrantHelper.GetAngle(e.ManipulationOrigin, this.RenderSize);
        }

        private void LayoutRoot_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            double angleFromYAxis = 360.0 - this.Angle;
            this.SelectedItem = this._pieSlices
                .SingleOrDefault(p => p.StartAngle <= angleFromYAxis && (p.StartAngle + p.Angle) > angleFromYAxis);

            double finalAngle = this.SelectedItem.StartAngle + this.SelectedItem.Angle / 2.0;

            this._doubleAnimation.From = this.Angle;
            this._doubleAnimation.To = 360.0 - finalAngle;
            this._storyBoard.Begin();

            this.Angle = 360.0 - finalAngle;
        }

        private void PieSlices_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (PieSlice item in e.NewItems)
                    {
                        layoutSpinner.Children.Add(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (PieSlice item in e.OldItems)
                    {
                        layoutSpinner.Children.Remove(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    layoutSpinner.Children.Clear();
                    break;
            }
        }

        /// <summary>
        /// Triggered when the pie slice changes.
        /// </summary>
        /// <param name="pieSlice">The changed pie slice.</param>
        private static void DependencyPropertyChanged(SpinningWheel wheel, DependencyPropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler handler = new NotifyCollectionChangedEventHandler(
                (o, args) =>
                {
                    if (wheel.IsLoaded)
                    {
                        wheel.Draw();
                    }
                });

            if (e.OldValue != null)
            {
                INotifyCollectionChanged oldSlices = (INotifyCollectionChanged)e.OldValue;
                oldSlices.CollectionChanged -= handler;
            }

            if (e.NewValue != null)
            {
                ObservableCollection<string> newSlices = (ObservableCollection<string>)e.NewValue;
                newSlices.CollectionChanged += handler;
            }

            /*if (wheel.IsLoaded)
            {
                wheel.Draw();
            }*/
        }

        private void SpinningWheelControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Draw();
            this.SelectedItem = this._pieSlices.FirstOrDefault();

            if (this.SelectedItem != null)
            {
                this.Angle = 360.0 - this.SelectedItem.Angle / 2.0;
            }
        }

        #endregion // Methods
    }
}