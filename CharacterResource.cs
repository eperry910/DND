using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class CharacterResource
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Current { get; set; }
        public int Maximum { get; set; }
        public string Source { get; set; } // "Class: Sorcerer", "Feat: Magic Initiate", etc.
        public bool ResetsOnShortRest { get; set; }
        public bool ResetsOnLongRest { get; set; }
        public string Description { get; set; }
    }
} 