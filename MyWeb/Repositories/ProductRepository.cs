using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Authentication;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MyWeb.Repositories
{
    public interface IProductRepository
    {
        Product Get(Expression<Func<Product, bool>> expression);
        IEnumerable<Product> List(Expression<Func<Product, bool>> expression);
        void UpdateOne(Product data);
        void CreateOne(Product data);
        void DeleteOne(int id);
    }

    public class ProductRepository : IProductRepository
    {
        IMongoCollection<Product> Collection { get; set; }

        public ProductRepository(DatabaseConfigurations config)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(config.MongoDBConnection));
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(config.DatabaseName);
            Collection = database.GetCollection<Product>("products");
        }

        public Product Get(Expression<Func<Product, bool>> expression)
        {
            var product = Collection.Find(expression).FirstOrDefault();
            return product;
        }

        public IEnumerable<Product> List(Expression<Func<Product, bool>> expression)
        {
            var products = Collection.Find(expression).ToList();
            return products;
        }

        public void UpdateOne(Product data)
        {
            Collection.ReplaceOne(it => it.Id == data.Id, data);
        }

        public void CreateOne(Product data)
        {
            Collection.InsertOne(data);
        }

        public void DeleteOne(int id)
        {
            Collection.DeleteOne(p => p.Id == id);
        }
    }
}