using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task2.Models;

namespace task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Orders1Controller : ControllerBase
    {
        private readonly MyDbContext _myDbContext1;
        public Orders1Controller(MyDbContext myDbContext)
        {
            _myDbContext1 = myDbContext;
        }

        //API للحصول على كل طلبات بناءً على ID
       [HttpGet("{id}")]
        public IActionResult Get2(int id)
        {
            var OrderId = _myDbContext1.Orders.FirstOrDefault(a => a.OrderId == id);
            if (OrderId == null)
            {
                return NotFound();
            }
            return Ok(OrderId);
        }
        [HttpGet("{OrderDate1}")]
        public IActionResult Get2(DateOnly OrderDate1)
        {
            var OrderId = _myDbContext1.Orders.FirstOrDefault(a => a.OrderDate == OrderDate1);
            if (OrderId == null)
            {
                return NotFound();
            }
            return Ok(OrderId);
        }

        // API للحصول على جميع طلبات
        [HttpGet]
        public IActionResult Get()
        {
            var Orders = _myDbContext1.Orders.ToList();
            return Ok(Orders);
        }


        // API للحذف على طلب بناءً على ID

        [HttpDelete("{id}")]
        public IActionResult DeleteOrders1(int id)
        {
            var Orders = _myDbContext1.Orders.FirstOrDefault(a => a.UserId == id);
            if (Orders == null)
            {
                return NotFound();
            }

            _myDbContext1.Orders.Remove(Orders);
            _myDbContext1.SaveChanges();

            return NoContent();
        }
        //[HttpGet("{id:int}")]
        //public IActionResult DeleteOrders2(int id)
        //{
        //    var Orders = _myDbContext1.Orders.FirstOrDefault(a => a.OrderId == id);
        //    if (Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    _myDbContext1.Orders.Remove(Orders);
        //    _myDbContext1.SaveChanges();

        //    return NoContent();
        //}
    }
}
