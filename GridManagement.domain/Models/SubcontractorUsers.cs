using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class SubcontractorUsers
    {
        public int Id { get; set; }
        public int? SubcontId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Subcontractors Subcont { get; set; }
        public virtual Users User { get; set; }
    }
}
