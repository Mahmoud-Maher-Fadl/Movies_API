namespace CRUD_API.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Category>Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        
    }
}
