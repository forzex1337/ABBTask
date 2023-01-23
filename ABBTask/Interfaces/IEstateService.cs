using ABBTask.Infrastructure.Dto.Estate;
using ABBTask.Infrastructure.Models.Estate;

namespace ABBTask.Interfaces
{
    public interface IEstateService
    {
        Task<IEnumerable<EstateDto>> FilterEstates(FilterEstate filterEstate);
    }
}
