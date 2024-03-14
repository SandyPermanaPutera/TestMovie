using Microsoft.EntityFrameworkCore;
using Movies.Model;

namespace Movies.Data
{
    public class ApiContext :DbContext
    {
        public DbSet<Movie> Bookings { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {

        }
    }
}
