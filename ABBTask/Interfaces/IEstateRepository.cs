using ABBTask.Infrastructure.Dto.Estate;
using ABBTask.Infrastructure.Models.Estate;

namespace ABBTask.Interfaces
{
    public interface IEstateRepository
    {
        Task<IEnumerable<EstateDto>> GetEstates();
        Task<EstateDto> GetEstateById(int id);
        Task<EstateDto> CreateEstate(CreateEstateDto createEstateDto);
        Task<EstateDto> UpdateEstate(UpdateEstateDto updateEstateDto);
        Task<bool> DeleteEstate(int id);
    }
}
