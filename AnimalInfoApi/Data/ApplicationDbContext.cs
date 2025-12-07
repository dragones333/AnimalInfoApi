using Microsoft.EntityFrameworkCore;
using AnimalInfoApi.Models;

namespace AnimalInfoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animales { get; set; }
    }
}