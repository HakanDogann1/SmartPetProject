using Microsoft.AspNetCore.Identity;
using SmartPetProject.BusinessLayer.Helpers;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.DtoLayer.Dtos.AuthDtos;
using SmartPetProject.EntityLayer.Entities;
using SmartPetProject.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(UserManager<ApplicationUser> userManager, AppDbContext context, ITokenHelper tokenHelper)
        {
            _userManager = userManager;
            _context = context;
            _tokenHelper = tokenHelper;
        }

        public async Task<IdentityResult> RegisterVeterinarianAsync(RegisterVeterinarianDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                UserType = UserType.Veterinarian,
                Picture = dto.Picture
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return result;

            var vet = new Veterinarian
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ClinicName = dto.ClinicName,
                LicenseNumber = dto.LicenseNumber,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Description = dto.Description
            };

            _context.Veterinarians.Add(vet);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IdentityResult> RegisterOwnerAsync(RegisterOwnerDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                UserType = UserType.PetOwner,
                Name = dto.Name,
                Surname = dto.Surname
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return result;

            var owner = new AnimalOwner
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                Address = dto.Address,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Animals = new List<Animal>()
            };

            _context.AnimalOwners.Add(owner);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<AccessToken> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return null;

            var token = _tokenHelper.CreateToken(user);

            return token;
        }
    }
}
