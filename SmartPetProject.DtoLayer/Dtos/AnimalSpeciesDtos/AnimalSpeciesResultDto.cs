﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DtoLayer.Dtos.AnimalSpeciesDtos
{
    public class AnimalSpeciesResultDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
