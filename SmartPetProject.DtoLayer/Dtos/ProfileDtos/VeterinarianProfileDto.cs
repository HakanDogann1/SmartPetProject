using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.ProfileDtos
{
    public class VeterinarianProfileDto:ApplicationUserProfileDto
    {
        public string ClinicName { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
