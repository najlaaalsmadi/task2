using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task2.Models;

namespace task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _myDbContext1;
        public ProductsController(MyDbContext myDbContext)
        {
            _myDbContext1 = myDbContext;
        }
        //[HttpGet("{id:int}")]
        //public IActionResult Get1(int id)
        //{
        //    var Product = _myDbContext1.Products.FirstOrDefault(a => a.ProductId == id);
        //    if (Pro

        //    return Ok(Product);
        //}
        // API للحصول على الفئة بناءً على ID
        [HttpGet("prodectbyId/{id}")]
        public IActionResult Get2(int id)
        {
            var Product = _myDbContext1.Products.FirstOrDefault(a => a.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }

        // API للحصول على جميع الفئات
        [HttpGet]
        public IActionResult Get()
        {
            var Product = _myDbContext1.Products.ToList();
            return Ok(Product);
        }
        // API للحصول على الفئة بناءً على Name
        //[HttpGet("byname/{name:alpha}")]
        //public IActionResult GetProductByName1(string name)
        //{
        //    var Product = _myDbContext1.Products.FirstOrDefault(c => c.ProductName == name);
        //    if (Product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(Product);
        //}

        [HttpGet("name/{name}")]
        public IActionResult GetProductByName2(string name)
        {
            var Product = _myDbContext1.Products.FirstOrDefault(c => c.ProductName == name);
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }
        [HttpGet("prodectbycategoryId/{category_id}")]
        public IActionResult GetProductbycategory(int category_id)
        {
            var Product = _myDbContext1.Products.Where(x=>x.CategoryId== category_id).ToList();
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }

        // API للحذف على الفئة بناءً على ID
        //[HttpGet("{id:int}")]
        //public IActionResult DeleteProduct1(int id)
        //{
        //    var Product = _myDbContext1.Products.FirstOrDefault(c => c.ProductId == id);
        //    if (Product == null)
        //    {
        //        return NotFound();
        //    }

        //    _myDbContext1.Products.Remove(Product);
        //    _myDbContext1.SaveChanges();

        //    return NoContent();
        //}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct2(int id)
        {
            var Product = _myDbContext1.Products.FirstOrDefault(c => c.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }

            _myDbContext1.Products.Remove(Product);
            _myDbContext1.SaveChanges();

            return NoContent();
        }
    }
}

