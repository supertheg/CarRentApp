using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarRentApp
{
    /// <summary>
    /// Used to convert to string DisplayAttribute of enum value 
    /// </summary>
    public class EnumDisplayNameConverter : EnumConverter
    {
        public EnumDisplayNameConverter(Type type) : base(type) { }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string)) {
                if (value != null) {
                    var fi = value.GetType().GetField(value.ToString());
                    if (fi != null) {
                        return fi.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute attribute && attribute.Name != null
                            ? attribute.Name
                            : value.ToString();
                    }
                }
                return string.Empty;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}