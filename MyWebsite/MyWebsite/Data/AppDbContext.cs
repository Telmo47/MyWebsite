using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Models;

namespace MyWebsite.Data
{

    /// <summary>
    /// Database context for the application.
    /// </summary>
    public class AppDbContext : DbContext
    {

        /// <summary>
        /// COnstructor for the AppDbContext.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {



        }

        /// <summary>
        /// Table for storing contact messages.
        /// </summary>
        public DbSet<ContactMessage> ContactMessages { get; set; }

        /// <summary>
        /// Table for storing projects.
        /// </summary>
        public DbSet<Projects> Projects { get; set; }

        /// <summary>
        /// Table for storing project technologies.
        /// </summary>
        public DbSet<ProjectTecnologies> ProjectTecnologies { get; set; }

        /// <summary>
        /// Table for storing technologies.
        /// </summary>
        public DbSet<Tecnologies> Tecnologies { get; set; }





        /// <summary>
        /// Function to configure the double key relationship between Projects and Tecnologies. Using fluent API.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Role
            var adminRoleId = "role-admin-id";
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                }
            );

            // Seed User
            var adminUserId = "user-admin-id";
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "Aa0_aa")
            };

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(r => new { r.UserId, r.RoleId }); 

            
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "Admin",
                    RoleId = "a"
                });


            
            modelBuilder.Entity<ProjectTecnologies>()
                .HasKey(pt => new { pt.ProjectId, pt.TecnologyId });

            modelBuilder.Entity<ProjectTecnologies>()
                .HasOne(pt => pt.Projects)
                .WithMany(p => p.ProjectTecnologies)
                .HasForeignKey(pt => pt.ProjectId);

            modelBuilder.Entity<ProjectTecnologies>()
                .HasOne(pt => pt.Tecnologies)
                .WithMany(t => t.ProjectTecnologies)
                .HasForeignKey(pt => pt.TecnologyId);
        }


    }


}
