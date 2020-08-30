using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class GridGeolocations
    {
        public int Id { get; set; }
        public int GridId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public virtual Grids Grid { get; set; }
    }
}
