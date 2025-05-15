using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.DtoLayer.Dtos.AppointmentDtos;
using SmartPetProject.EntityLayer.Entities;
using System.Security.Claims;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly AppDbContext _context;

        public AppointmentController(IAppointmentService appointmentService, AppDbContext context)
        {
            _appointmentService = appointmentService;
            _context = context;
        }

        // Tüm randevuları listele (sadece Owner görebilir)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!IsPetOwner())
                return StatusCode(403, "Sadece hayvan sahipleri bu veriye erişebilir.");

            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }


        // Randevu oluştur (sadece Owner erişebilir)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateDto dto)
        {
            if (!IsPetOwner())
                return StatusCode(403, "Sadece hayvan sahipleri randevu alabilir.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var owner = await _context.AnimalOwners.FirstOrDefaultAsync(x => x.UserId == userId);
            if (owner == null) return BadRequest("Hayvan sahibi bulunamadı.");

            if (!TimeSpan.TryParse(dto.AppointmentTime, out var parsedTime))
                return BadRequest("Randevu saati formatı hatalı. Örn: '09:30'");

            var isAvailable = await _appointmentService.IsAppointmentSlotAvailableAsync(
                dto.AppointmentDate, parsedTime, dto.VeterinarianId);

            if (!isAvailable) return BadRequest("Bu saat diliminde randevu dolu.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid().ToString(),
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = parsedTime,
                DurationInMinutes = 30,
                VeterinarianId = dto.VeterinarianId,
                AnimalOwnerId = owner.Id
            };

            await _appointmentService.AddAppointmentAsync(appointment);
            return Ok("Randevu oluşturuldu.");
        }

        private bool IsPetOwner()
        {
            var userTypeClaim = User.FindFirst("UserType")?.Value;
            return userTypeClaim == "Owner" || userTypeClaim == "PetOwner";
        }

        [HttpGet("upcoming/veterinarian")]
        public async Task<IActionResult> GetUpcomingAppointmentsForVeterinarian()
        {
            if (!IsUserType("veterinarian"))
                return StatusCode(403, "Sadece veteriner kullanıcılar erişebilir.");

            var userId = GetUserId();
            var result = await _appointmentService.GetUpcomingAppointmentsForVeterinarianAsync(userId);
            return Ok(result);
        }

        [HttpGet("upcoming/owner")]
        public async Task<IActionResult> GetUpcomingAppointmentsForOwner()
        {
            if (!IsUserType("owner") && !IsUserType("petowner"))
                return StatusCode(403, "Sadece hayvan sahibi kullanıcılar erişebilir.");

            var userId = GetUserId();
            var result = await _appointmentService.GetUpcomingAppointmentsForOwnerAsync(userId);
            return Ok(result);
        }

        [HttpGet("all/veterinarian")]
        public async Task<IActionResult> GetAllAppointmentsForVeterinarian()
        {
            if (!IsUserType("veterinarian"))
                return StatusCode(403, "Sadece veteriner kullanıcılar erişebilir.");

            var userId = GetUserId();
            var result = await _appointmentService.GetAllAppointmentsForVeterinarianAsync(userId);
            return Ok(result);
        }

        [HttpGet("all/owner")]
        public async Task<IActionResult> GetAllAppointmentsForOwner()
        {
            if (!IsUserType("owner") && !IsUserType("petowner"))
                return StatusCode(403, "Sadece hayvan sahibi kullanıcılar erişebilir.");

            var userId = GetUserId();
            var result = await _appointmentService.GetUpcomingAppointmentsForOwnerAsync(userId);
            return Ok(result);
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

