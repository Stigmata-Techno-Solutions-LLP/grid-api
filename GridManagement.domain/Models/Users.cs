﻿using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Users
    {
        public Users()
        {
            AuditLogs = new HashSet<AuditLogs>();
            ClientBilling = new HashSet<ClientBilling>();
            GridsCreatedByNavigation = new HashSet<Grids>();
            GridsUpdatedByNavigation = new HashSet<Grids>();
            LayerDetailsCreatedByNavigation = new HashSet<LayerDetails>();
            LayerDetailsUpdatedByNavigation = new HashSet<LayerDetails>();
            LayerDocuments = new HashSet<LayerDocuments>();
            SubcontractorUsers = new HashSet<SubcontractorUsers>();
            Subcontractors = new HashSet<Subcontractors>();
            Userroles = new HashSet<Userroles>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<AuditLogs> AuditLogs { get; set; }
        public virtual ICollection<ClientBilling> ClientBilling { get; set; }
        public virtual ICollection<Grids> GridsCreatedByNavigation { get; set; }
        public virtual ICollection<Grids> GridsUpdatedByNavigation { get; set; }
        public virtual ICollection<LayerDetails> LayerDetailsCreatedByNavigation { get; set; }
        public virtual ICollection<LayerDetails> LayerDetailsUpdatedByNavigation { get; set; }
        public virtual ICollection<LayerDocuments> LayerDocuments { get; set; }
        public virtual ICollection<SubcontractorUsers> SubcontractorUsers { get; set; }
        public virtual ICollection<Subcontractors> Subcontractors { get; set; }
        public virtual ICollection<Userroles> Userroles { get; set; }
    }
}