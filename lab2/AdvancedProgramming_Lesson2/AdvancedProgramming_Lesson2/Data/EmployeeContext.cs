using AdvancedProgramming_Lesson2.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming_Lesson2.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
    }
}