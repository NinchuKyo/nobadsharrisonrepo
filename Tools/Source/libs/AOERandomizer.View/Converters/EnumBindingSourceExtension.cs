using AOERandomizer.ViewModel.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace AOERandomizer.View.Converters
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        public EnumBindingSourceExtension(Type enumType)
        {
            if (enumType == null || !enumType.IsEnum)
            {
                throw new Exception("'enumType' must be a non-null Enum type.");
            }

            EnumType = enumType;
        }

        public Type EnumType { get; private set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            List<string> values = new();

            foreach (Enum val in Enum.GetValues(EnumType))
            {
                values.Add(val.GetDisplayName());
            }

            return values.ToArray();
        }
    }
}