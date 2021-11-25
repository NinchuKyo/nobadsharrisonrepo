using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace AOERandomizer.ViewModel.Extensions
{
    /// <summary>
    /// Extension class for certain enum operations.
    /// </summary>
    internal static class EnumExtensions
    {
        /// <summary>
        /// Gets the display name of the given enum.
        /// </summary>
        /// <param name="enumValue">The enum to extract the display name from.</param>
        /// <returns>The given enum's display name.</returns>
        internal static string GetDisplayName(this Enum enumValue)
        {
            string? name = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>()
                ?.GetName();

            if (!String.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            return enumValue.ToString();
        }
    }
}