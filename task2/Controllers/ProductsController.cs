using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task2.DOTS;
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
        [HttpGet("Getpricemax")]
        public IActionResult Getpricemax()
        {
            var Product = _myDbContext1.Products.OrderByDescending(a => a.Price).ToList();

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


        [HttpPost]
        public async Task<IActionResult> AddnewProduct([FromForm] ProductRequestDTO ProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // تحقق من وجود الصورة المرفوعة
            if (ProductDTO.ProductImage != null && ProductDTO.ProductImage.Length > 0)
            {
                // تحديد مسار حفظ الصور
                var folderPath = Path.Combine("wwwroot", "images", "products");
                Directory.CreateDirectory(folderPath);

                // إنشاء اسم فريد للملف باستخدام GUID
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProductDTO.ProductImage.FileName);

                // تحديد المسار الكامل للملف
                var filePath = Path.Combine(folderPath, uniqueFileName);

                // فتح ملف جديد وحفظ الصورة فيه
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // نسخ بيانات الصورة المرفوعة إلى الملف
                    await ProductDTO.ProductImage.CopyToAsync(fileStream);
                }

                // حفظ المسار النسبي للصورة لاستخدامه لاحقًا
                string imagePath = "/images/products/" + uniqueFileName;

                // البحث عن الفئة بناءً على CategoryId
                var category = await _myDbContext1.Categories.FindAsync(ProductDTO.CategoryId);

                // التحقق من وجود الفئة
                if (category == null)
                {
                    return BadRequest("Category not found");
                }

                // إنشاء كائن جديد من فئة Product وتعبئته بالبيانات
                var product = new Product
                {
                    ProductName = ProductDTO.ProductName,
                    Description = ProductDTO.Description,
                    Price = ProductDTO.Price,
                    CategoryId = ProductDTO.CategoryId,
                    ProductImage = imagePath
                };

                _myDbContext1.Products.Add(product); // إضافة المنتج إلى قاعدة البيانات
                await _myDbContext1.SaveChangesAsync(); // حفظ التغييرات

                return Ok(); // إرجاع استجابة بنجاح العملية
            }

            return BadRequest("No image was uploaded"); // إذا لم يتم رفع صورة، إرجاع خطأ
        }
        [HttpPut]
        public IActionResult Updateprodect(int id, [FromForm] ProductRequestDTO ProductDTO)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, ProductDTO.ProductImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 ProductDTO.ProductImage.CopyToAsync(stream);
            }
            var product=_myDbContext1.Products.FirstOrDefault(L=>L.ProductId == id);
            product.ProductName = ProductDTO.ProductName;
            product.Description = ProductDTO.Description;
            product.Price = ProductDTO.Price;
            product.CategoryId = ProductDTO.CategoryId;
            product.ProductImage= product.ProductImage;
            _myDbContext1.Products.Update(product);
            _myDbContext1.SaveChanges();



            return Ok();
        }


    }
}

