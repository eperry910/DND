namespace DND.Models
{
    public class Weapon : IEquipment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public string DamageDice { get; set; }
        public string DamageType { get; set; } // e.g., Slashing, Piercing, Bludgeoning
        public int Range { get; set; }
        public string AdditionalEffect { get; set; }
        public string AttackModifier { get; set; }
    }
}