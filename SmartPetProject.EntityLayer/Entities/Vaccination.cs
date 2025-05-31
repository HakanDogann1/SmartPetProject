using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class Vaccination:BaseEntity
    {
        public string Name { get; set; }
        public int NextVaccination { get; set; }
        public string AnimalSpeciesId { get; set; }
        public AnimalSpecies AnimalSpecies { get; set; }
        public List<VaccinationCard> VaccinationCards { get; set; }
    }
}
