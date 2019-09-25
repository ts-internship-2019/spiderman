using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TouristAttractionId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
