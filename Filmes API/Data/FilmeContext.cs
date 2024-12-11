using Filmes_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmes_API.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> ops): base(ops) 
        { 

        }

        public DbSet<Filme> Filmes { get; set; }
    }
}
