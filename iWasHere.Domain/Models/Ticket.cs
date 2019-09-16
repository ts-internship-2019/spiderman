using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketPayment = new HashSet<TicketPayment>();
        }

        public int TicketId { get; set; }
        public int TicketPrice { get; set; }
        public int TouristAttractionId { get; set; }
        public int CurrencyId { get; set; }

        public virtual DictionaryCurrency Currency { get; set; }
        public virtual TouristAttraction TouristAttraction { get; set; }
        public virtual ICollection<TicketPayment> TicketPayment { get; set; }
    }
}
