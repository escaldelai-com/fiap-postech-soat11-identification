using Bogus;

namespace Restaurant.Identification.Application.Test;

public abstract class TestBase
{
    protected Faker Faker { get; } = new("pt_BR");

    protected static string GetNakedCpf(string cpf)
    {
        return cpf
            .Replace(".", "")
            .Replace("-", "");
    }

    protected static string GetGuid()
    {
        return Guid.NewGuid().ToString("n");
    }
}
