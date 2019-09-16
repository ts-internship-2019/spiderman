using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionarySeason
    {
        public DictionarySeason()
        {
            Schedule = new HashSet<Schedule>();
        }

        public int DictionarySeasonId { get; set; }
        public string DictionarySeasonName { get; set; }

        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}
