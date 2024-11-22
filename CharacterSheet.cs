namespace DND.Models
{
    public class CharacterSheet
    {
        public int Id { get; set; }
        public Dictionary<string, CharacterClass> Classes { get; set; }
        public string Name { get; set; }
        public List<Feats> Feats { get; set; }
        public Dictionary<string, int> AbilityScores { get; set; }
        public int CharacterLevel { get; set; }
        public Race CharacterRace { get; set; }
        public List<string> SkillProficiencies { get; set; }
        public List<Item> Inventory { get; set; }
        public Dictionary<string, List<string>> GearProficiencies { get; set; }
        public List<string> Expertise { get; set; }
        public List<string> Equipment { get; set; }
        public List<string> Languages { get; set; }
        public Dictionary<int, int> SpellSlots { get; set; }
        public int HitPoints { get; set; }
        public Dictionary<int, int> PactMagicSlots { get; set; }
        public int ProficiencyBonus { get; set; }
        public string Background { get; set; }
        public string[] SavingThrows { get; set; }
        public string Alignment { get; set; }
        public int ArmorClass { get; set; }
        public int Initiative { get; set; }
    }
}
