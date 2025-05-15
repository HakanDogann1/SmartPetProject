using System;

namespace SmartPetProject.EntityLayer.Entities
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;

        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}
