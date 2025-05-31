using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.AuthDtos
{
    public class RegisterVeterinarianDto:BaseDto
    {
        public string Password { get; set; }
        public string ClinicName { get; set; }
        public string LicenseNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Picture { get; set; }
        public string? Description { get; set; }
    }
}
