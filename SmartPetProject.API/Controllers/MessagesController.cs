using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using SmartPetProject.API.Hubs;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SmartPetProject.EntityLayer.Enums;

namespace SmartPetProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _chatHub;

        public MessagesController(AppDbContext context, IHubContext<ChatHub> chatHub)
        {
            _context = context;
            _chatHub = chatHub;
        }

        // DTO sınıfı controller içinde ya da ayrı dosyada olabilir
        public class SendMessageDto
        {
            public string ReceiverId { get; set; }
            public string Content { get; set; }
        }

        [HttpGet("history/{otherUserId}")]
        public async Task<IActionResult> GetMessageHistory(string otherUserId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            var currentUser = await _context.Users.FindAsync(currentUserId);
            var otherUser = await _context.Users.FindAsync(otherUserId);

            if (currentUser == null || otherUser == null)
                return NotFound("User not found.");

            bool currentIsVet = currentUser.UserType == UserType.Veterinarian;
            bool otherIsVet = otherUser.UserType == UserType.Veterinarian;

            if (currentIsVet == otherIsVet)
                return BadRequest("Messaging allowed only between Veterinarian and Owner.");

            var messages = await _context.Messages
                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == otherUserId) ||
                            (m.SenderId == otherUserId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.SentDate)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(senderId))
                return Unauthorized();

            var sender = await _context.Users.FindAsync(senderId);
            var receiver = await _context.Users.FindAsync(dto.ReceiverId);

            if (sender == null || receiver == null)
                return NotFound("Sender or Receiver not found.");

            bool senderIsVet = sender.UserType == UserType.Veterinarian;
            bool receiverIsVet = receiver.UserType == UserType.Veterinarian;

            if (senderIsVet == receiverIsVet)
                return BadRequest("Messages are allowed only between Veterinarian and Owner users.");

            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                SentDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            await _chatHub.Clients.User(dto.ReceiverId).SendAsync("ReceiveMessage", senderId, dto.Content, message.SentDate);

            return Ok(new { message = "Message sent successfully", messageId = message.Id });
        }
    }
}
