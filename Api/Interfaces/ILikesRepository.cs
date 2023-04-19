using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Helpers;

namespace Api.Interfaces
{
    public interface ILikesRepository
	{
		Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
		Task<AppUser> GetUserWithLikes(int userId);
		Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
	}
}

