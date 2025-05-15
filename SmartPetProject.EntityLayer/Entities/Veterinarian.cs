using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class Veterinarian:BaseEntity
    {
        public string ClinicName { get; set; }
        public string LicenseNumber { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
