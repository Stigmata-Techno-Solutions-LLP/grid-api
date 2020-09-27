using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GridManagement.common;
using Microsoft.AspNetCore.Http;  

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

        public bool status { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int user_id { get; set; }

        [Required]
        public List<GridGeoLocation> gridGeoLocation { get; set;}

        [Required]
        [Range(-180,180)]
        [Display(Name = "Marker Latitude Value")]
        public double? marker_latitide { get; set; } = null;

        [Required]
        [Range(-180,180)]
        [Display(Name = "Marker Longitide Value")]
        public double? marker_longitude { get; set; } = null;       

    }
    public class GridGeoLocation
    {

        [Required]
        [Range(-90,90)]
        [Display(Name = "Longitide Value")]
public double? latitude {get;set;} = null;

        [Required]
        [Range(-180,180)]
        [Display(Name = "Longitide Value")]
    public double? longitude {get;set;} = null;
    

        //[RegularExpression( @"^(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)$",ErrorMessage="")]
       // [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$", ErrorMessage="Longitude value not valid")]
       
   //     public string longitude { get; set; }

    }

    public class RemoveDocs {

    }


    public class GridDocs
    {
        [Required]
        public int fileId { get; set; }
        public string fileName {get;set;}
        public string fileUrl{get;set;}

    }

    public class GridNo {
        public int Id{get;set;}
        public string gridName{get;set;}
    }

    public class gridFilter {
        public  int? gridId {get;set;} = null;

        public  string gridNo {get;set;}
        public string status { get; set; }

        [Display(Name = "RFI No")]
        public string CG_RFIno { get; set; }
                
        [EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI Status value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus? CG_RFI_status { get; set;} = null;
    }


    public class GridDetails
    {
       public int gridId { get; set; }
        public string gridno { get; set; }

        public decimal grid_area { get; set; }

        public string status { get; set; }

        public string CG_RFIno { get; set; }

        public string CG_inspection_date { get; set; }

        public string CG_approval_date { get; set; }

        public double marker_latitide { get; set; }
        public double marker_longitude { get; set; }

        public string CG_RFI_status { get; set;} =null;

        public List<GridGeoLocation> gridGeoLocation { get; set;}
        public List<Grid_Docs> gridDocuments {get;set;}
        public List<layerDtls> lyrDtls {get;set;}
        public string createdAt {get;set;}
        public string updatedAt {get;set;}
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

        [EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI Status value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus CG_RFI_status { get; set;}
        public string layerSubContractor { get; set; }   
        public IFormFile[] uploadDocs { get; set; }  
        public string[] remove_docs_filename {get;set;} 

    
}

public class Grid_Docs {
    
    public int Id {get;set;} 
    public string fileName {get;set;}
    public string fileType {get;set;}
    public string uploadType {get;set;} 
    public string filepath {get;set;}    
}
}
