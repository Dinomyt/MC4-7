using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // [HttpGet]
        // public string SayHello(string customName)
        // {
        //     return "Hello " + customName;
        // }

        [HttpGet("sayHello")]
        public string SayHello([FromQuery] string customName)
        {
            return "Hello " + customName;
        }

        [HttpGet("addTwo")]
        public string addTwo(int x, int y)
        {
             return $"The sum of {x} + {y} is: {x + y}";
        }

        [HttpGet("wokeUp")]
        public string wokeUp(string name, string wakeUp)
        {
            return $"{name} woke up at {wakeUp}";
        }
         [HttpGet("comparison")]
         public string comparison(int x, int y)
         {
            if (x == y)
            {
                return $"{x} is equal to {y}";
            }
            else if (x > y)
            {
                return $"{x} is greater than {y}\n{y} is less than {x}";
            }
            else
            {
                return $"{x} is less than {y}\n{y} is greater than {x}";    
            }    
         }
    }
}