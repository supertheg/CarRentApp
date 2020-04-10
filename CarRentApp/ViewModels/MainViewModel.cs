using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CarRentApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ISearchCarService carService;
        private readonly IRentCarService rentCarService;

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

        public ICommand SearchCommand => new Command(async _ => SearchResults = await carService.SearchCarsAsync(Filter));

        public ICommand RentCarCommand => new Command(async _ => RentResult = await rentCarService.RentCarAsync(SelectedCar.Id));

        public MainViewModel(ISearchCarService carService, IRentCarService rentCarService)
        {
            this.carService = carService;
            this.rentCarService = rentCarService;

            Filter = new CriteriaViewModel {
                CarTypes = carService.GetCarTypes()
            };
        }
    }
}