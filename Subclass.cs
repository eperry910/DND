namespace DND.Models;

public class Subclass{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> SubclassFeatures { get; set; }
    public string ParentClass { get; set; }
}