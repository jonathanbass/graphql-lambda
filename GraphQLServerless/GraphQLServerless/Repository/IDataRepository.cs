using GraphQLServerless.Models;

namespace GraphQLServerless.Repository
{
    public interface IDataRepository
    {
        Task<IEnumerable<Movie>> GetMovies();

        Task<Movie> GetMovie(string id);

        Task<Movie> UpdateMovie(Movie movie);

        Task DeleteMovie(string id);

        Task<Movie> CreateMovie(Movie movie);
    }
}