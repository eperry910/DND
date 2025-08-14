using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class Spell
    {
        [BsonId]
        public string Name { get; set; }
        public int Level { get; set; }
        public string School { get; set; }
        public string CastingTime { get; set; }
        public string Range { get; set; }
        public string Components { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public bool Ritual { get; set; }
        public bool Concentration { get; set; }
        public bool Prepared { get; set; }
        public AreaOfEffectType AreaOfEffectType { get; set; }
    }
}