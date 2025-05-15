using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.AuthDtos
{
    public class RegisterOwnerDto:BaseDto
    {
        public string Password { get; set; }
        public string Address { get; set; }
      
        public string? Description { get; set; }
    }
}
