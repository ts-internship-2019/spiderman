using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public int TouristAttractionId { get; set; }
        public int SeasonId { get; set; }
        public string Day { get; set; }
        public TimeSpan? StartHour { get; set; }
        public TimeSpan? EndHour { get; set; }

        public virtual DictionarySeason Season { get; set; }
        public virtual TouristAttraction TouristAttraction { get; set; }
    }
}
