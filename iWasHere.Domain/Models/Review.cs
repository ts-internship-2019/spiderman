using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TouristAttractionId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public virtual TouristAttraction TouristAttraction { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
