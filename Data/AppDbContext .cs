using Microsoft.EntityFrameworkCore;
using Minimal_API.Models;

namespace Minimal_API.Data
{
    //public class AppDbContext : DbContext
    //{
    //    public AppDbContext(DbContextOptions<AppDbContext> options)
    //        : base(options)
    //    {
    //    }

        //public DbSet<Tareas> Tareas { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MINIMAL_DB;Trusted_Connection=True;");
        //}

        public interface IMinimal_APIData : IDisposable
        {
            DbSet<Tareas> Tareas { get; set; } // Tareas

            int SaveChanges();
        }

        public class Minimal_APIData : DbContext, IMinimal_APIData
        {
            public DbSet<Tareas> Tareas { get; set; } // Tareas

            private IConfigurationRoot configuration;

            public Minimal_APIData()
            {

            }

            public Minimal_APIData(IConfigurationRoot _configuration)
            {
                this.configuration = _configuration;
            }

            public override void Dispose()
            {
                base.Dispose();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Integrated Security=True;Trust Server Certificate=True");
            //Data Source=localhost\SQLExpress;Initial Catalog=master;Integrated Security=True;Trust Server Certificate=True
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                //modelBuilder.Configurations.Add(new auxiliar_dataConfiguration());
                modelBuilder.Entity<Tareas>(entity =>
                {
                    entity.ToTable("Tareas");
                    entity.HasKey(x => x.Id);

                    entity.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
                    entity.Property(x => x.Nombre).HasColumnName(@"nombre").HasMaxLength(50).IsRequired();
                    entity.Property(x => x.Finalizada).HasColumnName(@"finalizada").IsRequired();
                    entity.Property(x => x.Orden).HasColumnName(@"orden").IsRequired();
                });
            }
        }
    //}
}
