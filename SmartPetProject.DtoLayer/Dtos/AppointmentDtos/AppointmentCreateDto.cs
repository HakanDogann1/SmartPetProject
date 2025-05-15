using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.AppointmentDtos
{
    public class AppointmentCreateDto
    {
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string VeterinarianId { get; set; }
    }
}
