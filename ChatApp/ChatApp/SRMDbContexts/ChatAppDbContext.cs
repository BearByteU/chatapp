using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.SRMDbContexts
{
    public class ChatAppDbContext : DbContext
    {
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                email = "umesh@gmail.com",
                Name = "Umesh",
                password = "Code@123$",
                username = "Alpha__D"
            });

        }

        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}
        

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
