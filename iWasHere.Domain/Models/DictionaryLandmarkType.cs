﻿using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryLandmarkType
    {
        public DictionaryLandmarkType()
        {
            TouristAttraction = new HashSet<TouristAttraction>();
        }

        public int DictionaryItemId { get; set; }
        public string DictionaryItemCode { get; set; }
        public string DictionaryItemName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TouristAttraction> TouristAttraction { get; set; }
    }
}
