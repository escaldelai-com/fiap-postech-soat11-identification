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
            var model = new
            {
                Nome = Faker.Name.FullName(),
                CPF = new CPF(Faker.Person.Cpf(true)),
                Email = Faker.Internet.Email()
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
            var cpf = Faker.Person.Cpf(true);
            var email = Faker.Internet.Email();

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
            var name = Faker.Name.FullName();
            var cpf = Faker.Person.Cpf(true);

            // Act
            var test = () => new Client(name, cpf, email!);

            // Assert
            test.Should().Throw<ValidationException>();
        }

    }
}
