using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GDC_API.Models;

namespace GDC_API.Data
{
    public class MySQLDatabaseContext : DbContext
    {
        public MySQLDatabaseContext (DbContextOptions<MySQLDatabaseContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
