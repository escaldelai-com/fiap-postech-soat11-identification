using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Application.UseCases;

namespace Restaurant.Identification.Application.Test;

public class ClientGetByCpfUseCaseTest : TestBase
{

    [Fact]
    public async Task ClientGetByCpfUseCase_Ok()
    {
        //Arrange
        var repo = new Mock<IClientRepository>();
        var cpf = Faker.Person.Cpf();
        var nakedCpf = cpf.Replace(".", "").Replace("-", "");
        var client = new ClientDto
        {
            Id = Faker.Random.Guid().ToString(),
            Nome = Faker.Person.FullName,
            CPF = cpf,
            Email = Faker.Person.Email
        };
        repo.Setup(x => x.GetByCpf(nakedCpf)).ReturnsAsync(client);
        var useCase = new ClientGetByCpfUseCase(repo.Object);

        //Act
        var data = await useCase.GetClientIdentify(cpf);

        //Assert
        repo.Verify(x => x.GetByCpf(nakedCpf), Times.Once);
        data.Should().BeEquivalentTo(client);
    }

}
