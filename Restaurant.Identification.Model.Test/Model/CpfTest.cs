using Bogus.Extensions.Brazil;
using FluentAssertions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant.Identification.Model.Test;

public class CpfTest : TestBase
{

    [Fact]
    public void CPF_Ok_With_Symbols()
    {
        //Arrange
        var number = Faker.Person.Cpf();
        var nakedNumber = number
            .Trim()
            .Replace(".", "")
            .Replace("-", "");

        // Act
        var cpf = new CPF(number);

        // Assert
        cpf.Should().NotBeNull();
        cpf.Numero.Should().Be(nakedNumber);
    }

    [Fact]
    public void CPF_Ok_Without_Symbols()
    {
        //Arrange
        var number = Faker.Person.Cpf(false);

        // Act
        var cpf = new CPF(number);

        // Assert
        cpf.Should().NotBeNull();
        cpf.Numero.Should().Be(number);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("999.999.999-99")]
    [InlineData("99999999999")]
    [InlineData("825,049,640,05")]
    [InlineData("825049640")]
    public void CPF_Invalid(string? number)
    {
        // Act
        var test = () => new CPF(number!);

        // Assert
        test.Should().Throw<ValidationException>();
    }


}
