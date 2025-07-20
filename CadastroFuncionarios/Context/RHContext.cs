using Funcionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace Funcionarios.Context
{
    public class RHContext : DbContext
    {
        public RHContext(DbContextOptions<RHContext>options) : base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
