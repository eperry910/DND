namespace DND.Models
{
    public class CharacterClass
    {
        public string ClassName { get; set; }
        public int ClassLevel { get; set; }
        public boolean StartingClass { get; set; }
        public boolean SpellCasting { get; set; }
        public boolean PactMagic { get; set; }
        public string HitDice { get; set; } // e.g., "1d10"
        public List<string> SavingThrows { get; set; } // e.g., "Strength", "Constitution"
        public List<string> ClassFeatures { get; set; } // e.g., "Second Wind", "Action Surge"
    }
}
