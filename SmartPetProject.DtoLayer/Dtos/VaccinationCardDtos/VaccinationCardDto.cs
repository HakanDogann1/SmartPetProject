using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.VaccinationCardDtos
{
    public class VaccinationCardDto
    {
        public string AnimalId { get; set; }
        public string AnimalName { get; set; }
        public string VaccinationId { get; set; }
        public string VaccinationName { get; set; }
        public DateTime NextVaccinationDate { get; set; }
    }
}
