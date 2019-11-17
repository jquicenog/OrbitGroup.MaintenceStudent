using Microsoft.EntityFrameworkCore;
using OrbitGroup.MaintenceStudent.Core.Models;

namespace OrbitGroup.MaintenceStudent.Dal
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Student> Student { get; set; }
    }
}
