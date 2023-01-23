using ABBTask.Data.Data;
using ABBTask.Data.Schema.Entities;
using ABBTask.Infrastructure.Dto.Estate;
using ABBTask.Infrastructure.Models.Estate;
using ABBTask.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABBTask.Repositories
{
    public class EstateRepository : IEstateRepository
    {
        private readonly ABBTaskDbContext _dbContext;
        private readonly ILogger _logger;

        public EstateRepository(ABBTaskDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<EstateDto>> GetEstates()
        {
            IEnumerable<EstateDto> estates = new List<EstateDto>();
            try
            {
                estates = await _dbContext.Estates.Select(x => new EstateDto()
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
                _logger.LogError(excp, "GetAll: Error while getting Estates from database");
            }

            return estates;
        }

        public async Task<EstateDto> GetEstateById(int id)
        {
            EstateDto result = new EstateDto();
            try
            {
                var estateDto = await _dbContext.Estates.Select(x => new EstateDto()
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    ModificationDate = x.ModificationDate,
                    Description = x.Description,
                    ExpirationDate = x.ExpirationDate,
                    IsSold = x.IsSold,
                    Name = x.Name,
                    Price = Decimal.ToDouble(x.Price)
                }).FirstOrDefaultAsync(x => x.Id == id);

                if (estateDto != null)
                {
                    result = estateDto;
                }
                else
                {
                    result.Id = -1;
                }
            }
            catch (Exception excp)
            {
                _logger.LogError(excp, $"GetEstateById: Error while getting Estate from database Id: {id}");               
            }

            return result;
        }

        public async Task<EstateDto> CreateEstate(CreateEstateDto createEstateDto)
        {
            EstateDto result = new EstateDto();

            try
            {
                var estateEntity = new Estate()
                {
                    ModificationDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    IsSold = createEstateDto.IsSold,
                    Name = createEstateDto.Name,
                    Price = Convert.ToDecimal(createEstateDto.Price),
                    Description = createEstateDto.Description
                };

                _dbContext.Estates.Add(estateEntity);
                await _dbContext.SaveChangesAsync();

                result = new EstateDto() // mapuję encję na Dto z wygenerowanym id
                {
                    Id = estateEntity.Id,
                    CreatedDate = estateEntity.CreatedDate,
                    ModificationDate = estateEntity.ModificationDate,
                    Description = estateEntity.Description,
                    ExpirationDate = estateEntity.ExpirationDate,
                    IsSold = estateEntity.IsSold,
                    Name = estateEntity.Name,
                    Price = Decimal.ToDouble(estateEntity.Price)
                };
            }
            catch (Exception excp)
            {
                _logger.LogError(excp, $"CreateEstate: Error while Create Estate on database");
            }
            return result;
        }

        public async Task<EstateDto> UpdateEstate(UpdateEstateDto estateDto)
        {
            EstateDto result = new EstateDto();

            try
            {
                var estateEntity = _dbContext.Estates.FirstOrDefault(x => x.Id == estateDto.Id);

                if (estateEntity != null)
                {
                    estateEntity.Id = estateDto.Id;
                    estateEntity.ModificationDate = DateTime.Now;
                    estateEntity.IsSold = estateDto.IsSold;
                    estateEntity.Name = estateDto.Name;
                    estateEntity.Price = Convert.ToDecimal(estateDto.Price);
                    estateEntity.Description = estateDto.Description;

                    _dbContext.Update(estateEntity);

                    await _dbContext.SaveChangesAsync();

                    result = new EstateDto()
                    {
                        Id = estateEntity.Id,
                        CreatedDate = estateEntity.CreatedDate,
                        ModificationDate = estateEntity.ModificationDate,
                        Description = estateEntity.Description,
                        ExpirationDate = estateEntity.ExpirationDate,
                        IsSold = estateEntity.IsSold,
                        Name = estateEntity.Name,
                        Price = Decimal.ToDouble(estateEntity.Price)
                    };
                }
                else
                {
                    result.Id = -1;
                }
            }
            catch (Exception excp)
            {
                _logger.LogError(excp, $"UpdateEstate: Error while Update Estate on database Id: {estateDto.Id}");
            }
            return result;
        }

        public async Task<bool> DeleteEstate(int id)
        {
            try
            {
                var estate = await _dbContext.Estates.FindAsync(id);
                if (estate == null)
                {
                    return false;
                }
                _dbContext.Estates.Remove(estate);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception excp)
            {
                _logger.LogError(excp, $"DeleteEstate: Error while Delete Estate on database Id: {id}");
            }

            return true;
        }
    }
}
