using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.County.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountyController : ControllerBase
    {
        private ILogger<CountyController> _logger;
        private static Random rng = new Random();
     

        public CountyController(ILogger<CountyController> logger)
        {
            _logger = logger;
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<List<string>> GetById(int id)
        {
            _logger.LogWarning(" Exam.county.Controllers.GetById methodu çağrıldı.");

            var countyCount = rng.Next(3, 10);
            var countyList = new List<string>();
            for (int i = 0; i < countyCount; i++)
            {
                countyList.Add(RandomString(rng.Next(10, 30)));
            }

            return countyList;
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rng.Next(s.Length)]).ToArray());
        }



    }
}
