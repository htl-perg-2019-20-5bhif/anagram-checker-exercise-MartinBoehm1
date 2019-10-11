using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace anagram.Controllers
{
    [ApiController]
    [Route("anagram")]
    public class AnagramController : ControllerBase
    {

        private readonly ILogger<AnagramController> _logger;

        public AnagramController(ILogger<AnagramController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("checkAnagram")]
        public ActionResult checkForAnagram([FromBody] Anagram values)
        {
            var value1 = values.W1;
            var value2 = values.W2;
            Logic l = new Logic();
            if (l.checkForAnagram(value1, value2))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("getKnownAnagrams/{value}", Name = "GetSpecificItem")]
        public ActionResult getAnagrams(string value)
        {
            Logic l = new Logic();
            List<string> s = new List<string>();
            s.Add(value);
            Console.WriteLine(value);
            var result="";
            var first = true;
            foreach(var i in l.getAnagrams(value, s))
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    result += i + " ";
                }
                
            }
            return Ok(result);
        }
    }
}
