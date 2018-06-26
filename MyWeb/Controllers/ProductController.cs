using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Models;
using MyWeb.Repositories;

namespace MyWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        IProductRepository repo;
        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public void Create([FromBody]Product data)
        {
            repo.CreateOne(data);
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return repo.Get(p => p.Id == id);
        }

        [HttpGet("{price}")]
        public Product GetByPrice(int price)
        {
            return repo.Get(p => p.Price > 20);
        }

        [HttpGet]
        public IEnumerable<Product> List()
        {
            return repo.List(p => true);
        }

        [HttpPost]
        public void Update([FromBody]Product data)
        {
            repo.UpdateOne(data);
        }

        [HttpPost]
        public void AddStock([FromBody]AddStockRequest request)
        {
            var product = repo.Get(p => p.Id == request.Id);
            product.Stock += request.Amount;
            repo.UpdateOne(product);
        }

        [HttpPost("{id}")]
        public void Delete(int id)
        {
            repo.DeleteOne(id);
        }
    }
}
