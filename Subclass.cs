using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models;

public class Subclass{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, List<string>> SubclassFeatures { get; set; }
    public string ParentClass { get; set; }
}