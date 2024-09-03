using Microsoft.EntityFrameworkCore;
using PokerManager.API.Models;
using PokerManager.API.Data.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PokerManager.API.Data
{
    public class PokerManagerContext : IdentityDbContext<IdentityUser>
    {
        public PokerManagerContext(DbContextOptions<PokerManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
        }
    }
}