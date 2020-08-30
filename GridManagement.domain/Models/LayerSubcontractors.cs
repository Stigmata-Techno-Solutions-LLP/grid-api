using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class LayerSubcontractors
    {
        public int Id { get; set; }
        public int LayerdetailsId { get; set; }
        public int? SubcontractorId { get; set; }
        public int? Quantity { get; set; }

        public virtual LayerDetails Layerdetails { get; set; }
        public virtual Subcontractors Subcontractor { get; set; }
    }
}
