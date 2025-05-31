using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAllByOwnerIdAsync(string ownerId);
        Task<Animal> GetByIdAsync(string id);
        Task AddAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(Animal animal);
        Task<IEnumerable<Animal>> GetAllAsync();
    }
}
