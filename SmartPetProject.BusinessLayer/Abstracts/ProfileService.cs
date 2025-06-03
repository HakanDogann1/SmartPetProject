using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Context;
using SmartPetProject.DtoLayer.Dtos.ProfileDtos;
using SmartPetProject.EntityLayer.Entities;
using SmartPetProject.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class ProfileService:IProfileService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> UpdateVeterinarianFullProfileAsync(string userId, VeterinarianUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.PhoneNumber = dto.PhoneNumber;
            user.Picture = dto.Picture;
            await _userManager.UpdateAsync(user);

            var vet = await _context.Veterinarians.FirstOrDefaultAsync(v => v.UserId == userId);
            if (vet == null) return false;

           
            vet.ClinicName = dto.ClinicName;
            vet.LicenseNumber = dto.LicenseNumber;
            vet.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAnimalOwnerFullProfileAsync(string userId, AnimalOwnerUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Picture = dto.Picture;
            await _userManager.UpdateAsync(user);

            var owner = await _context.AnimalOwners.FirstOrDefaultAsync(o => o.UserId == userId);
            if (owner == null) return false;

            
            owner.Address = dto.Address;
            owner.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersForAdminAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<VeterinarianProfileDto> GetVeterinarianProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.UserType != UserType.Veterinarian)
                return null;

            var vet = await _context.Veterinarians.FirstOrDefaultAsync(v => v.UserId == userId);
            if (vet == null) return null;

            return new VeterinarianProfileDto
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                ClinicName = vet.ClinicName,
                LicenseNumber = vet.LicenseNumber,
                Picture = user.Picture
            };
        }

        public async Task<AnimalOwnerProfileDto> GetAnimalOwnerProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.UserType != UserType.PetOwner)
                return null;

            var owner = await _context.AnimalOwners.FirstOrDefaultAsync(o => o.UserId == userId);
            if (owner == null) return null;

            return new AnimalOwnerProfileDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Name=user.Name,
                Surname = user.Surname,
                Id = owner.Id,
                PhoneNumber = user.PhoneNumber,
                Address = owner.Address,
                Picture = user.Picture
,            };
        }

        public async Task<bool> PasswordChangeAsync(string userId ,PasswordChangeDto passwordChangeDto)
        {
            if(passwordChangeDto == null || string.IsNullOrEmpty(userId) ||
                string.IsNullOrEmpty(passwordChangeDto.Password) || string.IsNullOrEmpty(passwordChangeDto.NewPassword))
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            
            var passwordCheck = await _userManager.CheckPasswordAsync(user, passwordChangeDto.Password);
            if (!passwordCheck)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(passwordChangeDto.NewPassword) ||passwordChangeDto.NewPassword.Length < 8 ||!passwordChangeDto.NewPassword.Any(char.IsUpper)
                || !passwordChangeDto.NewPassword.Any(char.IsLower) || !passwordChangeDto.NewPassword.Any(char.IsDigit) || !passwordChangeDto.NewPassword.Any(c => "!@#$%^&*()_+-=[]{}|;:',.<>/?".Contains(c)))
                return false;

           
            var value =await _userManager.ChangePasswordAsync(
                await _userManager.FindByIdAsync(userId),
                passwordChangeDto.Password,
                passwordChangeDto.NewPassword
            );
            await _userManager.UpdateSecurityStampAsync(user);
            return true;

        }
    }
}
