using AOERandomizer.RandomGeneration;
using System.Windows.Media;

namespace AOERandomizer.View.SpinningWheel.Helpers
{
    /// <summary>
    /// Colour helper class.
    /// </summary>
    public static class ColourHelper
    {
        /// <summary>
        /// Gets a randomly generated colour.
        /// </summary>
        /// <returns>Random colour.</returns>
        public static Color GetRandomAssColour()
        {
            float red = (float)MasterRNG.GetRandomNumberFrom(0, 255);
            float green = (float)MasterRNG.GetRandomNumberFrom(0, 255);
            float blue = (float)MasterRNG.GetRandomNumberFrom(0, 255);

            return Color.FromArgb((byte)255, (byte)red, (byte)green, (byte)blue);
        }
    }
}