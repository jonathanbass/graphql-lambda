using AutoFixture;
using FluentAssertions;
using GraphQLServerless.Models;
using GraphQLServerless.Repository;
using GraphQLServerless.Services;
using Moq;
using NUnit.Framework;

namespace GraphQLServerless.Tests.Unit.Services
{
    [TestFixture]
    internal class GivenAMovieService
    {
        private IEnumerable<Movie> _expectedMovies;
        private Mock<IDataRepository> _mockDataRepository;
        private IEnumerable<Movie> _actualMovies;

        [OneTimeSetUp]
        public async Task WhenTheMoviesAreRetrieved()
        {
            var fixture = new Fixture();
            _expectedMovies = fixture.CreateMany<Movie>();

            _mockDataRepository = new Mock<IDataRepository>();
            _mockDataRepository.Setup(m => m.GetMovies()).ReturnsAsync(_expectedMovies);

            var movieService = new MovieService(_mockDataRepository.Object);
            _actualMovies = await movieService.GetMovies();
        }

        [Test]
        public void ThenTheMoviesAreRetrieved()
        {
            _mockDataRepository.Verify(m => m.GetMovies(), Times.Once);
        }

        [Test]
        public void ThenTheCorrectMoviesAreReturned()
        {
            _actualMovies.Should().BeEquivalentTo(_expectedMovies);
        }
    }
}
