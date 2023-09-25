using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAdd.Models;
using ProductAdd.Repositary;
using System.Collections;
using System.Collections.Generic;

namespace ProductAddApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IData _idata;
        public ProductController(IData data) {
            _idata = data;
        }

        [HttpPost]
        public Product Create(Product product)
        {
            _idata.SaveProductDetails(product);
            return product;
        }

        [HttpGet]
        public List<Product> GetAllProdcutDetails()
        {
            var list= _idata.GetAllProductDetails();
            return list;
        }
        [HttpPut("{id}")]
        public Product Update(Product product,int id)
        {
            _idata.UpdateProductDetails(product, id);
            return product;
        }
    }
}
