namespace DND.Models
{
    public class Possession : IEquipment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public string Category { get; set; } // e.g., Tool, Trinket, Miscellaneous
        public string AdditionalEffect { get; set; }
    }
}