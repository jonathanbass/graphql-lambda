using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FluentAssertions;
using GraphQLServerless.Models;
using GraphQLServerless.Repository;
using Moq;
using NUnit.Framework;

namespace GraphQLServerless.Tests.Unit.Repository.DataRepositoryTests
{
    [TestFixture]
    internal class GivenADataRepositoryS
    {
        private List<Movie> _expectedMovies;
        private Mock<IDynamoDBContext> _mockDataBaseContext;
        private IEnumerable<Movie> _actualMovies;

        [OneTimeSetUp]
        public async Task WhenAllMoviesAreRetrieved()
        {
            var fixture = new Fixture();
            _expectedMovies = fixture.CreateMany<Movie>().ToList();

            _mockDataBaseContext = new Mock<IDynamoDBContext>();
            _mockDataBaseContext.Setup(m =>
                m.ScanAsync<Movie>(default, It.IsAny<DynamoDBOperationConfig>())
                .GetRemainingAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedMovies);

            var dataRepository = new DataRepository(_mockDataBaseContext.Object);
            _actualMovies = await dataRepository.GetMovies();
        }

        [Test]
        public void ThenTheCorrectMoviesAreRetrieved()
        {
            _mockDataBaseContext.Verify(m =>
                m.ScanAsync<Movie>(default, It.IsAny<DynamoDBOperationConfig>())
                .GetRemainingAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ThenTheCorrectMoviesAreReturned()
        {
            _actualMovies.Should().BeEquivalentTo(_expectedMovies);
        }
    }
}
