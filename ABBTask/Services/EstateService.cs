using ABBTask.Data.Data;
using ABBTask.Data.Schema.Entities;
using ABBTask.Infrastructure.Dto.Estate;
using ABBTask.Infrastructure.Models.Estate;
using ABBTask.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ABBTask.Services
{
    public class EstateService : IEstateService
    {
        private readonly ABBTaskDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IEstateValidatorService _estateValidatorService;

        public EstateService(ABBTaskDbContext dbContext, ILogger logger, IEstateValidatorService estateValidatorService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _estateValidatorService = estateValidatorService;
        }

        public async Task<IEnumerable<EstateDto>> FilterEstates(FilterEstate filter)
        {
            var estatesDto = new List<EstateDto>();

            try
            {
                _estateValidatorService.ValidateFilterEstate(filter);

                var filtered = _dbContext.Estates.AsQueryable();

                filtered = FilterExpirationDate(filter, filtered);

                filtered = filtered.Where(d => d.Price >= Convert.ToDecimal(filter.PriceFrom) && d.Price <= Convert.ToDecimal(filter.PriceTo)); // zakładam że ktoś może chcieć coś za darmo :)

                estatesDto = await filtered.Select(x => new EstateDto()
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    ModificationDate = x.ModificationDate,
                    Description = x.Description,
                    ExpirationDate = x.ExpirationDate,
                    IsSold = x.IsSold,
                    Name = x.Name,
                    Price = Decimal.ToDouble(x.Price)
                }).ToListAsync();
            }
            catch (Exception excp)
            {
                _logger.LogError(excp, "Error trying geting Filtered Estates");
            }

            return estatesDto;
        }

        private IQueryable<Estate> FilterExpirationDate(FilterEstate filter, IQueryable<Estate> filtered)
        {
            if (!string.IsNullOrWhiteSpace(filter.ExpirationDateFrom) && !string.IsNullOrWhiteSpace(filter.ExpirationDateTo))
            {
                DateTime expirationDateFrom = ParseToDateTime(filter.ExpirationDateFrom);
                DateTime expirationDateTo = ParseToDateTime(filter.ExpirationDateTo);
                filtered = filtered.Where(d => d.ExpirationDate >= expirationDateFrom && d.ExpirationDate <= expirationDateTo);
            }
            else if (!string.IsNullOrWhiteSpace(filter.ExpirationDateFrom))
            {
                DateTime expirationDateFrom = ParseToDateTime(filter.ExpirationDateFrom);
                filtered = filtered.Where(d => d.ExpirationDate >= expirationDateFrom);
            }
            else if (!string.IsNullOrWhiteSpace(filter.ExpirationDateTo))
            {
                DateTime expirationDateTo = ParseToDateTime(filter.ExpirationDateTo);
                filtered = filtered.Where(d => d.ExpirationDate <= expirationDateTo);
            }

            return filtered;
        }

        private DateTime ParseToDateTime(string filter)
        {
            return DateTime.ParseExact(filter, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
