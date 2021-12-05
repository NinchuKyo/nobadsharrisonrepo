using AOERandomizer.ViewModel.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace AOERandomizer.View.Converters
{
    /// <summary>
    /// Enum binding source extension.
    /// </summary>
    public class EnumBindingSourceExtension : MarkupExtension
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="enumType">Enum for binding.</param>
        public EnumBindingSourceExtension(Type enumType)
        {
            if (enumType == null || !enumType.IsEnum)
            {
                throw new("'enumType' must be a non-null Enum type.");
            }

            this.EnumType = enumType;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the enum type.
        /// </summary>
        public Type EnumType { get; private set; }

        #endregion // Properties

        #region Methods

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            List<string> values = new();

            foreach (Enum val in Enum.GetValues(EnumType))
            {
                values.Add(val.GetDisplayName());
            }

            return values.ToArray();
        }

        #endregion // Methods
    }
}