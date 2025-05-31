using Microsoft.AspNetCore.Identity;
using SmartPetProject.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Picture { get; set; }
        public UserType UserType { get; set; }
    }
}
