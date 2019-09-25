using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class TouristAttractionModel
    {
        public int TouristAttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CountryName { get; set; }
        public string CountyName { get; set; }

        public string Longtitudine { get; set; }
        public string Latitudine { get; set; }

        public IEnumerable<string> Image { get; set; }

        public string  CategoryName { get; set; }
        public string  CityName { get; set; }
        public string  LandmarkTypeName { get; set; }

      
      

    }
}
