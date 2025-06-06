using Microsoft.EntityFrameworkCore;
using WiiZoneNowy.Models;

namespace WiiZoneNowy.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        
        public DbSet<Game>        Games        { get; set; }
        public DbSet<Client>      Clients      { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Tag>         Tags         { get; set; }
        public DbSet<GameTag>     GameTags     { get; set; }
        
        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);
            
            b.Entity<GameTag>()
                .HasKey(gt => new { gt.GameId, gt.TagId });

            b.Entity<GameTag>()
                .HasOne(gt => gt.Game)
                .WithMany(g => g.GameTags)
                .HasForeignKey(gt => gt.GameId);

            b.Entity<GameTag>()
                .HasOne(gt => gt.Tag)
                .WithMany(t => t.GameTags)
                .HasForeignKey(gt => gt.TagId);
        }
    }
}