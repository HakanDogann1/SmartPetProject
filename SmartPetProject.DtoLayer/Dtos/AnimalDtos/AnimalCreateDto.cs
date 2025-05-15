using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.AnimalDtos
{
    public class AnimalCreateDto
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Genus { get; set; }
        public string Type { get; set; }
        public string Weight { get; set; }
    }
}
