using Microsoft.EntityFrameworkCore;

namespace MvcFilmes.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    public DbSet<Filme> Filmes { get; set; }
}
