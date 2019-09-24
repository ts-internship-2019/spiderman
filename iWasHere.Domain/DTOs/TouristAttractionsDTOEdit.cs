using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class TouristAttractionsDTOEdit
    {
        public int TouristAttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longtitudine { get; set; }
        public string Latitudine { get; set; }


        public int CityId { get; set; }
        public int LandmarkId { get; set; }
        public int CategoryId { get; set; }
    }
}
