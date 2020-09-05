using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class ClientBillingLayerDetails
    {
        public int Id { get; set; }
        public int? ClientBillingId { get; set; }
        public int? LayerDetailsId { get; set; }

        public virtual LayerDetails LayerDetails { get; set; }
    }
}
