using ApiCubosPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubosPractica.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options)
        {
        }

        public DbSet<Cubos> Cubos { get; set; }
        public DbSet<Usuarios> Usuarios{ get; set; }
        public DbSet<Compra> Compras{ get; set; }

    }
}
