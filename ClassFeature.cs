
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace DND.Models
{
    public class ClassFeature
    { 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string FeatureName { get; set; }
        public string ActionType { get; set; } // e.g., "AddProficiency", "ChooseSpell", etc.
        public string Description { get; set; }
        public string ActionEconomy { get; set; } // e.g., "Action", "BonusAction", "Reaction", "Passive" etc.
        public string Resource { get; set; } // e.g., "Channel Divinoty Charges", "Sorcery Points", etc.
    }
}