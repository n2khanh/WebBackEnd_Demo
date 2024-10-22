using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext(DbContextOptions<ZooContext> options) : base(options) { }
        public virtual DbSet<Animal> Animals { get; set; }  
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Cage> Cages { get; set; }  
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().ToTable(nameof(Thu));
            modelBuilder.Entity<Employee>().ToTable(nameof(NhanVien));
            modelBuilder.Entity<Cage>().ToTable(nameof(Chuong));
            modelBuilder.Entity<Food>().ToTable(nameof(ThucAn));
            modelBuilder.Entity<Event>().ToTable(nameof(SuKien));
        }
    }
}
