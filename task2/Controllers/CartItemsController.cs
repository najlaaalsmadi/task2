using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task2.DOTS;
using task2.Models;

namespace task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly MyDbContext _myDbContext1;
        public CartItemsController(MyDbContext myDbContext)
        {
            _myDbContext1 = myDbContext;
        }
        [HttpGet]
        public IActionResult GetAlldata()
        {
            var getData = _myDbContext1.CartItems.Select(x =>
            new CartItemsResponseDTO
            {
              CartId = x.CartId,
              CartItemId = x.CartItemId,
              Quantity = x.Quantity,
              productRequestDTO = new ProductRequestDTO1
              {
               ProductName =x.Product.ProductName,
               Description=x.Product.Description,
                  Price=x.Product.Price,
                  CategoryId=x.Product.CategoryId,

              }
            }
            ).ToList();

            return Ok(getData);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var getData = _myDbContext1.CartItems.Select(x =>
            new CartItemsResponseDTO
            {
                CartId = x.CartId,
                CartItemId = x.CartItemId,
                Quantity = x.Quantity,
                productRequestDTO = new ProductRequestDTO1
                {
                    ProductName = x.Product.ProductName,
                    Description = x.Product.Description,
                    Price = x.Product.Price,
                    CategoryId = x.Product.CategoryId,

                }
            }
            ).ToList();

            return Ok(getData);
        }
        [HttpPost]
        public IActionResult addcart([FormBody] AddcardItemrequest cart)
        {
            var data = new CartItem
            {
                CartId = cart.CartId,
                Quantity = cart.Quantity,
                ProductId = cart.ProductId,
            };
            _myDbContext1.CartItems.Add(data);
            _myDbContext1.SaveChanges();

            return Ok();
        }



    }

    internal class FormBodyAttribute : Attribute
    {
    }
}
