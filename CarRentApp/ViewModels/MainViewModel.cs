using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CarRentApp
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private readonly ICarService carService;

        public CriteriaViewModel Filter { get; set; }

        private IEnumerable<CarViewModel> searchResults;
        public IEnumerable<CarViewModel> SearchResults
        {
            get => searchResults;
            set
            {
                if (value != searchResults) {
                    searchResults = value;
                    OnPropertyChanged();
                }
            }
        }

        private CarViewModel selectedCar;
        public CarViewModel SelectedCar
        {
            get => selectedCar;
            set
            {
                if (value != selectedCar) {
                    selectedCar = value;
                    RentResult = null;
                    OnPropertyChanged();
                }
            }
        }

        private RentResults? rentResult;
        public RentResults? RentResult
        {
            get => rentResult;
            set
            {
                if (value != rentResult) {
                    rentResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand SearchCommand => new Command(_ => SearchResults = carService.SearchCars(Filter));

        public ICommand RentCarCommand => new Command(_ => RentResult = carService.RentCar(SelectedCar.Id));

        public MainViewModel(ICarService carService)
        {
            this.carService = carService;

            Filter = new CriteriaViewModel {
                CarTypes = carService.GetCarTypes().Prepend(string.Empty)
            };
        }
    }

    [TypeConverter(typeof(EnumDisplayNameConverter))]
    public enum RentResults
    {
        [Display(Name = "Rent Successful")]
        RentSuccessful,
        [Display(Name = "Already Rented")]
        AlreadyRented,
        EngineFail,
        AirConditioningFail,
        RadioFail
    }

    public class EnumDisplayNameConverter : EnumConverter
    {
        public EnumDisplayNameConverter(Type type) : base(type) { }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string)) {
                if (value != null) {
                    var fi = value.GetType().GetField(value.ToString());
                    if (fi != null) {
                        return (fi.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute attribute) && (!string.IsNullOrEmpty(attribute.Name))
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