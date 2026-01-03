using MongoDB.Bson;

namespace Restaurant.Identification.Data.Model;

public class ClientData
{

    public ObjectId Id { get; set; }

    public string? Nome { get; set; }

    public string? CPF { get; set; }

    public string? Email { get; set; }

}
