using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class InventoryItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; } // "Backpack", "Belt Pouch", "Saddlebags", etc.
        public bool IsEquipped { get; set; }
        public string Notes { get; set; }
    }
} 