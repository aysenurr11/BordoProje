using BordoProje.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BordoProje.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Answer> Answers { get; set; }
        //public object Answer { get; internal set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }

        public DbSet<ExistingTask> ExistingTasks{ get; set; }

    }
}
