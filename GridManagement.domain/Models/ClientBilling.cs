using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class ClientBilling
    {
        public ClientBilling()
        {
            ClientBillingLayerDetails = new HashSet<ClientBillingLayerDetails>();
        }

        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string Ipcno { get; set; }
        public DateTime? BillMonth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Clients Client { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual ICollection<ClientBillingLayerDetails> ClientBillingLayerDetails { get; set; }
    }
}
