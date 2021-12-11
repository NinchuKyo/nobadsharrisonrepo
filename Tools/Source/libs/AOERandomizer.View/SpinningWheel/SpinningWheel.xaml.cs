using AOERandomizer.Multimedia;
using AOERandomizer.RandomGeneration;
using AOERandomizer.View.SpinningWheel.Base;
using AOERandomizer.View.SpinningWheel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
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

        private PieSlice? _selectedItem;
        private Color _backgroundColor;
        private Color _foregroundColor;
        private double _angle;
        private double _size;
        private bool _hideLabels;
        private bool _showWheel;

        private static readonly BackEase BackEase;
        private static readonly CircleEase CircleEase;

        private static double SliceAngle;
        private static double DegreesToTurn;
        private static double BeepThreshold;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static SpinningWheel()
        {
            BackEase = new()
            {
                EasingMode = EasingMode.EaseOut,
                Amplitude = 0.1
            };

            CircleEase = new()
            {
                EasingMode = EasingMode.EaseOut
            };
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SpinningWheel()
        {
            this._pieSlices = new();
            this._pieSlices.CollectionChanged += PieSlices_CollectionChanged;

            this._slices = new();
            this._showWheel = false;
            this.SetValue(SlicesDependencyProperty, this._slices);

            this.InitializeComponent();

            this._storyBoard = (Storyboard)this.Resources["storyBoard"];
            this._storyBoard.Completed += this.StoryBoard_Completed;

            this._doubleAnimation = (DoubleAnimation)this._storyBoard.Children.First();
            this._doubleAnimation.EasingFunction = CircleEase;
            this._doubleAnimation.CurrentTimeInvalidated += DoubleAnimation_CurrentTimeInvalidated;
        }

        #endregion // Constructors

        #region Dependency Properties

        /// <summary>
        /// Slices dependency property.
        /// </summary>
        public static readonly DependencyProperty SlicesDependencyProperty =
            DependencyProperty.Register("Slices", typeof(ObservableCollection<string>), typeof(SpinningWheel),
                new(default(ObservableCollection<string>), (s, e) =>
                {
                    if (s is SpinningWheel w)
                    {
                        DependencyPropertyChanged(w, e);
                    }
                }));

        #endregion // Dependency Properties

        #region Properties

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public Color BackgroundColor
        {
            get { return this._backgroundColor; }
            set { this.SetProperty(ref this._backgroundColor, value); }
        }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public Color ForegroundColor
        {
            get { return this._foregroundColor; }
            set { this.SetProperty(ref this._foregroundColor, value); }
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        public double Angle
        {
            get { return this._angle; }
            set { this.SetProperty(ref this._angle, value); }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the slices.
        /// </summary>
        public ObservableCollection<string> Slices
        {
            get { return (ObservableCollection<string>)this.GetValue(SlicesDependencyProperty); }
            set
            {
                ObservableCollection<string> upper = new(value.Select(x => x.ToUpperInvariant()));
                this.SetValue(SlicesDependencyProperty, upper);
            }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether to hide the pie slice labels.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        private PieSlice? SelectedItem
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

        /// <summary>
        /// Gets the selected item's value (if any, empty string if nothing selected).
        /// </summary>
        public string SelectedItemValue => this.SelectedItem == null ? String.Empty : this.SelectedItem.Label;

        /// <summary>
        /// Gets or sets a flag indicating whether this spinning wheel is visible.
        /// </summary>
        public bool ShowWheel
        {
            get { return this._showWheel; }
            set { this.SetProperty(ref this._showWheel, value); }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// TODO
        /// </summary>
        public void AutoSpin()
        {
            this.Angle = (this.Angle + 5) % 360.0;
            Debug.WriteLine($"Angle: {this.Angle}");
            return;
        }

        /// <summary>
        /// Spins the wheel, randomizes number of full spins and randomizes the spin duration in seconds.
        /// </summary>
        /// <param name="minSpins">Minimum number of full spins.</param>
        /// <param name="maxSpins">Maximum number of full spins.</param>
        /// <param name="minDuration">Minimum spin duration in seconds.</param>
        /// <param name="maxDuration">Maximum spin duration in seconds.</param>
        public void Spin(int minSpins, int maxSpins, int minDuration, int maxDuration)
        {
            if (this.SelectedItem == null)
            {
                double angleFromYAxis = 360.0 - this.Angle;

                if (this.Angle == 0)
                {
                    this.SelectedItem = this._pieSlices.FirstOrDefault();
                }
                else
                {
                    this.SelectedItem = this._pieSlices
                        .SingleOrDefault(p => p.StartAngle <= angleFromYAxis && (p.StartAngle + p.Angle) > angleFromYAxis);
                }
            }

            if (this.SelectedItem != null && this._pieSlices.Any())
            {
                // Decide if this is a troll spin, long spin, or regular spin...
                if (MasterRNG.GetRandomNumberFrom(0, 100) <= 45)
                {
                    this._doubleAnimation.EasingFunction = BackEase;
                }
                else
                {
                    this._doubleAnimation.EasingFunction = CircleEase;
                }

                int winner = MasterRNG.GetRandomNumberFrom(0, this._pieSlices.Count - 1);
                int spins = MasterRNG.GetRandomNumberFrom(minSpins, maxSpins);
                int duration = MasterRNG.GetRandomNumberFrom(minDuration, maxDuration);

                this.SpinTo(winner, spins, duration);
            }
        }

        /// <summary>
        /// Spins the wheel to the specified item, with the specified number of full spins,
        /// for the specified duration in seconds.
        /// </summary>
        /// <param name="winner">The item to spin to.</param>
        /// <param name="spins">The number of full spins.</param>
        /// <param name="duration">The duration of the spin in seconds.</param>
        private void SpinTo(int winner, int spins, int duration)
        {
            if (this.SelectedItem != null)
            {
                Debug.WriteLine($"Predicted Winner: {winner}");
                double winnerOffset = (this._pieSlices.Count - winner) * this.SelectedItem.Angle;
                double startAngle = this.Angle;
                double finalAngle = startAngle + (spins * 360.0) - startAngle + winnerOffset - (MasterRNG.GetRandomDoubleFromZeroTo(1.0) * this.SelectedItem.Angle);

                this._doubleAnimation.From = startAngle;
                this._doubleAnimation.To = finalAngle;
                this._doubleAnimation.Duration = new(new(0, 0, duration));

                DegreesToTurn = finalAngle - startAngle;
                SliceAngle = this.SelectedItem.Angle;
                BeepThreshold = startAngle - (startAngle % SliceAngle) + SliceAngle;

                this._storyBoard.Begin();

                this.Angle = finalAngle % 360.0;
            }
        }

        /// <summary>
        /// Triggers when the spin animation has completed.
        /// </summary>
        /// <param name="sender">Object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void StoryBoard_Completed(object? sender, object? e)
        {
            double angleFromYAxis = 360.0 - this.Angle;

            this.SelectedItem = this._pieSlices
                .SingleOrDefault(p => p.StartAngle <= angleFromYAxis && (p.StartAngle + p.Angle) > angleFromYAxis);

            if (this.SelectedItem != null)
            {
                Debug.WriteLine($"WINNER! {this.SelectedItem.Label}");
                AudioHelper.PlayGolfClapSound();
            }
        }

        /// <summary>
        /// Draws the wheel.
        /// </summary>
        private void Draw()
        {
            this._pieSlices.Clear();

            if (this.Slices.Count < 2)
            {
                this.ShowWheel = false;
                return;
            }
            else
            {
                this.ShowWheel = true;
            }

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
                    color = ColourHelper.GetRandomAssColour();
                }
            }
        }

        /// <summary>
        /// Triggered when the collection of pie slices for the wheel has changed.
        /// </summary>
        /// <param name="sender">Object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PieSlices_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (PieSlice item in e.NewItems)
                        {
                            layoutSpinner.Children.Add(item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (PieSlice item in e.OldItems)
                        {
                            layoutSpinner.Children.Remove(item);
                        }
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
            NotifyCollectionChangedEventHandler handler = new(
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
        }

        /// <summary>
        /// Triggered when the spinning wheel control has finished loading.
        /// </summary>
        /// <param name="sender">Object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SpinningWheelControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Draw();
            this.SelectedItem = this._pieSlices.FirstOrDefault();

            if (this.SelectedItem != null)
            {
                this.Angle = 360.0 - this.SelectedItem.Angle / 2.0;
            }
        }

        /// <summary>
        /// Triggers when the wheel spin animation ticks/updates.
        /// This is used to determine whether or not to make the wheel *beep*
        /// as it rotates from item to item.
        /// </summary>
        /// <param name="sender">Object that fired the event.</param>
        /// <param name="e">The event arguments.</param>
        private void DoubleAnimation_CurrentTimeInvalidated(object? sender, EventArgs e)
        {
            double easeValue = this._doubleAnimation.EasingFunction.Ease(this._storyBoard.GetCurrentProgress());
            double degrees = DegreesToTurn * easeValue;

            if (degrees + this._doubleAnimation.From > BeepThreshold)
            {
                AudioHelper.PlayWheelBeepSound();
                BeepThreshold += SliceAngle;
            }
        }

        #endregion // Methods
    }
}