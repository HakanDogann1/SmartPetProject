using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.ProfileDtos
{
    public class AnimalOwnerUpdateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AnimalOwnerName { get; set; }
        public string AnimalOwnerSurname { get; set; }
        public string Address { get; set; }
        public string? Picture { get; set; }

    }
}
