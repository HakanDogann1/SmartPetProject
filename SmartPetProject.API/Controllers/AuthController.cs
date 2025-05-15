using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.AuthDtos;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-veterinarian")]
        public async Task<IActionResult> RegisterVeterinarian(RegisterVeterinarianDto dto)
        {
            var result = await _authService.RegisterVeterinarianAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Veteriner kaydı başarılı.");
        }

        [HttpPost("register-owner")]
        public async Task<IActionResult> RegisterOwner(RegisterOwnerDto dto)
        {
            var result = await _authService.RegisterOwnerAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Hayvan sahibi kaydı başarılı.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Geçersiz bilgiler");

            return Ok(new { token });
        }
    }
}
