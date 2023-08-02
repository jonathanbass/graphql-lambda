using AutoFixture;
using FluentAssertions;
using GraphQLServerless.Models;
using GraphQLServerless.Repository;
using Moq;
using NUnit.Framework;

namespace GraphQLServerless.Tests.Unit.Models.MutationTypeTests
{
    [TestFixture]
    internal class GivenAMutationTypeU
    {
        private Movie _expectedMovie;
        private Mock<IDataRepository> _mockDataRepository;
        private Movie _actualMovie;

        [OneTimeSetUp]
        public async Task WhenTheMovieIsUpdated()
        {
            var fixture = new Fixture();
            _expectedMovie = fixture.Create<Movie>();

            _mockDataRepository = new Mock<IDataRepository>();
            var mutationType = new MutationType();
            _actualMovie = await mutationType.UpdateMovieAsync(_mockDataRepository.Object, _expectedMovie);
        }

        [Test]
        public void ThenTheDataIsUpdated()
        {
            _mockDataRepository.Verify(m => m.UpdateMovie(_expectedMovie), Times.Once);
        }

        [Test]
        public void ThenTheCorrectMovieIsReturned()
        {
            _actualMovie.Should().BeEquivalentTo(_expectedMovie);
        }
    }
}
