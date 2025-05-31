using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Interfaces;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class AnimalService:IAnimalService
    {
        private readonly IRepository<Animal> _animalRepository;

        public AnimalService(IRepository<Animal> animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<IEnumerable<Animal>> GetAllByOwnerIdAsync(string ownerId)
        {
            var all = await _animalRepository.GetAllAsync();
            return all.Where(a => a.AnimalOwnerId == ownerId);
        }

        public async Task<Animal> GetByIdAsync(string id)
        {
            return await _animalRepository.GetByIdAsync(id);
        }

        public async Task AddAnimalAsync(Animal animal)
        {
            await _animalRepository.AddAsync(animal);
            await _animalRepository.SaveChangesAsync();
        }

        public async Task UpdateAnimalAsync(Animal animal)
        {
            _animalRepository.Update(animal);
            await _animalRepository.SaveChangesAsync();
        }

        public async Task DeleteAnimalAsync(Animal animal)
        {
            _animalRepository.Remove(animal);
            await _animalRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            var value = await _animalRepository.GetAllAsync();
            return value;
        }
    }
}
