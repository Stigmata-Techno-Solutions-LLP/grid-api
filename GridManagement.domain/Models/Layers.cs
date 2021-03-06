﻿using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Layers
    {
        public Layers()
        {
            LayerDetails = new HashSet<LayerDetails>();
        }

        public int Id { get; set; }
        public string Layerno { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LayerDetails> LayerDetails { get; set; }
    }
}
