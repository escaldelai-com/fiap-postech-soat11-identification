using Bogus;

namespace Restaurant.Identification.Model.Test;

public abstract class TestBase
{
    protected Faker Faker { get; } = new("pt_BR");

}
