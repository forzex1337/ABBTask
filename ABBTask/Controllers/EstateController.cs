using ABBTask.Infrastructure.Dto.Estate;
using ABBTask.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABBTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstateController : ControllerBase
    {
        private readonly IEstateRepository _estateRepository;
        private readonly IEstateService _estateService;

        public EstateController(IEstateRepository estateRepository, IEstateService estateService)
        {
            _estateRepository = estateRepository;
            _estateService = estateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstates()
        {
            return Ok(await _estateRepository.GetEstates());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstate(int id)
        {
            var estateDto = await _estateRepository.GetEstateById(id);

            if (estateDto.Id < 0)
            {
                return NotFound();
            }

            return Ok(await _estateRepository.GetEstateById(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEstate(UpdateEstateDto updateEstateDto)
        {
            return Ok(await _estateRepository.UpdateEstate(updateEstateDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstate(CreateEstateDto createEstateDto)
        {
            return Ok(await _estateRepository.CreateEstate(createEstateDto));
        }


        [HttpGet]
        [Route("FilterEstates/{ExpirationDateFrom}/{ExpirationDateTo}/{PriceFrom}/{PriceTo}")] 
        public async Task<IActionResult> FilterEstates(string ExpirationDateFrom, string ExpirationDateTo, double PriceFrom, double PriceTo)
        {
            var filterEstate = new FilterEstate()
            { 
                ExpirationDateFrom = ExpirationDateFrom,
                ExpirationDateTo = ExpirationDateTo,
                PriceFrom = PriceFrom,
                PriceTo = PriceTo
            };

            var filteredEstates = await _estateService.FilterEstates(filterEstate);

            if (filteredEstates == null)
            {
                return BadRequest();
            }
                
            return Ok(filteredEstates);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstate(int id)
        {
            return Ok(await _estateRepository.DeleteEstate(id));
        }
    }
}
