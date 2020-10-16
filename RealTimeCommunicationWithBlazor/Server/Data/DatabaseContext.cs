using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealTimeCommunicationWithBlazor.Shared;

namespace RealTimeCommunicationWithBlazor.Server.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<RealTimeCommunicationWithBlazor.Shared.Blog> Blog { get; set; }

        public DbSet<RealTimeCommunicationWithBlazor.Shared.Comment> Comment { get; set; }
    }
}
