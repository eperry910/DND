namespace DND.Models;

public class OverallClasses{
    string Name { get; set; }
    List<Subclass> Subclasses { get; set; }
    Dictionary<int, string> ClassFeatures { get; set; }
}