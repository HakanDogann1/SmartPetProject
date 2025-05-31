using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class Appointment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        [Required]
        public int DurationInMinutes { get; set; } = 30;

        [Required]
        public string AnimalOwnerId { get; set; }

        [ForeignKey(nameof(AnimalOwnerId))]
        public AnimalOwner AnimalOwner { get; set; }

        [Required]
        public string VeterinarianId { get; set; }

        [ForeignKey(nameof(VeterinarianId))]
        public Veterinarian Veterinarian { get; set; }

        public string AnimalId { get; set; }
        [ForeignKey(nameof(AnimalId))]
        public Animal Animal { get; set; }
        public Room Room { get; set; }
        public bool Status { get; set; }

        public string? Note { get; set; }
    }
}
