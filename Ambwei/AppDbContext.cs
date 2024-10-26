using Ambwei.Models;
using Microsoft.EntityFrameworkCore;

namespace Ambwei
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
               .HasKey(l => l.location_id); // Definindo a chave primária

            modelBuilder.Entity<Location>()
                .Property(l => l.location_id)
                .ValueGeneratedOnAdd();

            // Configurando Pacient
            modelBuilder.Entity<Pacient>()
                .HasKey(p => p.pacient_id);
            modelBuilder.Entity<Pacient>()
                .Property(p => p.pacient_id)
                .ValueGeneratedOnAdd(); // Auto-incremento, se necessário

            // Configurando Process
            modelBuilder.Entity<Process>()
                .HasKey(pr => pr.process_id);
            modelBuilder.Entity<Process>()
                .Property(pr => pr.process_id)
                .ValueGeneratedOnAdd(); // Auto-incremento, se necessário

            // Configurando Role
            modelBuilder.Entity<Role>()
                .HasKey(r => r.role_id);
            modelBuilder.Entity<Role>()
                .Property(r => r.role_id)
                .ValueGeneratedOnAdd(); // Auto-incremento, se necessário

            // Configurando Task
            modelBuilder.Entity<Models.Task>()
                .HasKey(t => t.task_id);
            modelBuilder.Entity<Models.Task>()
                .Property(t => t.task_id)
                .ValueGeneratedOnAdd(); // Auto-incremento, se necessário

            // Configurando User
            modelBuilder.Entity<User>()
                .HasKey(u => u.user_id);
            modelBuilder.Entity<User>()
                .Property(u => u.user_id)
                .ValueGeneratedOnAdd(); // Auto-incremento, se necessário

            // Configurando relacionamentos (Foreign Keys)
            modelBuilder.Entity<Process>()
                .HasOne(pr => pr.Paciente)
                .WithMany()
                .HasForeignKey(pr => pr.pacient_id);

            modelBuilder.Entity<Process>()
                .HasOne(pr => pr.Usuario)
                .WithMany()
                .HasForeignKey(pr => pr.user_id);

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.user_id);

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Processo)
                .WithMany()
                .HasForeignKey(t => t.process_id);

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Local)
                .WithMany()
                .HasForeignKey(t => t.location_id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.role_id);
        }
    }
}
