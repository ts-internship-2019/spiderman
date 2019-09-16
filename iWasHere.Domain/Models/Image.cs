using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int TouristAttractionId { get; set; }
        public string Path { get; set; }

        public virtual TouristAttraction TouristAttraction { get; set; }
    }
}
