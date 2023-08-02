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
    internal class GivenADataRepositoryG
    {
        private Movie _expectedMovie;
        private Mock<IDynamoDBContext> _mockDataBaseContext;
        private Movie _actualMovie;

        [OneTimeSetUp]
        public async Task WhenTheMovieIsRetrieved()
        {
            var fixture = new Fixture();
            _expectedMovie = fixture.Create<Movie>();

            _mockDataBaseContext = new Mock<IDynamoDBContext>();
            _mockDataBaseContext.Setup(m => m.LoadAsync<Movie>(_expectedMovie.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedMovie);

            var dataRepository = new DataRepository(_mockDataBaseContext.Object);
            _actualMovie = await dataRepository.GetMovie(_expectedMovie.Id);
        }

        [Test]
        public void ThenTheCorrectMovieIsRetrieved()
        {
            _mockDataBaseContext.Verify(m =>
                m.LoadAsync<Movie>(_expectedMovie.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ThenTheCorrectMovieIsReturned()
        {
            _actualMovie.Id.Should().Be(_expectedMovie.Id);
        }
    }
}
