
using DemoCRUDASPCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoCRUDASPCore.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
        }
        public DbSet<Person> Person { get; set; }
    }
}
