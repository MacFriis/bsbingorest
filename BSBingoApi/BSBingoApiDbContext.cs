using System;
using BSBingoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BSBingoApi
{
    public class BSBingoApiDbContext : DbContext
    {


        public BSBingoApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BSWordEntity> BSWords { get; set; }
    }
}
