using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class Entity
    {
        public int EntityId { get; set; }
        public string Foo { get; set; }
    }

    public class EfContext : DbContext
    {
        public EfContext() { }

        public EfContext(DbContextOptions<EfContext> options) : base(options) { }

        public DbSet<Entity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"host=localhost;port=5469;database=test_db;user id=postgres;password=password1;");
            }
        }
    }
}
