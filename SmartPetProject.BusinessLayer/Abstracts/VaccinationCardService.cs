using Microsoft.EntityFrameworkCore;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.DataAccessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.VaccinationCardDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class VaccinationCardService : IVaccinationCardService
    {
        private readonly IRepository<VaccinationCard> _repository;
        private readonly IRepository<Vaccination> _vaccinationRepository;
        private readonly AppDbContext _context;
        public VaccinationCardService(IRepository<VaccinationCard> repository, IRepository<Vaccination> vaccinationRepository, AppDbContext context)
        {
            _repository = repository;
            _vaccinationRepository = vaccinationRepository;
            _context = context;
        }

        public async Task AddAsync(VaccinationCardCreateDto vaccinationCreateDto)
        {
            var nextVaccinationDate = await _vaccinationRepository.GetByIdAsync(vaccinationCreateDto.VaccinationId);
            await _repository.AddAsync(new VaccinationCard
            {
                Id= Guid.NewGuid().ToString(),
                AnimalId = vaccinationCreateDto.AnimalId,
                VaccinationId = vaccinationCreateDto.VaccinationId,
                UpdatedDate = DateTime.Now,
                NextVaccinationDate= DateTime.Now.AddDays(nextVaccinationDate.NextVaccination)
            });
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<VaccinationCardResultDto>> GetAllAsync()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new VaccinationCardResultDto
            {
                Id = x.Id,
                AnimalId = x.AnimalId,
                VaccinationId = x.VaccinationId,
                NextVaccinationDate = x.NextVaccinationDate,
                UpdatedDate = x.UpdatedDate,
                CreatedDate = x.CreatedDate
            });
        }

        public async Task<VaccinationCardResultDto> GetByIdAsync(string id)
        {
            var value = await _repository.GetByIdAsync(id);
         
            return new VaccinationCardResultDto
            {
                Id = value.Id,
                AnimalId = value.AnimalId,
                VaccinationId = value.VaccinationId,
                NextVaccinationDate = value.NextVaccinationDate,
                UpdatedDate = value.UpdatedDate,
                CreatedDate = value.CreatedDate
            };
        }

        public async Task Remove(string id)
        {
            var value = await _repository.GetByIdAsync(id);
            if (value != null)
            {
                _repository.Remove(value);
            }
            else
            {
                throw new KeyNotFoundException("Vaccination card not found.");
            }
            await _repository.SaveChangesAsync();
        }

        public async Task Update(VaccinationCardUpdateDto vaccinationUpdateDto)
        {
           _repository.Update(new VaccinationCard
            {
                Id = vaccinationUpdateDto.Id,
                AnimalId = vaccinationUpdateDto.AnimalId,
                VaccinationId = vaccinationUpdateDto.VaccinationId,
                UpdatedDate = DateTime.Now
            });

            await _repository.SaveChangesAsync();
        }

        public async Task<List<VaccinationCardDto>> GetVaccinationCardsByOwnerIdAsync(string ownerId)
        {
            var cards = await _context.VaccinationCards
                .Include(vc => vc.Animal)
                .Include(vc => vc.Vaccination)
                .Where(vc => vc.Animal.AnimalOwnerId == ownerId)
                .Select(vc => new VaccinationCardDto
                {
                    AnimalId = vc.AnimalId,
                    AnimalName = vc.Animal.Name,
                    VaccinationId = vc.VaccinationId,
                    VaccinationName = vc.Vaccination.Name,
                    NextVaccinationDate = vc.NextVaccinationDate
                })
                .ToListAsync();

            return cards;
        }

    }
}
