using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class AnimalSpecies:BaseEntity
    {
        public string Name { get; set; }

        public List<Vaccination> Vaccinations { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
