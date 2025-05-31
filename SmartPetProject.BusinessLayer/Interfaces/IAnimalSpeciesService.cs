using SmartPetProject.DtoLayer.Dtos.AnimalSpeciesDtos;
using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IAnimalSpeciesService
    {
        Task<AnimalSpeciesResultDto> GetByIdAsync(string id);
        Task<IEnumerable<AnimalSpeciesResultDto>> GetAllAsync();

        Task AddAsync(AnimalSpeciesCreateDto animalSpeciesCreateDto);
        Task Update(AnimalSpeciesUpdateDto animalSpeciesUpdateDto);
        Task Remove(string id);
    }
}
