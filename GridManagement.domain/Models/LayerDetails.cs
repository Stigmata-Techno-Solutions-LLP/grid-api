using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class LayerDetails
    {
        public LayerDetails()
        {
            ClientBillingLayerDetails = new HashSet<ClientBillingLayerDetails>();
            LayerDocuments = new HashSet<LayerDocuments>();
            LayerSubcontractors = new HashSet<LayerSubcontractors>();
        }

        public int Id { get; set; }
        public int GridId { get; set; }
        public int LayerId { get; set; }
        public DateTime? FillingDate { get; set; }
        public string FillingMaterial { get; set; }
        public decimal? AreaLayer { get; set; }
        public int? TotalQuantity { get; set; }
        public string FillType { get; set; }
        public string ToplevelFillmaterial { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CtRfino { get; set; }
        public DateTime? CtInspectionDate { get; set; }
        public DateTime? CtApprovalDate { get; set; }
        public string CtRfiStatus { get; set; }
        public string LvRfino { get; set; }
        public DateTime? LvInspectionDate { get; set; }
        public DateTime? LvApprovalDate { get; set; }
        public string LvRfiStatus { get; set; }
        public bool? IsBillGenerated { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Grids Grid { get; set; }
        public virtual Layers Layer { get; set; }
        public virtual Users UpdatedByNavigation { get; set; }
        public virtual ICollection<ClientBillingLayerDetails> ClientBillingLayerDetails { get; set; }
        public virtual ICollection<LayerDocuments> LayerDocuments { get; set; }
        public virtual ICollection<LayerSubcontractors> LayerSubcontractors { get; set; }
    }
}
