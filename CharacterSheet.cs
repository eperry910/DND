namespace DND.Models;

public class CharacterSheet
{
    public string Name { get; set; }

    public Dictionary<string, int> ClassLevels{ get; set; }

    public int Level { get; set; }
    
    public string Race { get; set; }

    public List<string> Proficiencies { get; set; }
    
    public List<string> Equipment { get; set; }

    public List<string> Languages { get; set; }
    
    public Dictionary<int, int> SpellSlots { get; set; }
    
    public string Background { get; set; }
}
