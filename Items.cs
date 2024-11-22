namespace DND.Models
{
    public class Item
    {
        public string Name { get; set; }
        public List<string> Properties { get; set; }
        public int Weight { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public bool IsEquipped { get; set; }
        public string EquipmentSlot { get; set; }
    }
}