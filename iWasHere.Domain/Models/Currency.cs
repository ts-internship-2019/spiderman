using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Currency
    {
        public int CurrencyId { get; set; }
        public int DictionaryCurrencyId { get; set; }
        public DateTime Data { get; set; }
        public string CurrencyCode { get; set; }
        public string Value { get; set; }

        public virtual DictionaryCurrency DictionaryCurrency { get; set; }
    }
}
