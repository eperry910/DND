using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class CharacterClassLevel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClassName { get; set; }
        public int Level { get; set; }
        public string Subclass { get; set; }
        public List<string> Features { get; set; }
        public List<SpellSlot> SpellSlots { get; set; }
        public Dictionary<string, object> ClassSpecificData { get; set; }
    }
} 