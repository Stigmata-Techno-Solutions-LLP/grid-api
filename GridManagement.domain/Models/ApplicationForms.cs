using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class ApplicationForms
    {
        public ApplicationForms()
        {
            RolesApplicationforms = new HashSet<RolesApplicationforms>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsAdd { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsView { get; set; }
        public bool? IsViewOnly { get; set; }

        public virtual ICollection<RolesApplicationforms> RolesApplicationforms { get; set; }
    }
}
