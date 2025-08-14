using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class CharacterClass
    {
        [BsonId]
        public string ClassName { get; set; }
        public int ClassLevel { get; set; }
        public bool StartingClass { get; set; }
        public bool SpellCasting { get; set; }
        public bool PactMagic { get; set; }
        public string HitDice { get; set; } // e.g., "1d10"
        public List<string> SavingThrows { get; set; } // e.g., "Strength", "Constitution"
        public List<string> ClassFeatures { get; set; } // e.g., "Second Wind", "Action Surge"
        public Subclass ChosenSubclass { get; set; } // nullable
    }
}
