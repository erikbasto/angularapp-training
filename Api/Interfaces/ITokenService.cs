using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ITokenService
	{
		Task<string> CreateToken(AppUser user);
	}
}

