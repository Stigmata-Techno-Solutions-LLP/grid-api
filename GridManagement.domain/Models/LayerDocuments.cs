using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class LayerDocuments
    {
        public int Id { get; set; }
        public int LayerdetailsId { get; set; }
        public string Path { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual LayerDetails Layerdetails { get; set; }
    }
}
