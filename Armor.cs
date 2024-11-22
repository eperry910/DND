namespace DND.Models
{
    public class Armor : IEquipment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public int ArmorClass { get; set; }
        public string ArmorType { get; set; } // e.g., Light, Medium, Heavy
        public string? AdditionalEffect { get; set; }
    }
}