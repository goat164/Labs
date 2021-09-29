using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
    }
}