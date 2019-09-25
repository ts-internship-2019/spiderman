using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class TouristAttractionsDTO
    {
        //public TouristAttraction()
        //{
        //    Image = new HashSet<Image>();
        //    Review = new HashSet<Review>();
        //    Schedule = new HashSet<Schedule>();
        //    Ticket = new HashSet<Ticket>();
        //}

        public int TouristAttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longtitudine { get; set; }
        public string Latitudine { get; set; }


        public string CityName { get; set; }
        public string LandmarkName { get; set; }
        public string CategoryName { get; set; }
        public string FirstPhotoPath { get; set; }
        public int CountryId { get; set; }
    }
}
