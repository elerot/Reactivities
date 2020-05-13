using Domain;

namespace Applicaiton.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }

}