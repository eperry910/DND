using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class CharacterSheet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CharacterName { get; set; }
        public Race CharacterRace { get; set; }
        public string Background { get; set; }
        public string Alignment { get; set; }
        
        // Core stats
        public Dictionary<string, int> AbilityScores { get; set; }
        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }
        
        // Proficiencies
        public List<string> SkillProficiencies { get; set; }
        public List<string> Expertise { get; set; }
        public List<string> Languages { get; set; }
        public List<string> ToolProficiencies { get; set; }
        public List<string> WeaponProficiencies { get; set; }
        public List<string> ArmorProficiencies { get; set; }
        
        // Classes and levels
        public List<CharacterClassLevel> ClassLevels { get; set; }
        
        // Features and resources
        public List<CharacterFeature> ActiveFeatures { get; set; }
        public List<CharacterResource> Resources { get; set; }
        
        // Equipment
        public List<EquippedItem> EquippedItems { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        
        // Spells
        public List<PreparedSpell> PreparedSpells { get; set; }
        public List<SpellSlot> SpellSlots { get; set; }
        public List<SpellSlot> WarlockSpellSlots { get; set; }
        
        // Experience
        public int ExperiencePoints { get; set; }
        
        // Calculated properties (not stored in DB)
        [BsonIgnore]
        public int TotalLevel => ClassLevels?.Sum(cl => cl.Level) ?? 0;
        
        [BsonIgnore]
        public int ProficiencyBonus => ((TotalLevel - 1) / 4) + 2;
        
        [BsonIgnore]
        public int FullCasterLevels => ClassLevels?
            .Where(cl => IsFullCaster(cl.ClassName))
            .Sum(cl => cl.Level) ?? 0;
            
        [BsonIgnore]
        public int HalfCasterLevels => ClassLevels?
            .Where(cl => IsHalfCaster(cl.ClassName))
            .Sum(cl => cl.Level) ?? 0;
            
        [BsonIgnore]
        public int WarlockLevels => ClassLevels?
            .Where(cl => cl.ClassName.Equals("Warlock", StringComparison.OrdinalIgnoreCase))
            .Sum(cl => cl.Level) ?? 0;
        
        private static bool IsFullCaster(string className) => 
            new[] { "Bard", "Cleric", "Druid", "Sorcerer", "Wizard" }
            .Contains(className, StringComparer.OrdinalIgnoreCase);
            
        private static bool IsHalfCaster(string className) => 
            new[] { "Paladin", "Ranger", "Artificer" }
            .Contains(className, StringComparer.OrdinalIgnoreCase);
    }
}
