using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.AppointmentDtos
{
    public class VeterinarianUpdateAppointmentDto
    {
        public string appointmentId { get; set; }
        public string? Note { get; set; }
    }
}
