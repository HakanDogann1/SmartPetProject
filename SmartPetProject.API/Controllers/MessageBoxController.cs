using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.EntityLayer.Enums;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartPetProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessageBoxController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessageBoxController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/messagebox/contacts
        [HttpGet("contacts")]
        public async Task<IActionResult> GetContacts()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            var currentUser = await _context.Users.FindAsync(currentUserId);
            if (currentUser == null)
                return NotFound();

            // Kullanıcının gönderdiği veya aldığı mesajlardaki diğer kullanıcıların Id'leri
            var contactIds = await _context.Messages
                .Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
                .Select(m => m.SenderId == currentUserId ? m.ReceiverId : m.SenderId)
                .Distinct()
                .ToListAsync();

            // İzin verilen mesajlaşma türü: Vet <-> Owner
            var currentIsVet = currentUser.UserType == UserType.Veterinarian;

            var contacts = await _context.Users
                .Where(u => contactIds.Contains(u.Id))
                .Where(u => (u.UserType == UserType.Veterinarian && !currentIsVet) || (u.UserType != UserType.Veterinarian && currentIsVet))
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Surname,
                    u.UserType,
                    u.Email,
                    u.PhoneNumber
                })
                .ToListAsync();

            return Ok(contacts);
        }
    }
}
