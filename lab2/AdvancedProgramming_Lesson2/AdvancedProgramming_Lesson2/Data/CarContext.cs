using AdvancedProgramming_Lesson2.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming_Lesson2.Data
{
    public class CarContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {
        }
    }
}
