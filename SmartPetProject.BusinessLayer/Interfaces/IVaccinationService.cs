using SmartPetProject.DtoLayer.Dtos.VaccinationDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IVaccinationService
    {
        Task<VaccinationResultDto> GetByIdAsync(string id);
        Task<IEnumerable<VaccinationResultDto>> GetAllAsync();

        Task AddAsync(VaccinationCreateDto vaccinationCreateDto);
        Task Update(VaccinationUpdateDto vaccinationUpdateDto);
        Task Remove(string id);
    }
}
