using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Identification.Data.Model;

public class ServiceData
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Secret { get; set; }

    public string[] Audiences { get; set; } = [];

    public string[] Roles { get; set; } = [];

}
