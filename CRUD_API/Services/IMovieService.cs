namespace CRUD_API.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll(byte CategoryId = 0);
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> PostAsync(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);



    }
}
