using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class Metamagics
    {
        [BsonId]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prerequisite { get; set; }
    }
}