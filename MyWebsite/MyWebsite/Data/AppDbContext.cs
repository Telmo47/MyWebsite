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
    }
}
