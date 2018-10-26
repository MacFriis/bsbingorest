using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BSBingoApi.Model;

namespace BSBingoApi.Services
{
    public interface IUsersService
    {
        Task<PagedResults<User>> GetUsersAsync(
            PagingOptions pagingOptions,
            SortOptions<User, UserEntity> sortOptions,
            SearchOptions<User, UserEntity> searchOptions
        );

        Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm form);

        Task<Guid?> GetUserIdAsync(ClaimsPrincipal principal);

        Task<User> GetUserByIdAsync(Guid userId);

        Task<User> GetUserAsync(ClaimsPrincipal user);

    }
}
