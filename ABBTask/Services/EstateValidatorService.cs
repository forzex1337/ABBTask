using ABBTask.Infrastructure.Dto.Estate;
using ABBTask.Interfaces;
using System.Globalization;

namespace ABBTask.Services
{
    public class EstateValidatorService : IEstateValidatorService
    {
        private readonly ILogger _logger;

        public EstateValidatorService(ILogger logger)
        {
            _logger = logger;
        }

        public void ValidateFilterEstate(FilterEstate filter)
        {
            if ((string.IsNullOrWhiteSpace(filter.ExpirationDateFrom) || string.IsNullOrWhiteSpace(filter.ExpirationDateTo))
                && filter.PriceFrom == 0 && filter.PriceTo == double.MaxValue)
                throw new Exception("At least one property of FilterEstate must have a value");
            if (!RightDateFormat(filter.ExpirationDateFrom) || !RightDateFormat(filter.ExpirationDateTo))
                throw new Exception($"Incorrect date format ExpirationDateFrom : {filter.ExpirationDateFrom} ExpirationDateTo {filter.ExpirationDateTo}");
        }

        private bool RightDateFormat(string filter)
        {
            var rightDateFormat = false;
            try
            {
                DateTime.ParseExact(filter, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                rightDateFormat = true;
            }
            catch (Exception excp)
            {
                _logger.LogError(excp, $"Incorrect date format filter : {filter}");
            }

            return rightDateFormat;
        }
    }
}
