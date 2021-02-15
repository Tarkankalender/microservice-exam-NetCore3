using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.City
{
    public class City
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Population { get; set; }
        public string Region { get; set; }
    }
    public class GetByIdResponse
    {
        public City city { get; set; }
        public List<string> county { get; set; }
    }
}
