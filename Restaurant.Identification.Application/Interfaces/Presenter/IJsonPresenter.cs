namespace Restaurant.Identification.Application.Interfaces.Presenter;

public interface IJsonPresenter
{

    string? Serialize(object? obj);

    T? Deserialize<T>(string? json);

}
