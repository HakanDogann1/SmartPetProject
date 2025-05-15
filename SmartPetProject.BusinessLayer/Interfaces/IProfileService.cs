using SmartPetProject.DtoLayer.Dtos.ProfileDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IProfileService
    {
        Task<bool> UpdateVeterinarianFullProfileAsync(string userId, VeterinarianUpdateDto dto);
        Task<bool> UpdateAnimalOwnerFullProfileAsync(string userId, AnimalOwnerUpdateDto dto);
        Task<IEnumerable<ApplicationUser>> GetAllUsersForAdminAsync();
        Task<VeterinarianProfileDto> GetVeterinarianProfileAsync(string userId);
        Task<AnimalOwnerProfileDto> GetAnimalOwnerProfileAsync(string userId);

    }
}
