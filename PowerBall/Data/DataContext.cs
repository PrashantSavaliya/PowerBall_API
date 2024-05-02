using Microsoft.EntityFrameworkCore;
using PowerBall.Models;

namespace PowerBall.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<RegisterLogin> RegisterLogins { get; set; }
        public DbSet<Games> Game { get; set; }
        public DbSet<Draw> Draw { get; set; }
        public DbSet<DrawWinners> DrawWinners { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<DataList> DataList { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Games>()
                .HasOne(g => g.Tickets)
                .WithMany(t => t.Game)
                .HasForeignKey(g => g.TicketID);
        }

    }
}
