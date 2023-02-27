using System;
using CarSharing.Application.Authentication.Commands;
using CarSharing.Application.Authentication.Common;
using CarSharing.Domain.Authentication;
using CarSharing.Domain.Repositories;
using FluentAssertions;

namespace CarSharing.Tests.Application.Authentication
{
	public class RegisterCommandHandlerTests
	{
		private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IJwtTokenGenerator> _tokenGenerator;
        private readonly Mock<IPasswordHash> _hash;

        public RegisterCommandHandlerTests()
        {
            _userRepoMock = new();
            _tokenGenerator = new();
            _hash = new();
        }

        [Fact]
        public async Task Handle_Register_Should_Return_Fail()
        {
            var command = new RegisterCommand("arif","hidayat","arif.sarbini@gmail.com","P@ssW0rd");
            var user = new User { Email = "arif.sarbini", FirstName = "arif", LastName = "hidayat", Id = Guid.NewGuid(),
                Password = "", Role = "admin" };

            _userRepoMock.Setup(
                x => x.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);
            var handler = new RegisterCommandHandler(_tokenGenerator.Object, _userRepoMock.Object, _hash.Object);
            var result = await handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();

        }

        [Fact]
        public async Task Handle_Register_Should_Return_True()
        {
            var command = new RegisterCommand("arif", "hidayat", "arif.sarbini@gmail.com", "P@ssW0rd");
            var user = new User
            {
                Email = "arif.sarbini",
                FirstName = "arif",
                LastName = "hidayat",
                Id = Guid.NewGuid(),
                Password = "",
                Role = "admin"
            };

            //_userRepoMock.Setup(
            //    x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(Nullable<User>);
            var handler = new RegisterCommandHandler(_tokenGenerator.Object, _userRepoMock.Object, _hash.Object);
            var result = await handler.Handle(command, default);
            result.IsSuccess.Should().BeTrue();
        }
    }
}

