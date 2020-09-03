using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Subcontractors
    {
        public Subcontractors()
        {
            LayerSubcontractors = new HashSet<LayerSubcontractors>();
            SubcontractorUsers = new HashSet<SubcontractorUsers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual ICollection<LayerSubcontractors> LayerSubcontractors { get; set; }
        public virtual ICollection<SubcontractorUsers> SubcontractorUsers { get; set; }
    }
}
