using SmartPetProject.DtoLayer.Dtos.VeterinarianDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IVeterinarianService
    {
        Task<List<VeterinarianListDto>> GetAllVeterinariansAsync();
    }
}
