using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class EquippedItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Slot { get; set; } // "Head", "Chest", "MainHand", "OffHand", "Feet", "Hands", "Neck", "Finger1", "Finger2", "Waist", "Shoulders", "Back"
        public Item Item { get; set; }
        public bool IsAttuned { get; set; }
        public Dictionary<string, object> ActiveEffects { get; set; }
        public int Quantity { get; set; } = 1;
    }
} 