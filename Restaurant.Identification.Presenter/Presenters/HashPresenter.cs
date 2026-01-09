using Restaurant.Identification.Application.Interfaces.Presenter;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Identification.Presenter.Presenters;

public class HashPresenter : IHashPresenter
{

    private readonly SHA256 sha = SHA256.Create();

    public string GetHash(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException(nameof(input));

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

}

