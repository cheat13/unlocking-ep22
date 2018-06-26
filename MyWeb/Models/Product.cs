using MongoDB.Bson.Serialization.Attributes;

namespace MyWeb
{
    public class Product
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
    }
}