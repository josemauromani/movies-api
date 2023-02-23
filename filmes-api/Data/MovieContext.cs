
using Microsoft.EntityFrameworkCore;
using movies_api.Models;

namespace movies_api.Data;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> opts) : base(opts)
    {

    }

    public DbSet<Movie> Movies { get; set; }
}

