namespace CRUD_API.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;
        public MovieService(AppDbContext context)
        {
            _context = context;
        }
        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte categoryId = 0)
        {
            return await _context.Movies
                .Where(m => m.CategoryId == categoryId || categoryId==0)
                .Include(m => m.Category)
                .OrderBy(m => m.Title)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies.Include(m => m.Category).SingleAsync(m=>m.Id==id);
        }

       
        public async Task<Movie> PostAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public  Movie Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
