using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Users
    {
        public Users()
        {
            AuditLogs = new HashSet<AuditLogs>();
            ClientBilling = new HashSet<ClientBilling>();
            GridDocuments = new HashSet<GridDocuments>();
            GridsCreatedByNavigation = new HashSet<Grids>();
            GridsUpdatedByNavigation = new HashSet<Grids>();
            LayerDetailsCreatedByNavigation = new HashSet<LayerDetails>();
            LayerDetailsUpdatedByNavigation = new HashSet<LayerDetails>();
            LayerDocuments = new HashSet<LayerDocuments>();
            SubcontractorsCreatedByNavigation = new HashSet<Subcontractors>();
            SubcontractorsUpdatedByNavigation = new HashSet<Subcontractors>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<AuditLogs> AuditLogs { get; set; }
        public virtual ICollection<ClientBilling> ClientBilling { get; set; }
        public virtual ICollection<GridDocuments> GridDocuments { get; set; }
        public virtual ICollection<Grids> GridsCreatedByNavigation { get; set; }
        public virtual ICollection<Grids> GridsUpdatedByNavigation { get; set; }
        public virtual ICollection<LayerDetails> LayerDetailsCreatedByNavigation { get; set; }
        public virtual ICollection<LayerDetails> LayerDetailsUpdatedByNavigation { get; set; }
        public virtual ICollection<LayerDocuments> LayerDocuments { get; set; }
        public virtual ICollection<Subcontractors> SubcontractorsCreatedByNavigation { get; set; }
        public virtual ICollection<Subcontractors> SubcontractorsUpdatedByNavigation { get; set; }
    }
}
