using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        

        public string weight { get; set; }
        
        public string AnimalOwnerId { get; set; }
        [ForeignKey("AnimalOwnerId")]
        public AnimalOwner AnimalOwner { get; set; }

       
        public string AnimalSpeciesId { get; set; }
        [ForeignKey("AnimalSpeciesId")]
        public AnimalSpecies AnimalSpecies { get; set; }
    }
}
