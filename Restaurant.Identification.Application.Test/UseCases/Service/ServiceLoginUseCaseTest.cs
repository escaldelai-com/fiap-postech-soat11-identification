using FluentAssertions;
using Moq;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Presenter;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Application.UseCases;
using Restaurant.Identification.Model;
using Restaurant.Identification.Presenter.Presenters;

namespace Restaurant.Identification.Application.Test;

public class ServiceLoginUseCaseTest : TestBase
{

    [Fact]
    public async Task ServiceLoginUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IServiceRepository>();
        var presenter = new Mock<IHashPresenter>();
        var useCase = new ServiceLoginUseCase(repo.Object, presenter.Object);
        var secret = GetGuid();
        var hash = new HashPresenter().GetHash(secret);
        var login = new ServiceLoginDto
        {
            client_id = GetGuid(),
            grant_type = "client_credentials",
            client_secret = secret
        };
        var service = new ServiceDto
        {
            Id = login.client_id,
            Name = Faker.Random.Word(),
            Secret = hash
        };
        repo.Setup(r => r.GetById(login.client_id!)).ReturnsAsync(service);
        presenter.Setup(p => p.GetHash(login.client_secret!)).Returns(hash);

        // Act
        var result = await useCase.Login(login);

        // Assert
        result.Should().Be(service);
    }

    [Fact]
    public async Task ServiceLoginUseCase_Null_Login()
    {
        // Arrange
        var repo = new Mock<IServiceRepository>();
        var presenter = new Mock<IHashPresenter>();
        var useCase = new ServiceLoginUseCase(repo.Object, presenter.Object);
        var secret = GetGuid();
        var hash = new HashPresenter().GetHash(secret);

        // Act
        var act = () => useCase.Login(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("foo")]
    public async Task ServiceLoginUseCase_Invalid_Grant_Type(string? value)
    {
        // Arrange
        var repo = new Mock<IServiceRepository>();
        var presenter = new Mock<IHashPresenter>();
        var useCase = new ServiceLoginUseCase(repo.Object, presenter.Object);
        var secret = GetGuid();
        var hash = new HashPresenter().GetHash(secret);
        var login = new ServiceLoginDto
        {
            client_id = GetGuid(),
            grant_type = value,
            client_secret = secret
        };

        // Act
        var act = () => useCase.Login(login);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ServiceLoginUseCase_Service_Not_Found()
    {
        // Arrange
        var repo = new Mock<IServiceRepository>();
        var presenter = new Mock<IHashPresenter>();
        var useCase = new ServiceLoginUseCase(repo.Object, presenter.Object);
        var secret = GetGuid();
        var hash = new HashPresenter().GetHash(secret);
        var login = new ServiceLoginDto
        {
            client_id = GetGuid(),
            grant_type = "client_credentials",
            client_secret = secret
        };
        repo.Setup(r => r.GetById(login.client_id!)).ReturnsAsync(() => null!);

        // Act
        var result = await useCase.Login(login);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task ServiceLoginUseCase_Invalid_Secret()
    {
        // Arrange
        var repo = new Mock<IServiceRepository>();
        var presenter = new Mock<IHashPresenter>();
        var useCase = new ServiceLoginUseCase(repo.Object, presenter.Object);
        var login = new ServiceLoginDto
        {
            client_id = GetGuid(),
            grant_type = "client_credentials",
            client_secret = GetGuid()
        };
        var service = new ServiceDto
        {
            Id = login.client_id,
            Name = Faker.Random.Word(),
            Secret = GetGuid()
        };
        repo.Setup(r => r.GetById(login.client_id!)).ReturnsAsync(() => null!);
        presenter.Setup(p => p.GetHash(login.client_secret!)).Returns("foo");

        // Act
        var result = await useCase.Login(login);

        // Assert
        result.Should().BeNull();
    }

}
