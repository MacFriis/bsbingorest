using System;
using Microsoft.AspNetCore.Identity;

namespace BSBingoApi.Model
{
    public class UserRoleEntity : IdentityRole<Guid>
    {
        public UserRoleEntity() : base()
        {
        }

        public UserRoleEntity(string roleName) : base(roleName)
        {

        }
    }
}
