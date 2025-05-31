using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.VaccinationCardDtos
{
    public class VaccinationCardUpdateDto
    {
        public string Id { get; set; }
        public string AnimalId { get; set; }
        public string VaccinationId { get; set; }
    }
}
