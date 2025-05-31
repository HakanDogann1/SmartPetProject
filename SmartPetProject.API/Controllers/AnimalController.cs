using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.DtoLayer.Dtos.AnimalDtos;
using SmartPetProject.EntityLayer.Entities;
using System.Security.Claims;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly AppDbContext _context;

        public AnimalController(IAnimalService animalService, AppDbContext context)
        {
            _animalService = animalService;
            _context = context;
        }

        [HttpGet("animal-owner")]
        public async Task<IActionResult> GetAllByAnimalOwners()
        {
            var owner = await GetCurrentOwnerAsync();
            if (owner == null) return BadRequest("Hayvan sahibi bulunamadı.");

            var animals = await _animalService.GetAllByOwnerIdAsync(owner.Id);
            return Ok(animals);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
           

            var animals = await _animalService.GetAllAsync();
            return Ok(animals);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalCreateDto dto)
        {
            if (!IsPetOwner())
                return StatusCode(403, "Sadece hayvan sahipleri hayvan ekleyebilir.");

            var owner = await GetCurrentOwnerAsync();
            if (owner == null) return BadRequest("Hayvan sahibi bulunamadı.");

            var animal = new Animal
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Age = dto.Age,
                Genus = dto.Genus,
                AnimalSpeciesId = dto.AnimalSpeciesId,
                weight = dto.Weight,
                AnimalOwnerId = owner.Id
            };

            await _animalService.AddAnimalAsync(animal);
            return Ok("Hayvan kaydı başarılı.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AnimalUpdateDto dto)
        {
            if (!IsPetOwner())
                return StatusCode(403, "Sadece hayvan sahipleri güncelleme yapabilir.");

            var owner = await GetCurrentOwnerAsync();
            if (owner == null) return BadRequest("Hayvan sahibi bulunamadı.");

            var animal = await _animalService.GetByIdAsync(dto.Id);
            if (animal == null || animal.AnimalOwnerId != owner.Id)
                return Forbid("Bu hayvan size ait değil.");

            animal.Name = dto.Name;
            animal.Age = dto.Age;
            animal.Genus = dto.Genus;
            animal.AnimalSpeciesId = dto.AnimalSpeciesId;
            animal.weight = dto.Weight;

            await _animalService.UpdateAnimalAsync(animal);
            return Ok("Hayvan bilgisi güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!IsPetOwner())
                return StatusCode(403, "Sadece hayvan sahipleri silebilir.");

            var owner = await GetCurrentOwnerAsync();
            if (owner == null) return BadRequest("Hayvan sahibi bulunamadı.");

            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null || animal.AnimalOwnerId != owner.Id)
                return Forbid("Bu hayvan size ait değil.");

            await _animalService.DeleteAnimalAsync(animal);
            return Ok("Hayvan silindi.");
        }

        // Yardımcı metodlar
        private bool IsPetOwner()
        {
            var userTypeClaim = User.FindFirst("UserType")?.Value?.ToLower();
            return userTypeClaim == "owner" || userTypeClaim == "petowner";
        }

        private async Task<AnimalOwner> GetCurrentOwnerAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _context.AnimalOwners.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
