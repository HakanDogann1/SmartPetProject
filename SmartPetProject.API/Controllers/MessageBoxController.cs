using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.DtoLayer.Dtos.MessageDtos;
using SmartPetProject.EntityLayer.Entities;
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

            var users = await _context.Users
    .Where(u => contactIds.Contains(u.Id))
    .Where(u => (u.UserType == UserType.Veterinarian && !currentIsVet) ||
                (u.UserType != UserType.Veterinarian && currentIsVet))
    .ToListAsync();

            var contacts = new List<MessageBoxDto>();

            foreach (var u in users)
            {
                var lastMessage = await _context.Messages
                    .Where(m => (m.SenderId == currentUserId && m.ReceiverId == u.Id) ||
                                (m.SenderId == u.Id && m.ReceiverId == currentUserId))
                    .OrderByDescending(m => m.SentDate)
                    .Select(m => m.Content)
                    .FirstOrDefaultAsync();

                contacts.Add(new MessageBoxDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    UserType = u.UserType,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Message = lastMessage
                });
            }

            contacts.Reverse();

            return Ok(contacts);
        }
    }
}
