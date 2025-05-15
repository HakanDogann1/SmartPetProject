using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class Animal:BaseEntity
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Genus { get; set; }
        public string Type { get; set; }
        public string weight { get; set; }

        public string AnimalOwnerId { get; set; }
        public AnimalOwner AnimalOwner { get; set; }
    }
}
