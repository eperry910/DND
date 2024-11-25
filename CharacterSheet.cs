namespace DND.Models
{
    public class CharacterSheet
    {
        public string Id { get; set; }
        public string startingClass { get; set; }
        public Dictionary<string, OverallClasses> Classes { get; set; }
        public int UnarmoredAC { get; set; }
        public string CharacterName { get; set; }
        public List<Feats> Feats { get; set; }
        public Dictionary<string, int> AbilityScores { get; set; }
        public int CharacterLevel { get; set; }
        public Race CharacterRace { get; set; }
        public List<string> SkillProficiencies { get; set; }
        public Dictionary<string, List<string>> GearProficiencies { get; set; }
        public List<string> Expertise { get; set; }
        public List<string> Languages { get; set; }
        public int HitPoints { get; set; }
        public int ProficiencyBonus { get; set; }
        public string Background { get; set; }
        public string Alignment { get; set; }
        public Armor equippedArmor { get; set; }
        public Weapon equippedWeapon { get; set; }
        public List<Armor> storedArmor { get; set; }
        public List<Weapon> storedWeapon { get; set; }
        public List<Possession> possessions { get; set; }
        public int currentExperience { get; set; }
        public List<string> EldritchInvocations { get; set; }
        public int SorceryPoints { get; set; }
        public List<Metamagics> KnownMetamagics { get; set; }
        public List<EldritchInvocations> KnownEldritchInvocations { get; set; }
        public Pacts possessedPact { get; set; }
        public string sneakAttackDamage { get; set;}
        public int ArcaneRecoveryPoints { get; set; }
        public List<Spells> AlwaysFreeSpells { get; set; }
        public List<Spells> FreeIfPreparedSpells { get; set; }
        public int RageCount { get; set; }
        public int RageDamage { get; set; }
        public int NumberOfExtraAttacks { get; set; }
        
        
    }
}
