using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class VaccinationService : IVaccinationService
    {
        private readonly IRepository<Vaccination> _repository;

        public VaccinationService(IRepository<Vaccination> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(VaccinationCreateDto vaccinationCreateDto)
        {
            await _repository.AddAsync(new Vaccination
            {
                Id=Guid.NewGuid().ToString(),
                AnimalSpeciesId = vaccinationCreateDto.AnimalSpeciesId,
                NextVaccination = vaccinationCreateDto.NextVaccination,
                Name = vaccinationCreateDto.Name,
                CreatedDate= DateTime.Now,
                UpdatedDate = DateTime.Now
            });
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<VaccinationResultDto>> GetAllAsync()
        {
            var values = await _repository.GetAllAsync();

            return values.Select(v => new VaccinationResultDto
            {
                Id = v.Id,
                AnimalSpeciesId = v.AnimalSpeciesId,
                Name = v.Name,
                NextVaccination = v.NextVaccination,
                CreatedDate = v.CreatedDate,
                UpdatedDate = v.UpdatedDate
            });
        }

        public async Task<VaccinationResultDto> GetByIdAsync(string id)
        {
            var value =await _repository.GetByIdAsync(id);
            return new VaccinationResultDto 
            { 
                Name=value.Name,
                NextVaccination=value.NextVaccination,
                AnimalSpeciesId = value.AnimalSpeciesId,
                Id = value.Id,
                CreatedDate=value.CreatedDate,
                UpdatedDate=value.UpdatedDate
            };
        }

        public async Task Remove(string id)
        {
            var value = await _repository.GetByIdAsync(id);
            _repository.Remove(value);
            await _repository.SaveChangesAsync();
        }

        public async Task Update(VaccinationUpdateDto vaccinationUpdateDto)
        {
            _repository.Update(new Vaccination
            {
                Id = vaccinationUpdateDto.Id,
                AnimalSpeciesId = vaccinationUpdateDto.AnimalSpeciesId,
                Name = vaccinationUpdateDto.Name,
                NextVaccination = vaccinationUpdateDto.NextVaccination,
                UpdatedDate = DateTime.Now
            });
            await _repository.SaveChangesAsync();
        }
    }
}
