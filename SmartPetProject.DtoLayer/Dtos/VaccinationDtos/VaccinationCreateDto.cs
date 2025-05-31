using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.VaccinationDtos
{
    public class VaccinationCreateDto
    {
        public string Name { get; set; }
        public string AnimalSpeciesId { get; set; }
        public int NextVaccination { get; set; }
    }
}
