using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.VaccinationDtos
{
    public class VaccinationResultDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int NextVaccination { get; set; }
        public string AnimalSpeciesId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
