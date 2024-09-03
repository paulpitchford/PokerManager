using Microsoft.EntityFrameworkCore;
using PokerManager.API.Models;
using PokerManager.API.Data.EntityConfigurations;

namespace PokerManager.API.Data
{
    public class PokerManagerContext : DbContext
    {
        public PokerManagerContext(DbContextOptions<PokerManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
        }
    }
}