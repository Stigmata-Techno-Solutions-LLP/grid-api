﻿using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Userroles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
    }
}
