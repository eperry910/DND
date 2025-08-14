using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class SpellSlot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Level { get; set; }
        public int Total { get; set; }
        public int Used { get; set; }
        public string Source { get; set; } // "Warlock", "Multiclass", "Magic Item"
        
        [BsonIgnore]
        public int Available => Total - Used;
    }
} 