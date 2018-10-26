using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BSBingoApi.Model
{
    public class UserDabaseContext : IdentityDbContext<UserEntity, UserRoleEntity, Guid>
    {
        public UserDabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
