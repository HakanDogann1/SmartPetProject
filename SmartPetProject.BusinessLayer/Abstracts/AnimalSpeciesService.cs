using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.AnimalSpeciesDtos;
using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class AnimalSpeciesService : IAnimalSpeciesService
    {
        private readonly IRepository<AnimalSpecies> _animalSpeciesRepository;

        public AnimalSpeciesService(IRepository<AnimalSpecies> animalSpeciesRepository)
        {
            _animalSpeciesRepository = animalSpeciesRepository;
        }

        public async Task AddAsync(AnimalSpeciesCreateDto animalSpeciesCreateDto)
        {
            await _animalSpeciesRepository.AddAsync(new AnimalSpecies
            {
                Id = Guid.NewGuid().ToString(),
                Name = animalSpeciesCreateDto.Name,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
            await _animalSpeciesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AnimalSpeciesResultDto>> GetAllAsync()
        {
            var values = await _animalSpeciesRepository.GetAllAsync();

            return values.Select(v => new AnimalSpeciesResultDto
            {
                Id = v.Id,
                Name = v.Name,
                CreatedDate = v.CreatedDate,
                UpdatedDate = v.UpdatedDate
            });
        }

        public async Task<AnimalSpeciesResultDto> GetByIdAsync(string id)
        {
            var value = await _animalSpeciesRepository.GetByIdAsync(id);
            return new AnimalSpeciesResultDto
            {
                Name = value.Name,
                Id = value.Id,
                CreatedDate = value.CreatedDate,
                UpdatedDate = value.UpdatedDate
            };
        }

        public async Task Remove(string id)
        {
            var value = await _animalSpeciesRepository.GetByIdAsync(id);
            _animalSpeciesRepository.Remove(value);
            await _animalSpeciesRepository.SaveChangesAsync();
        }

        public async Task Update(AnimalSpeciesUpdateDto animalSpeciesUpdateDto)
        {
            _animalSpeciesRepository.Update(new AnimalSpecies
            {
                Id = animalSpeciesUpdateDto.Id,
                Name = animalSpeciesUpdateDto.Name,
                UpdatedDate = DateTime.Now
            });
            await _animalSpeciesRepository.SaveChangesAsync();
        }
    }
}
