using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCity
    {
        public DictionaryCity()
        {
            TouristAttraction = new HashSet<TouristAttraction>();
        }

        public int DictionaryCityId { get; set; }
        public int DictionaryCountryId { get; set; }
        public string DictionaryCityName { get; set; }

        public virtual DictionaryCounty DictionaryCountry { get; set; }
        public virtual ICollection<TouristAttraction> TouristAttraction { get; set; }
    }
}
