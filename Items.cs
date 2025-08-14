using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class Item
    {
        [BsonId]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemType { get; set; } // "Weapon", "Armor", "Adventuring Gear", "Tool", "Wondrous Item", "Potion", "Scroll"
        public string Rarity { get; set; } // "Common", "Uncommon", "Rare", "Very Rare", "Legendary"
        public bool RequiresAttunement { get; set; }
        public string AttunementPrerequisite { get; set; }
        public List<string> Properties { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public string EquipmentSlot { get; set; } // For equippable items
        public Dictionary<string, object> Effects { get; set; }
        public List<string> Tags { get; set; } // For filtering and searching
    }
}