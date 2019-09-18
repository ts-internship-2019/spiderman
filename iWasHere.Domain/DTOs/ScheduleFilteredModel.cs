using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
   public  class ScheduleFilteredModel
    {

        public int TouristAttractionId { get; set; }

        public string TouristAttractionName { get; set; }
        public string CurrentFilter { get; set; }


    }
}
