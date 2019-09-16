using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCategory
    {
        public DictionaryCategory()
        {
            TouristAttraction = new HashSet<TouristAttraction>();
        }

        public int DictionaryCategoryId { get; set; }
        public string DictionaryCategoryName { get; set; }

        public virtual ICollection<TouristAttraction> TouristAttraction { get; set; }
    }
}
