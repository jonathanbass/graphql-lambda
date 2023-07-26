using GraphQLServerless.Repository;

namespace GraphQLServerless.Models
{
    public class MutationType
    {
        public async Task<Movie> CreateMovieAsync([Service] IDataRepository repository, Movie movie)
        {
            await repository.CreateMovie(movie);
            return movie;
        }

        public async Task<Movie> UpdateMovieAsync([Service] IDataRepository repository, Movie movie)
        {
            await repository.UpdateMovie(movie);
            return movie;
        }

        public async Task<string> DeleteMovieAsync([Service] IDataRepository repository, string id)
        {
            await repository.DeleteMovie(id);
            return $"Movie with Id: {id} deleted.";
        }
    }
}
