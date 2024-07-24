using Microsoft.EntityFrameworkCore;
using SchoolDbWeb.Models;

namespace SchoolDbWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClassStream> ClassStreams { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
