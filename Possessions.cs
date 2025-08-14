using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class Possession : IEquipment
    {
        [BsonId]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public string Category { get; set; }
        public string AdditionalEffect { get; set; }
    }
}