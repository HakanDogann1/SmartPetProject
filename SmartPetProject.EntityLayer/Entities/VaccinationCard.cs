using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class VaccinationCard:BaseEntity
    {
        public string AnimalId { get; set; }
        public Animal Animal { get; set; }

        public string VaccinationId { get; set; }
        public Vaccination Vaccination { get; set; }

        public DateTime NextVaccinationDate { get; set; }


    }
}
