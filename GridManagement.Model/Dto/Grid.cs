using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GridManagement.common;
namespace GridManagement.Model.Dto
{

    public class AddGrid
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "Grid Number")]
        public string gridno { get; set; }

        [Display(Name = "Grid Area")]
        public decimal grid_area { get; set; }

        public bool isCompleted { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int user_id { get; set; }

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

        public int grid_id { get; set; }
    }

    public class GridNo {
        public int Id{get;set;}
        public string gridName{get;set;}
    }

    public class gridFilter {
        public  string gridId {get;set;}

        public  string gridNo {get;set;}
        public string status { get; set; }

        [Display(Name = "RFI No")]
        public string CG_RFIno { get; set; }
                
        [EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus? CG_RFI_status { get; set;} = null;
    }


    public class GridDetails
    {
       public int gridId { get; set; }
        public string gridno { get; set; }

        public decimal grid_area { get; set; }

        public bool isCompleted { get; set; }

        public string CG_RFIno { get; set; }

        public string CG_inspection_date { get; set; }

        public string CG_approval_date { get; set; }

        [EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus? CG_RFI_status { get; set;} =null;

        public List<GridGeoLocation> gridGeoLocation { get; set;}
        public DateTime createdAt {get;set;}
        public DateTime updatedAt {get;set;}
          public string createdBy {get;set;}
        public string updatedBy {get;set;}

    }


public class AddCG_RFI {
        [Required]
        [Display(Name = "User Id")]
        public int user_id { get; set; }

        [Required]
        [Display(Name = "Grid Id")]
        public int grid_id { get; set; }
        [Required]
        [Display(Name = "RFI No")]
        public string CG_RFIno { get; set; }

        [Display(Name = "RFI Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime CG_inspection_date { get; set; }


        [Display(Name = "RFI Approval Date")]
        [DataType(DataType.Date)]
        public DateTime CG_approval_date { get; set; }

        [EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus CG_RFI_status { get; set;}

}
}
