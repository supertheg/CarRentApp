using System.Collections.Generic;

namespace CarRentApp
{
    public class CriteriaViewModel
    {
        public IEnumerable<CarType> CarTypes { get; set; }
        public CarType Type { get; set; }
        public decimal? PriceFrom { get; set; } = 50;
        public decimal? PriceTo { get; set; } = 100;
        public bool Conditioning { get; set; }
        public bool Radio { get; set; }
    }
}