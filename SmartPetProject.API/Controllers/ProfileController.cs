using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.ProfileDtos;
using SmartPetProject.EntityLayer.Entities;
using System.Security.Claims;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPut("veterinarian")]
        public async Task<IActionResult> UpdateVeterinarian([FromBody] VeterinarianUpdateDto dto)
        {
            if (!IsUserType("veterinarian")) return StatusCode(403,"Sadece veterinerler bu işlemi yapabilir.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _profileService.UpdateVeterinarianFullProfileAsync(userId, dto);
            return result ? Ok("Profil güncellendi.") : BadRequest("Profil bulunamadı.");
        }

        [HttpPut("owner")]
        public async Task<IActionResult> UpdateAnimalOwner([FromBody] AnimalOwnerUpdateDto dto)
        {
            if (!IsUserType("owner") && !IsUserType("petowner")) return StatusCode(403,"Sadece hayvan sahipleri bu işlemi yapabilir.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _profileService.UpdateAnimalOwnerFullProfileAsync(userId, dto);
            return result ? Ok("Profil güncellendi.") : BadRequest("Profil bulunamadı.");
        }

        [HttpGet("admin/all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!IsUserType("admin")) return StatusCode(403,"Sadece admin kullanıcılar erişebilir.");

            var users = await _profileService.GetAllUsersForAdminAsync();
            return Ok(users);
        }

        private bool IsUserType(string type)
        {
            return User.FindFirst("UserType")?.Value?.ToLower() == type.ToLower();
        }

        [HttpGet("veterinarian")]
        public async Task<IActionResult> GetVeterinarianProfile()
        {
            var userType = User.FindFirst("UserType")?.Value?.ToLower();
            if (userType != "veterinarian")
                return StatusCode(403,"Bu endpoint yalnızca veteriner kullanıcılar içindir.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profile = await _profileService.GetVeterinarianProfileAsync(userId);
            if (profile == null)
                return NotFound("Veteriner profili bulunamadı.");

            return Ok(profile);
        }

        [HttpGet("owner")]
        public async Task<IActionResult> GetAnimalOwnerProfile()
        {
            var userType = User.FindFirst("UserType")?.Value?.ToLower();
            if (userType != "owner" && userType != "petowner")
                return StatusCode(403,"Bu endpoint yalnızca hayvan sahibi kullanıcılar içindir.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profile = await _profileService.GetAnimalOwnerProfileAsync(userId);
            if (profile == null)
                return NotFound("Hayvan sahibi profili bulunamadı.");

            return Ok(profile);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı kimliği bulunamadı.");


            var result = await _profileService.PasswordChangeAsync(userId, dto);
            if (result)
                return Ok("Şifre başarıyla değiştirildi.");
            else
                return BadRequest("Şifre değişikliği başarısız oldu.");
        }
    }
}
