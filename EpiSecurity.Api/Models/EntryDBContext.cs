using Microsoft.EntityFrameworkCore;

namespace EpiSecurity.Api.Models
{
    public class EntryDBContext : DbContext
    {
        //The configuration of EntryDBContext
        public EntryDBContext(DbContextOptions<EntryDBContext> options)
            : base(options)
        {
        }

        //Create a DBSet property that will represent the collection of Entries
        public DbSet<Entry> Entries { get; set; }
    }
}
