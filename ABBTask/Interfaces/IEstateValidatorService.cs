using ABBTask.Infrastructure.Dto.Estate;

namespace ABBTask.Interfaces
{
    public interface IEstateValidatorService
    {
        void ValidateFilterEstate(FilterEstate filter);
    }
}
