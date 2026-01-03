using Bogus.Extensions.Brazil;
using FluentAssertions;

namespace Restaurant.Identification.Model.Test
{
    public class ClientTest : TestBase
    {

        [Fact]
        public void Client_Ok()
        {
            // Arrange
            var model = new {
                Nome = faker.Name.FullName(),
                CPF = new CPF(faker.Person.Cpf(true)),
                Email = faker.Internet.Email()
            };

            // Act
            var test = new Client(
                model.Nome, model.CPF.Numero, model.Email
            );

            // Assert
            test.Should().BeEquivalentTo(model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Client_Invalid_Name(string? name)
        {
            // Arrange
            var cpf = faker.Person.Cpf(true);
            var email = faker.Internet.Email();

            // Act
            var test = () => new Client(name!, cpf, email);

            // Assert
            test.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("plainaddress")]
        [InlineData("missing@")]
        [InlineData("@missingname.com")]
        [InlineData("missingdomain.com")]
        public void Client_Invalid_email(string? email)
        {
            // Arrange
            var name = faker.Name.FullName();
            var cpf = faker.Person.Cpf(true);

            // Act
            var test = () => new Client(name, cpf, email!);

            // Assert
            test.Should().Throw<ValidationException>();
        }

    }
}
