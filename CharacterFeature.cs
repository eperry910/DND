using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class CharacterFeature
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; } // "Class: Fighter 2", "Feat: Alert", "Race: Elf"
        public string Description { get; set; }
        public Dictionary<string, object> Effects { get; set; }
        public bool IsActive { get; set; }
        public string Prerequisite { get; set; }
        public string ActionType { get; set; } // "Action", "Bonus Action", "Reaction", "Free"
        public string ResourceCost { get; set; } // "1 Sorcery Point", "1 Channel Divinity", etc.
    }
} 