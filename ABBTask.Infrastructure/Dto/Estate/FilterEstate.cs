namespace ABBTask.Infrastructure.Dto.Estate
{
    public class FilterEstate
    {
        public string ExpirationDateFrom { get; set; }
        public string ExpirationDateTo { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
    }
}