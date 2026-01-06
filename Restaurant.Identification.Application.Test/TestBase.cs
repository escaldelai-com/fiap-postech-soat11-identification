using Bogus;

namespace Restaurant.Identification.Application.Test;

public abstract class TestBase
{

    protected Faker faker = new("pt_BR");

    protected string GetNakedCpf(string cpf)
    {
        return cpf
            .Replace(".", "")
            .Replace("-", "");
    }

    protected string GetGuid() => Guid.NewGuid().ToString("n");

}
