using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class PreparedSpell
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Spell Spell { get; set; }
        public string Source { get; set; } // "Class: Wizard", "Feat: Magic Initiate", "Race: Tiefling"
        public bool IsAlwaysPrepared { get; set; }
        public bool IsDomainSpell { get; set; }
        public bool IsFreeSpell { get; set; } // Doesn't count against prepared spells
    }
} 