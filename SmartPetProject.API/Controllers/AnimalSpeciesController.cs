using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPetProject.BusinessLayer.Abstracts;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.AnimalSpeciesDtos;
using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalSpeciesController : ControllerBase
    {
        private readonly IAnimalSpeciesService _animalSpeciesService;

        public AnimalSpeciesController(IAnimalSpeciesService animalSpeciesService)
        {
            _animalSpeciesService = animalSpeciesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _animalSpeciesService.GetAllAsync();
            if (values == null || !values.Any())
            {
                return NotFound("No animal species found.");
            }
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var value = await _animalSpeciesService.GetByIdAsync(id);
            if (value == null)
            {
                return NotFound($"Animal species with ID {id} not found.");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalSpeciesCreateDto animalSpeciesCreateDto)
        {
            if (animalSpeciesCreateDto == null)
            {
                return BadRequest("Invalid vaccination data.");
            }

            await _animalSpeciesService.AddAsync(animalSpeciesCreateDto);
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpPut]
        public IActionResult Update([FromBody] AnimalSpeciesUpdateDto animalSpeciesUpdateDto)
        {
            if (animalSpeciesUpdateDto == null)
            {
                return BadRequest("Invalid vaccination data.");
            }

            _animalSpeciesService.Update(animalSpeciesUpdateDto);
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid ID.");
            }

            await _animalSpeciesService.Remove(id);
            return Ok("Silme işlemi başarılı");
        }
    }
}
