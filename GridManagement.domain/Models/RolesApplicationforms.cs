using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class RolesApplicationforms
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int RoleId { get; set; }
        public bool? IsAdd { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsView { get; set; }

        public virtual ApplicationForms Form { get; set; }
        public virtual Roles Role { get; set; }
    }
}
