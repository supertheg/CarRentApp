using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRentApp
{
    [TypeConverter(typeof(EnumDisplayNameConverter))]
    public enum CarType
    {
        [Display(Name = "")]
        None,
        Sedan,
        Sport,
        Van
    }
}