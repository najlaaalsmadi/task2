using Microsoft.AspNetCore.Mvc;
using task2.DOTS;
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

            if (id <= 0)
            {
                return BadRequest("Invalid ID provided. ID must be greater than 0."); // 400 Bad Request
            }
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
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be null or empty."); // 400 Bad Request
            }
            var category = _myDbContext1.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Addnewcategory([FromForm] categoryRequestDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, categoryDTO.CategoryImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                categoryDTO.CategoryImage.CopyToAsync(stream);
            }

            // إنشاء كائن جديد من فئة Category وتعبئته بالبيانات
            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName,
                CategoryImage = Path.Combine("Uploads", categoryDTO.CategoryImage.FileName) // تعيين مسار الصورة المحفوظة
            };

            // إضافة الكائن إلى قاعدة البيانات
            _myDbContext1.Categories.Add(category);
            _myDbContext1.SaveChanges();

            // إرجاع استجابة بنجاح العملية
            return Ok();
        }

        [HttpPut] // استخدم PUT للتعديل
        public IActionResult Edit(int id, [FromForm] categoryRequestDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, categoryDTO.CategoryImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                categoryDTO.CategoryImage.CopyToAsync(stream);
            }

            var category = _myDbContext1.Categories.FirstOrDefault(l => l.CategoryId == id);
            if (category == null)
            {
                return NotFound($"الفئة بالرقم {id} غير موجودة.");
            }

            category.CategoryName = categoryDTO.CategoryName;

            // تحديث مسار الصورة
            category.CategoryImage = Path.Combine("Uploads", categoryDTO.CategoryImage.FileName);

            _myDbContext1.Categories.Update(category);
            _myDbContext1.SaveChanges();

            return Ok(category); // إعادة الفئة المعدلة أو رسالة نجاح
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
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                // احذف جميع المنتجات المرتبطة بالفئة إذا كانت موجودة
                var products = _myDbContext1.Products.Where(p => p.CategoryId == id).ToList();
                if (products.Any())
                {
                    _myDbContext1.Products.RemoveRange(products);
                }

                // الآن احذف الفئة نفسها
                var category = _myDbContext1.Categories.FirstOrDefault(p => p.CategoryId == id);
                if (category != null)
                {
                    _myDbContext1.Categories.Remove(category);
                    _myDbContext1.SaveChanges();
                    return Ok("Category and related products deleted successfully.");
                }

                return NotFound("Category not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
