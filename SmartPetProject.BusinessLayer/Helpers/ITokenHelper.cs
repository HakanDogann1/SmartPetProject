
using SmartPetProject.EntityLayer.Entities;

namespace SmartPetProject.BusinessLayer.Helpers
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(ApplicationUser user);
        RefreshToken CreateRefreshToken(ApplicationUser user, string ipAddress);
    }
}
