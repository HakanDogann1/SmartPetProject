using SmartPetProject.DtoLayer.Dtos.VaccinationCardDtos;
using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IVaccinationCardService
    {
        Task<VaccinationCardResultDto> GetByIdAsync(string id);
        Task<IEnumerable<VaccinationCardResultDto>> GetAllAsync();

        Task AddAsync(VaccinationCardCreateDto vaccinationCreateDto);
        Task Update(VaccinationCardUpdateDto vaccinationUpdateDto);
        Task<List<VaccinationCardDto>> GetVaccinationCardsByOwnerIdAsync(string ownerId);

        Task Remove(string id);
    }
}
