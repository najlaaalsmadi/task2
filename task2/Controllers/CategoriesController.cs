using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Addnewcategory([FromForm] categoryRequestDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // تحديد متغير لحفظ مسار الصورة
            string imagePath = null;

            // التحقق من وجود صورة مرفوعة
            if (categoryDTO.CategoryImage != null && categoryDTO.CategoryImage.Length > 0)
            {
                // تحديد مكان حفظ الصورة على السيرفر
                var folderPath = Path.Combine("wwwroot", "img");
                Directory.CreateDirectory(folderPath);

                // إنشاء اسم فريد للصورة باستخدام GUID
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryDTO.CategoryImage.FileName);

                // تحديد المسار الكامل للملف
                var fullPath = Path.Combine(folderPath, uniqueFileName);

                // فتح ملف جديد وحفظ الصورة فيه
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    // نسخ بيانات الصورة المرفوعة إلى الملف
                    await categoryDTO.CategoryImage.CopyToAsync(fileStream);
                }

                // حفظ المسار النسبي للصورة لاستخدامه لاحقًا
                imagePath = "/img/" + uniqueFileName;
            }

            // إنشاء كائن جديد من فئة Category وتعبئته بالبيانات
            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName,
                CategoryImage = imagePath // تعيين مسار الصورة المحفوظة
            };

            // إضافة الكائن إلى قاعدة البيانات
            _myDbContext1.Categories.Add(category);
            _myDbContext1.SaveChanges();

            // إرجاع استجابة بنجاح العملية
            return Ok();
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
