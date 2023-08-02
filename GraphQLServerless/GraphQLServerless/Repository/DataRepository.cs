using Amazon.DynamoDBv2.DataModel;
using GraphQLServerless.Models;

namespace GraphQLServerless.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public DataRepository(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            movie.Id = Guid.NewGuid().ToString();
            await _dynamoDbContext.SaveAsync(movie);
            return movie;
        }

        public async Task DeleteMovie(string id)
        {
            var movie = await _dynamoDbContext.LoadAsync<Movie>(id);
            await _dynamoDbContext.DeleteAsync(movie);
        }

        public async Task<Movie> GetMovie(string id)
        {
            return await _dynamoDbContext.LoadAsync<Movie>(id);
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _dynamoDbContext.ScanAsync<Movie>(default).GetRemainingAsync();
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            await _dynamoDbContext.SaveAsync(movie);
            return movie;
        }
    }
}
