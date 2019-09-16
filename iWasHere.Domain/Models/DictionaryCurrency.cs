using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCurrency
    {
        public DictionaryCurrency()
        {
            Currency = new HashSet<Currency>();
            Ticket = new HashSet<Ticket>();
        }

        public int DictionaryCurrencyId { get; set; }
        public string DictionaryCurrencyCode { get; set; }
        public string DictionaryCurrencyName { get; set; }

        public virtual ICollection<Currency> Currency { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
