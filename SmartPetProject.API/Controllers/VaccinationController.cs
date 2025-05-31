using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {

        private readonly IVaccinationService _vaccinationService;

        public VaccinationController(IVaccinationService vaccinationService)
        {
            _vaccinationService = vaccinationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _vaccinationService.GetAllAsync();
            if (values == null || !values.Any())
            {
                return NotFound("No vaccinations found.");
            }
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var value = await _vaccinationService.GetByIdAsync(id);
            if (value == null)
            {
                return NotFound($"Vaccination with ID {id} not found.");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VaccinationCreateDto vaccinationCreateDto)
        {
            if (vaccinationCreateDto == null)
            {
                return BadRequest("Invalid vaccination data.");
            }

            await _vaccinationService.AddAsync(vaccinationCreateDto);
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpPut]
        public IActionResult Update([FromBody] VaccinationUpdateDto vaccinationUpdateDto)
        {
            if (vaccinationUpdateDto == null)
            {
                return BadRequest("Invalid vaccination data.");
            }

            _vaccinationService.Update(vaccinationUpdateDto);
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid ID.");
            }

            await _vaccinationService.Remove(id);
            return Ok("Silme işlemi başarılı");
        }
    }
}
