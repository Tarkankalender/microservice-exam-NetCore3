using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net.Http;
using Exam.City.Helper;
//using Newtonsoft.Json;

namespace Exam.City.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private List<City> _cities;
        private readonly ILogger<CityController> _loggoer;


        public CityController(ILogger<CityController> logger)
        {
            _loggoer = logger;
            using (var sr = new StreamReader("data/cities.json", Encoding.UTF8))
            {
                var jsonString = sr.ReadToEnd();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                };


                _cities = JsonSerializer.Deserialize<List<City>>(jsonString, options);
                _loggoer.LogWarning("cities serisi listeye eklemmesi başarılı");
            }
        }

        [HttpGet]
        public IEnumerable<City> Get()
        {
            _loggoer.LogInformation(" IEnumerable<city> Get() methodu çağrıldı.");
            return _cities;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<GetByIdResponse> GetById(int id)
        {
            _loggoer.LogWarning(" Exam.City.Controllers.GetById methodu çağrıldı");
            
            var city = _cities.FirstOrDefault(a => a.Id == id);
            var countyList = new List<string>();

            var retVal = new GetByIdResponse();
            retVal.city = city;

            using (RestClientHelper r = new RestClientHelper())
            {
                var result = await r.GetAsync("http://localhost:7002/api/county/" + id, HttpContext);
                countyList = JsonSerializer.Deserialize<List<string>>(result);
            }
            retVal.county = countyList;

            return retVal;
        }
    }
}
