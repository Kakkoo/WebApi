using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class NestleDbContext : DbContext
    {
        public NestleDbContext(DbContextOptions<NestleDbContext> options)
            : base(options)
        {

        }

        public DbSet<tblUsers> tblUsers { get; set; }
    }
}
