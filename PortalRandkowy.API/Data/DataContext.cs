using Microsoft.EntityFrameworkCore;

namespace PortalRandkowy.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}