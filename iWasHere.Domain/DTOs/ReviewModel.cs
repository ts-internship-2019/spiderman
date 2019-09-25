using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class ReviewModel
    {

        public int ReviewId { get; set; }

        public string User { get; set; }

      

        public int TouristAttractionId { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public int RatingValue { get; set; }


    }
}
