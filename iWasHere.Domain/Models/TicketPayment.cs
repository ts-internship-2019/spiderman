using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class TicketPayment
    {
        public int TicketPaymentId { get; set; }
        public int TicketId { get; set; }
        public int PaymentId { get; set; }

        public virtual DictionaryPayment Payment { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
