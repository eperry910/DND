using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DND.Models
{
    public class Race
    {
        [BsonId]
        public string Name { get; set; }
        public List<string> RacialFeatures { get; set; }
        public int MovementSpeed { get; set; }
    }
}