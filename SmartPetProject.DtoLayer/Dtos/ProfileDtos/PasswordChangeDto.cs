using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.ProfileDtos
{
    public class PasswordChangeDto
    {
        public string email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
