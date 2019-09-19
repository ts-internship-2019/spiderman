using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryPayment
    {
        public DictionaryPayment()
        {
            TicketPayment = new HashSet<TicketPayment>();
        }

        public int DictionaryPaymentId { get; set; }
        public string DictionaryPaymentType { get; set; }

        public virtual ICollection<TicketPayment> TicketPayment { get; set; }
    }
}
