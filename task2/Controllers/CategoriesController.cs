using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task2.Models;

namespace task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _myDbContext1;
        public CategoriesController(MyDbContext myDbContext)
        {
            _myDbContext1 = myDbContext;
        }
        //[HttpGet("{id:int}")]
        //public IActionResult Get1(int id)
        //{
        //    var category = _myDbContext1.Categories.FirstOrDefault(a => a.CategoryId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(category);
        //}

        //API للحصول على الفئة بناءً على ID
       [HttpGet("{id}")]
        public IActionResult Get2(int id)
        {
            var category = _myDbContext1.Categories.FirstOrDefault(a => a.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // API للحصول على جميع الفئات
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _myDbContext1.Categories.ToList();
            return Ok(categories);
        }
        // API للحصول على الفئة بناءً على Name

        [HttpGet("name/{name}")]
        public IActionResult GetCategoryByName1(string name)
        {
            var category = _myDbContext1.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        //[HttpGet("byname/{name:alpha}")]
        //public IActionResult GetCategoryByName2(string name)
        //{
        //    var category = _myDbContext1.Categories.FirstOrDefault(c => c.CategoryName == name);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(category);
        //}

        // API للحذف على الفئة بناءً على ID

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory1(int id)
        {
            var category = _myDbContext1.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _myDbContext1.Categories.Remove(category);
            _myDbContext1.SaveChanges();

            return NoContent();
        }
        //[HttpGet("{id:int}")]
        //public IActionResult DeleteCategory2(int id)
        //{
        //    var category = _myDbContext1.Categories.FirstOrDefault(c => c.CategoryId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _myDbContext1.Categories.Remove(category);
        //    _myDbContext1.SaveChanges();

        //    return NoContent();
        //}
    }
}
