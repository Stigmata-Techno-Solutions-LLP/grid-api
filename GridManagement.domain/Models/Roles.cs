﻿using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Roles
    {
        public Roles()
        {
            RolesApplicationforms = new HashSet<RolesApplicationforms>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Level { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<RolesApplicationforms> RolesApplicationforms { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
