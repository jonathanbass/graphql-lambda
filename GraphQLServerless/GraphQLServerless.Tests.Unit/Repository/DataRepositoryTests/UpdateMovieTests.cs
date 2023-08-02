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
    internal class GivenADataRepositoryU
    {
        private Movie _expectedMovie;
        private Mock<IDynamoDBContext> _mockDataBaseContext;
        private Movie _actualMovie;

        [OneTimeSetUp]
        public async Task WhenTheMovieIsCreated()
        {
            var fixture = new Fixture();
            _expectedMovie = fixture.Create<Movie>();

            _mockDataBaseContext = new Mock<IDynamoDBContext>();

            var dataRepository = new DataRepository(_mockDataBaseContext.Object);
            _actualMovie = await dataRepository.UpdateMovie(_expectedMovie);
        }

        [Test]
        public void ThenTheDataIsUpdated()
        {
            _mockDataBaseContext.Verify(m =>
                m.SaveAsync(_expectedMovie, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ThenTheCorrectMovieIsReturned()
        {
            _actualMovie.Should().BeEquivalentTo(_expectedMovie);
        }
    }
}
