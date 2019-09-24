using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class TouristAttraction
    {
        public TouristAttraction()
        {
            Image = new HashSet<Image>();
            Review = new HashSet<Review>();
            Schedule = new HashSet<Schedule>();
            Ticket = new HashSet<Ticket>();
        }

        public int TouristAttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public int LandmarkId { get; set; }
        public string Longtitudine { get; set; }
        public string Latitudine { get; set; }

        public virtual DictionaryCategory Category { get; set; }
        public virtual DictionaryCity City { get; set; }
        public virtual DictionaryLandmarkType Landmark { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
