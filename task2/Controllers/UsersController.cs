using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task2.DOTS;
using task2.Models;

namespace task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _myDbContext1;
        public UsersController(MyDbContext myDbContext)
        {
            _myDbContext1 = myDbContext;
        }
        [HttpGet("{id:int}")]
        public IActionResult Get1(int id)
        {
            var USERid = _myDbContext1.Users.FirstOrDefault(a => a.UserId == id);
            if (USERid == null)
            {
                return NotFound();
            }
            return Ok(USERid);
        }

        // API للحصول على الفئة بناءً على ID
        //[HttpGet("{id}")]
        //public IActionResult Get2(int id)
        //{
        //    var USERid = _myDbContext1.Users.FirstOrDefault(a => a.UserId == id);
        //    if (USERid == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(USERid);
        //}

        // API للحصول على جميع الفئات
        [HttpGet]
        public IActionResult Get()
        {
            var user = _myDbContext1.Users.ToList();
            return Ok(user);
        }
        // API للحصول على الفئة بناءً على Name

        [HttpGet("name/{name}")]
        public IActionResult GetUserByName1(string name)
        {
            var USERname = _myDbContext1.Users.FirstOrDefault(a => a.Username == name);
            if (USERname == null)
            {
                return NotFound();
            }
            return Ok(USERname);
        }
        //[HttpGet("byname/{name:alpha}")]
        //public IActionResult GetUserByName2(string name)
        //{
        //    var USERname = _myDbContext1.Users.FirstOrDefault(a => a.Username == name);
        //    if (USERname == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(USERname);
        //}

        // API للحذف على الفئة بناءً على ID

        [HttpDelete("{id}")]
        public IActionResult DeleteUser1(int id)
        {
            var USERid = _myDbContext1.Users.FirstOrDefault(a => a.UserId == id);
            if (USERid == null)
            {
                return NotFound();
            }

            _myDbContext1.Users.Remove(USERid);
            _myDbContext1.SaveChanges();

            return NoContent();
        }
        //[HttpGet("{id:int}")]
        //public IActionResult DeleteUser2(int id)
        //{
        //    var USERid = _myDbContext1.Users.FirstOrDefault(a => a.UserId == id);
        //    if (USERid == null)
        //    {
        //        return NotFound();
        //    }

        //    _myDbContext1.Users.Remove(USERid);
        //    _myDbContext1.SaveChanges();

        //    return NoContent();
        //}


        [HttpPost]
        public IActionResult Careateuser([FromForm] usersRequestDOT usersRequestDOT)
        {
            if (!ModelState.IsValid) { 
            return BadRequest(ModelState);
        }
            var user = new User
            {
                Username = usersRequestDOT.Username,

                Password = usersRequestDOT.Password,

                Email = usersRequestDOT.Email,
            };
            _myDbContext1.Users.Add(user);
            _myDbContext1.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Edituser(int id, [FromForm] usersRequestDOT usersRequestDOT)
        {
            var user = _myDbContext1.Users.FirstOrDefault(l=>l.UserId == id);
            user.Username= usersRequestDOT.Username;
            user.Password= usersRequestDOT.Password;
            user.Email= usersRequestDOT.Email;
            _myDbContext1.Users.Update(user); 
            _myDbContext1.SaveChanges();
            return Ok();
        }
        [HttpGet("math")]
        public IActionResult MATH(string input) { 
            var x=input.Split(' ');
            var num1=Convert.ToDouble(x[0]);
            var op=x[1];
            var num2=Convert.ToDouble(x[2]);
            double result = 0;
            switch(op)
            {
                    case ("+"):
                    result=num1+ num2;
                    break;
                    case ("-"):
                    result=num1- num2;
                    break;
                    case ("*"):
                    result=num1*num2;
                    break;
            }

        return Ok(result);
        }
        [HttpGet("check")]

        public IActionResult check1(int num1, int num2)
        {
            bool result = false;
            if (num1 == 30 || num2 == 30)
            {
                result = true;
            }
            else if (num2 + num1 == 30)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return Ok(result);
        }
        [HttpGet("check3")]
        public IActionResult check3(int num)
        {
            bool result = false;
            if((num%3==0 || num % 7 == 0)&&num>0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return Ok(result);
        }
    }
}
