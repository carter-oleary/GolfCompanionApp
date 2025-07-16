using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GolfCompanion.Data
{
    public class GolfDbContextFactory : IDesignTimeDbContextFactory<GolfDbContext>
    {
        public GolfDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GolfDbContext>();
            optionsBuilder.UseSqlite("Data Source=GolfCompanion.db");
            return new GolfDbContext(optionsBuilder.Options);
        }
    }
}
