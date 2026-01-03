using Restaurant.Identification.Application.DTO;
using Bogus.Extensions.Brazil;
using Restaurant.Identification.Application.UseCases;
using Moq;
using Restaurant.Identification.Application.Interfaces.Repository;
using FluentAssertions;
using Restaurant.Identification.Model;
using Restaurant.Identification.Application.Interfaces.UseCases;

namespace Restaurant.Identification.Application.Test;

public class ClientRegisterUseCaseTest : TestBase
{

    [Fact]
    public async Task ClientRegisterUseCase_OK()
    {
        // Arrange
        var repo = new Mock<IClientRepository>();
        var getByCpfUseCase = new Mock<IClientGetByCpfUseCase>();
        var id = Guid.NewGuid().ToString("n");
        var cpf = faker.Person.Cpf();
        var nakedCpf = GetNakedCpf(cpf);
        var clientDto = new ClientDto
        {
            Nome = faker.Name.FullName(),
            CPF = cpf,
            Email = faker.Internet.Email()
        };
        repo.Setup(r => r.Save(clientDto)).ReturnsAsync(id);
        getByCpfUseCase.Setup(x => x.GetClientIdentify(cpf)).ReturnsAsync((ClientDto?)null);
        var useCase = new ClientRegisterUseCase(getByCpfUseCase.Object, repo.Object);

        // Act
        var result = await useCase.Register(clientDto);

        // Assert
        getByCpfUseCase.Verify(m => m.GetClientIdentify(cpf), Times.Once);
        repo.Verify(m => m.Save(clientDto), Times.Once);
        result.Should().Be(id);
    }

    [Fact]
    public async Task ClientRegisterUseCase_Exists()
    {
        // Arrange
        var repo = new Mock<IClientRepository>();
        var getByCpfUseCase = new Mock<IClientGetByCpfUseCase>();
        var id = Guid.NewGuid().ToString("n");
        var cpf = faker.Person.Cpf();
        var clientDto = new ClientDto
        {
            Id = id,
            Nome = faker.Name.FullName(),
            CPF = cpf,
            Email = faker.Internet.Email()
        };
        getByCpfUseCase.Setup(x => x.GetClientIdentify(cpf)).ReturnsAsync(clientDto);
        var useCase = new ClientRegisterUseCase(getByCpfUseCase.Object, repo.Object);

        // Act
        var result = await useCase.Register(clientDto);

        // Assert
        getByCpfUseCase.Verify(m => m.GetClientIdentify(cpf), Times.Once);
        repo.Verify(m => m.Save(clientDto), Times.Never);
        result.Should().Be(id);
    }

    [Fact]
    public async Task ClientRegisterUseCase_Error_Client_Null()
    {
        // Arrange
        var repo = new Mock<IClientRepository>();
        var getByCpfUseCase = new Mock<IClientGetByCpfUseCase>();
        var id = Guid.NewGuid().ToString("n");
        var clientDto = (ClientDto?)null;
        var useCase = new ClientRegisterUseCase(getByCpfUseCase.Object, repo.Object);

        // Act
        var act = () => useCase.Register(clientDto!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

}
