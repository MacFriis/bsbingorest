using System;
namespace BSBingoApi.Model
{
    public class UsersResponse : PagedCollection<User>
    {
        public Form Register { get; set; }
        public Link Me { get; set; }
    }
}
