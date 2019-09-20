using iWasHere.Domain.Models;
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

        public int ScheduleId { get; set; }
        
        public string Season { get; set;}
        public int SeasonId { get; set; }
        public DictionarySeason Seasonn { get; set; }
        public TouristAttraction Tourist { get; set; }
        public string TouristAttractionName { get; set; }
        public int TouristAttractionId { get; set; }


    }
}
