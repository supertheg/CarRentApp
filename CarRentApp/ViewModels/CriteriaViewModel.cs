using System.Collections.Generic;

namespace CarRentApp
{
    public class CriteriaViewModel
    {
        public IEnumerable<string> CarTypes { get; set; }
        public string TypeName { get; set; }
        public decimal? PriceFrom { get; set; } = 50;
        public decimal? PriceTo { get; set; } = 100;
        public bool Conditioning { get; set; }
        public bool Radio { get; set; }
    }
}