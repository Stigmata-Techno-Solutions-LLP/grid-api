using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GridManagement.Model.Dto
{

    public class AddGrid
    {

       public int gridId { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Grid Number")]
        public string gridno { get; set; }

        [Display(Name = "Grid Area")]
        public decimal grid_area { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool is_active { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int user_id { get; set; }

        [Display(Name = "RFI No")]
        public string CG_RFIno { get; set; }

        [Display(Name = "RFI Inspection Date")]
        public string CG_inspection_date { get; set; }


        [Display(Name = "RFI Approval Date")]
        public string CG_approval_date { get; set; }


        [Display(Name = "RFI Status")]
        public string CG_RFI_status { get; set; }

        [Required]
        public List<GridGeoLocation> gridGeoLocation { get; set;}

    }

    public class GridGeoLocation
    {
        [Required]
        [Display(Name = "Latitide Value")]
        public decimal latitude { get; set; }

        [Required]
        [Display(Name = "Longitide Value")]
        public decimal longitude { get; set; }
    }
}
