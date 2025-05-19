using Microsoft.AspNetCore.Identity;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.VeterinarianDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class VeterinarianService:IVeterinarianService
    {
        private readonly IRepository<Veterinarian> _veterinarianRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public VeterinarianService(IRepository<Veterinarian> veterinarianRepository, UserManager<ApplicationUser> userManager)
        {
            _veterinarianRepository = veterinarianRepository;
            _userManager = userManager;
        }


        public async Task<List<VeterinarianListDto>> GetAllVeterinariansAsync()
        {
            var vets = await _veterinarianRepository.GetAllAsync();
            var vetList = new List<VeterinarianListDto>();

            foreach (var v in vets)
            {
                var user = await _userManager.FindByIdAsync(v.UserId);
                if (user == null) continue;

                vetList.Add(new VeterinarianListDto
                {
                    Id = v.Id,
                    UserId = v.UserId,
                    Name = user.Name,
                    Surname = user.Surname,
                    ClinicName = v.ClinicName,
                    LicenseNumber = v.LicenseNumber
                });
            }

            return vetList;
        }
    }
}
