using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPetProject.BusinessLayer.Interfaces;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinarianController : ControllerBase
    {
        private readonly IVeterinarianService _veterinarianService;

        public VeterinarianController(IVeterinarianService veterinarianService)
        {
            _veterinarianService = veterinarianService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVeterinarians()
        {
            var list = await _veterinarianService.GetAllVeterinariansAsync();
            return Ok(list);
        }
    }
}
