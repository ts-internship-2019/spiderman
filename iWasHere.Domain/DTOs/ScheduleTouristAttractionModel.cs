using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
   public  class ScheduleTouristAttractionModel
    {
        public string Day { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }

        
        public string Season { get; set;}

        public string TouristAttractionName { get; set; }
        public int TouristAttractionId { get; set; }


    }
}
