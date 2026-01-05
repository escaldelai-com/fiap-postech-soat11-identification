using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Identification.Data.Model;

public class ClientData
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Nome { get; set; }

    public string? CPF { get; set; }

    public string? Email { get; set; }

}
