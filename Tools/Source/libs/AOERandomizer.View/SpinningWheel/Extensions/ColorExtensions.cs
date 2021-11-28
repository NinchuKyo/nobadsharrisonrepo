using AOERandomizer.RandomGeneration;
using System.Windows.Media;

namespace AOERandomizer.View.SpinningWheel.Extensions
{
    public static class ColorExtensions
    {
        #region Constants

        private const float DefaultCorrectionFactor = 0.25f;
        private const float MaxColorValue = 255.0f;

        #endregion // Constants

        #region Methods

        public static Color GetRandomAssColor(this Color source)
        {
            float red = (float)MasterRNG.GetRandomNumberFrom(0, 255);
            float green = (float)MasterRNG.GetRandomNumberFrom(0, 255);
            float blue = (float)MasterRNG.GetRandomNumberFrom(0, 255);

            //float red = source.R + (DefaultCorrectionFactor * (MaxColorValue - source.R));
            //float green = source.G + (DefaultCorrectionFactor * (MaxColorValue - source.G));
            //float blue = source.B + (DefaultCorrectionFactor * (MaxColorValue - source.B));

            return Color.FromArgb(source.A, (byte)red, (byte)green, (byte)blue);
        }

        #endregion // Methods
    }
}