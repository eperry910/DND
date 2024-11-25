namespace DND.Models;

public class OverallClasses{
    public string Name { get; set; }
    public List<Subclass> Subclasses { get; set; }
    public Dictionary<string, string> ClassFeatures { get; set; }
    public bool SpellCasting { get; set; }
    public bool PactMagic { get; set; }
    public string HitDice { get; set; }
    public List<string> SavingThrows { get; set; }
    public List<Weapon> startingClassWeapons { get; set; }
    public List<Armor> startingClassArmor { get; set; }
    public List<Possession> startingClassPossessions { get; set; }

}