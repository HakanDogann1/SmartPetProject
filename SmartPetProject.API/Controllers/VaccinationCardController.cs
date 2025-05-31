using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.VaccinationCardDtos;
using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;
using System.Security.Claims;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationCardController : ControllerBase
    {
        private readonly IVaccinationCardService _vaccinationCardService;

        public VaccinationCardController(IVaccinationCardService vaccinationCardService)
        {
            _vaccinationCardService = vaccinationCardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!IsUserType("veterinarian"))
                return StatusCode(403, "Sadece veteriner kullanıcılar erişebilir.");
            var values = await _vaccinationCardService.GetAllAsync();
            if (values == null || !values.Any())
            {
                return NotFound("No vaccinations found.");
            }
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!IsUserType("veterinarian"))
                return StatusCode(403, "Sadece veteriner kullanıcılar erişebilir.");
            var value = await _vaccinationCardService.GetByIdAsync(id);
            if (value == null)
            {
                return NotFound($"Vaccination with ID {id} not found.");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VaccinationCardCreateDto vaccinationCardCreateDto)
        {
            if (!IsUserType("veterinarian"))
                return StatusCode(403, "Sadece veteriner kullanıcılar erişebilir.");
            if (vaccinationCardCreateDto == null)
            {
                return BadRequest("Invalid vaccination card data.");
            }

            await _vaccinationCardService.AddAsync(vaccinationCardCreateDto);
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpPut]
        public IActionResult Update([FromBody] VaccinationCardUpdateDto vaccinationCardUpdateDto)
        {
            if (!IsUserType("veterinarian"))
                return StatusCode(403, "Sadece veteriner kullanıcılar erişebilir.");
            if (vaccinationCardUpdateDto == null)
            {
                return BadRequest("Invalid vaccination card data.");
            }

            _vaccinationCardService.Update(vaccinationCardUpdateDto);
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!IsUserType("veterinarian"))
                return StatusCode(403, "Sadece veteriner kullanıcılar erişebilir.");
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid ID.");
            }

            await _vaccinationCardService.Remove(id);
            return Ok("Silme işlemi başarılı");
        }
        [HttpGet("my-cards")]
        public async Task<IActionResult> GetMyVaccinationCards()
        {
            if (!IsUserType("petowner"))
                return StatusCode(403, "Sadece hayvan sahibi kullanıcılar erişebilir.");

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı kimliği bulunamadı.");

            var cards = await _vaccinationCardService.GetVaccinationCardsByOwnerIdAsync(userId);
            if (cards == null || !cards.Any())
                return NotFound("Size ait herhangi bir aşı kartı bulunamadı.");

            return Ok(cards);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private bool IsUserType(string type)
        {
            return User.FindFirst("UserType")?.Value?.ToLower() == type.ToLower();
        }
    }
}
