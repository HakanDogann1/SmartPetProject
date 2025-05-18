using Microsoft.AspNetCore.SignalR;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.EntityLayer.Entities;

namespace SmartPetProject.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content, message.SentDate);
        }
    }
}
