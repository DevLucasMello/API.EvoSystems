using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Evo.API.Models;

namespace Evo.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
          : base(options)
        { }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
