using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johansfunction.Models;
using Microsoft.EntityFrameworkCore;

namespace Johansfunction.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AthleticPlayer> AthleticPlayers { get; set;}
    }
}
