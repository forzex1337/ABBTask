using ABBTask.Data.Schema.Entities;
using ABBTask.Data.Schema.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ABBTask.Data.Data
{
    public class ABBTaskDbContext : DbContext
    {
        public ABBTaskDbContext(DbContextOptions<ABBTaskDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EstateConfiguration());
        }

        public DbSet<Estate> Estates { get; set; }
    }
}
