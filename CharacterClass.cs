namespace DND.Models
{
    public class CharacterClass
    {
        public string ClassName { get; set; }
        public Dictionary<string, int> Feat { get; set; }
        public int Level { get; set; }
        public string Race { get; set; }
        public List<string> Proficiencies { get; set; }
        public List<string> Equipment { get; set; }
        public List<string> Languages { get; set; }
        public Dictionary<int, int> SpellSlots { get; set; }
        public string StartingClass { get; set; }
        public string Background { get; set; }
        public string SpellCasting { get; set; }
        public string PactMagic { get; set; }
        public string HitDice { get; set; } // e.g., "1d10"
        public List<string> SavingThrows { get; set; } // e.g., "Strength", "Constitution"
        public List<string> ClassFeatures { get; 
        set; } // e.g., "Second Wind", "Action Surge"
    }
}
