using AutoFixture;
using FluentAssertions;
using GraphQLServerless.Models;
using GraphQLServerless.Repository;
using Moq;
using NUnit.Framework;

namespace GraphQLServerless.Tests.Unit.Models.MutationTypeTests
{
    [TestFixture]
    internal class GivenAMutationTypeD
    {
        private string _expectedId;
        private Mock<IDataRepository> _mockDataRepository;
        private string _responseMessage;

        [OneTimeSetUp]
        public async Task WhenTheMovieIsDeleted()
        {
            var fixture = new Fixture();
            _expectedId = fixture.Create<string>();

            _mockDataRepository = new Mock<IDataRepository>();
            var mutationType = new MutationType();
            _responseMessage = await mutationType.DeleteMovieAsync(_mockDataRepository.Object, _expectedId);
        }

        [Test]
        public void ThenTheDataIsDeleted()
        {
            _mockDataRepository.Verify(m => m.DeleteMovie(_expectedId), Times.Once);
        }

        [Test]
        public void ThenTheCorrectMovieIsReturned()
        {
            _responseMessage.Contains(_expectedId).Should().BeTrue();
        }
    }
}
