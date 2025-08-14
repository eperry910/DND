using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class Feats
    {
        [BsonId]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prerequisite { get; set; }
    }
}