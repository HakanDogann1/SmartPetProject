using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.VaccinationDtos
{
    public class VaccinationUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AnimalSpeciesId { get; set; }
        public int NextVaccination { get; set; }
        
    }
}
