using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class Armor : IEquipment
    {
        [BsonId]
        public string Name { get; set; }
        public string Description { get; set; }
        public int ArmorClass { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public string ArmorType { get; set; }
        public bool StealthDisadvantage { get; set; }
    }
}