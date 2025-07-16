using Microsoft.EntityFrameworkCore;
using TrabajadoresApp.Models;
using TrabajadoresAPP.Models;

namespace TrabajadoresApp.Data
{
    public class TrabajadoresDbContext : DbContext
    {
        public TrabajadoresDbContext(DbContextOptions<TrabajadoresDbContext> options)
            : base(options) { }

        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
    }
}
