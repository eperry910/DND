namespace DND.Models
{
    public interface IEquipment
    {
        string Name { get; set; }
        string Description { get; set; }
        double Weight { get; set; }
        int Value { get; set; } // Value in gold pieces
    }
}