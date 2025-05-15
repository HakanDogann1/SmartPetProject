using Microsoft.AspNetCore.Identity;
using SmartPetProject.BusinessLayer.Helpers;
using SmartPetProject.DtoLayer.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterVeterinarianAsync(RegisterVeterinarianDto dto);
        Task<IdentityResult> RegisterOwnerAsync(RegisterOwnerDto dto);
        Task<AccessToken> LoginAsync(LoginDto dto);
    }
}
