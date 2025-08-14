using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models;

public class OverallClasses{
    [BsonId]
    public string Name { get; set; }
    public List<string> Subclasses { get; set; }
    public Dictionary<string, List<string>> ClassFeatures { get; set; }
    public string CastingType { get; set; } //Full Caster, Half Caster, Subclass Caster, Pact Magic
    public int HitDice { get; set; }
    public List<string> SavingThrows { get; set; }
    public List<List<string>> startingItems { get; set; }
    public string SpellCastingAbility { get; set; }
    public List<string> Proficiencies { get; set; }
}