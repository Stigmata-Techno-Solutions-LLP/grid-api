using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Grids
    {
        public Grids()
        {
            GridGeolocations = new HashSet<GridGeolocations>();
            LayerDetails = new HashSet<LayerDetails>();
        }

        public int Id { get; set; }
        public string Gridno { get; set; }
        public decimal? GridArea { get; set; }
        public string Status { get; set; }
        public bool? IsActive { get; set; }
        public string CgRfino { get; set; }
        public DateTime? CgInspectionDate { get; set; }
        public DateTime? CgApprovalDate { get; set; }
        public string CgRfiStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Users UpdatedByNavigation { get; set; }
        public virtual ICollection<GridGeolocations> GridGeolocations { get; set; }
        public virtual ICollection<LayerDetails> LayerDetails { get; set; }
    }
}
