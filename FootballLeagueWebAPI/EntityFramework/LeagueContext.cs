using FootballLeagueWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueWebAPI.EntityFramework
{
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Team>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Match>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();



            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatchesPlayed);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.GuestTeam)
                .WithMany(t => t.GuestMatchesPlayed);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players);
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
