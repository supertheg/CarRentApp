using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
}