using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GridManagement.common;
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
        [DataType(DataType.Date)]
        public string CG_inspection_date { get; set; }


        [Display(Name = "RFI Approval Date")]
        [DataType(DataType.Date)]
        public string CG_approval_date { get; set; }



        
[EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
public commonEnum.CG_RFIStatus CG_RFI_status { get; set;}

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

    public class grid {
        public int Id{get;set;}
        public string gridNo{get;set;}
    }

    public class gridFilter {
        public  string gridId {get;set;}

        public  string gridNo {get;set;}
        public bool status { get; set; }

        [Display(Name = "RFI No")]
        public string CG_RFIno { get; set; }

                
        [EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus CG_RFI_status { get; set;}

    }
}
