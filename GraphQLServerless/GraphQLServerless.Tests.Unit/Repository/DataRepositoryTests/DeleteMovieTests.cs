using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using GraphQLServerless.Models;
using GraphQLServerless.Repository;
using Moq;
using NUnit.Framework;

namespace GraphQLServerless.Tests.Unit.Repository.DataRepositoryTests
{
    [TestFixture]
    internal class GivenADataRepositoryD
    {
        private Movie _expectedMovie;
        private Mock<IDynamoDBContext> _mockDataBaseContext;

        [OneTimeSetUp]
        public async Task WhenTheMovieIsCreated()
        {
            var fixture = new Fixture();
            _expectedMovie = fixture.Create<Movie>();

            _mockDataBaseContext = new Mock<IDynamoDBContext>();
            _mockDataBaseContext.Setup(m => m.LoadAsync<Movie>(_expectedMovie.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedMovie);

            var dataRepository = new DataRepository(_mockDataBaseContext.Object);
            await dataRepository.DeleteMovie(_expectedMovie.Id);
        }

        [Test]
        public void ThenTheCorrectMovieIsRetrieved()
        {
            _mockDataBaseContext.Verify(m =>
                m.LoadAsync<Movie>(_expectedMovie.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ThenTheDataIsDeleted()
        {
            _mockDataBaseContext.Verify(m =>
                m.DeleteAsync(_expectedMovie, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
