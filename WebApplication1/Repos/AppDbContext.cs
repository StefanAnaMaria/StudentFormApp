using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using WebApplication1.Models;

namespace WebApplication1.Repos;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<StudentForm> StudentForms { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurarea relațiilor între entități
        modelBuilder.Entity<StudentForm>()
            .HasOne(s => s.User)
            .WithMany(u => u.ListaStudentForms)
            .HasForeignKey("IdUser")
            .IsRequired(true);
            


         // Relația User -> Role (FK: IdRole)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey("IdRole")
            .IsRequired(false);
            // .OnDelete(DeleteBehavior.SetNull);

       
        // Configurarea cheii primare pentru Role
        modelBuilder.Entity<Role>()
            .HasKey(r => r.IdRole);


        modelBuilder.Entity<User>()
            .HasKey(u => u.IdUser);

    }
}
