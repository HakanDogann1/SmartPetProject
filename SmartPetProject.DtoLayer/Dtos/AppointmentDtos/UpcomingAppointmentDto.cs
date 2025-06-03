using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.AppointmentDtos
{
    public class UpcomingAppointmentDto
    {
        public string Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
       public string VeterinarianId { get; set; }
       public string AnimalOwnerId { get; set; }
       public Room Room { get; set; }
        public string VeterinarianName { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Genus { get; set; }
        public string weight { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
    }
}
