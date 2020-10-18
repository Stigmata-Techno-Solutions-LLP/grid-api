using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GridManagement.common;
using Microsoft.AspNetCore.Http;  


namespace GridManagement.Model.Dto
{
    public class AddLayer
    {
        [Required]
        public int? gridId { get; set; }


        [Required]
        public int? layerId { get; set; }   

        [Display(Name = "Filling Date")]
        public DateTime? fillingDate { get; set; } =null;


        [Display(Name = "Filling Material")]
        public string fillingMaterial { get; set; }


        [Display(Name = "Area of layer")]
        public decimal? area_layer { get; set; }


        [Required]
        [Display(Name = "Total Quantity")]
        public int totalQuantity { get; set; }


        [Display(Name = "Fill Type")]
        public string fillType { get; set; }

        [Display(Name = "Top Level Fill material")]
        public string topFillMaterial { get; set; }

        
        [Display(Name = "Remarks of layer")]
        public string remarks { get; set; }


        [Display(Name = "CLient Name")]
        public int? ClientLayerId { get; set; } = null;

        [Display(Name = "User Name")]
        public int? UserLayerId { get; set; } = null;

        [Required]
        [Display(Name = "User Id")]
        public int? user_id { get; set; }


        [Display(Name = "Compact testing RFI No")]
        public string CT_RFIno { get; set; }

        [Display(Name = "Comapct testing RFI Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime? CT_inspection_date { get; set; } = null;


        [Display(Name = "Compact testing RFI Approval Date")]
        [DataType(DataType.Date)]
        public DateTime? CT_approval_date { get; set; } = null;


        [Display(Name = "Comapct tesing RFI Status")]
        //[EnumDataType(typeof(commonEnum.CT_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CT_RFIStatus? CT_RFI_status { get; set;}= null;


        [Display(Name = "Level Verification  RFI No")]
        public string LV_RFIno { get; set; }

        [Display(Name = "Level Verification RFI Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime? LV_inspection_date { get; set; } = null;


        [Display(Name = "Level Verification RFI Approval Date")]
        [DataType(DataType.Date)]
        public DateTime? LV_approval_date { get; set; } = null;


        [Display(Name = "Level Verification RFI Status")]
       // [EnumDataType(typeof(commonEnum.LV_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.LV_RFIStatus? LV_RFI_status { get; set;} = null;

        [Required]
         [Display(Name = "Layer Status")]
        public commonEnum.LayerStatus status { get; set;}

public string layerSubContractor1 {get;set; }
        public List<LayerSubcontractor> layerSubContractor { get; set; }  


        public IFormFile[] uploadDocs { get; set; }  

        public string[] remove_docs_filename {get;set;} 
              // public List<LayerDocuments> layeDocument { get; set; }

    }   


public class Layer_Docs {
    
    public int Id {get;set;} 
    public string fileName {get;set;}
    public string fileType {get;set;}
    public string uploadType {get;set;} 
    public string filepath {get;set;}    
}

    public class LayerSubcontractor
    {

        [Required]
        public int subContractorId { get; set; }

        [Required]
        public int quantity { get; set; }
        public string subContractorName {get;set;}
    }

    public class LayerSubcontractor1
    {

        [Required]
        public int subContractorId { get; set; }

        [Required]
        public int quantity { get; set; }
    }

    

    public class LayerNo {
        public int Id{get;set;}
        public string layerName{get;set;}
    }


public class layerNoFilter{
        [Required]
    
        [Display(Name = "Grid Id")]
        public int? gridId { get; set; }= null;
}
    public class layerFilter {
         public int? layerDtlsId { get; set; } = null;
    
        [Display(Name = "Grid No")]
        public string gridNo { get; set; } = null;

        [Display(Name = "Layer No")]
        public string layerNo { get; set; }

 [Display(Name = "Grid Id")]
        public int? gridId { get; set; } = null;

        [Display(Name = "Layer Id")]
        public int? layerId { get; set; } = null;

        [Display(Name = "Compact testing RFI No")]
        public string CT_RFIno { get; set; }

        // [Display(Name = "Comapct tesing RFI Status")]
        // [EnumDataType(typeof(commonEnum.CT_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus? CT_RFI_status { get; set;}=null;


        [Display(Name = "Level Verification  RFI No")]
        public string LV_RFIno { get; set; }

        public commonEnum.CG_RFIStatus? LV_RFI_status { get; set;} = null;

        [Display(Name = "Client Billing Generated Status")]
        public bool? isBillGenerated {get;set;} = null;

        [Display(Name = "Layer Approved Status")]
        public bool? isApproved {get;set;} = null;

        [Display(Name = "Sub-Contractor Id")]
        public int? subContractorId { get;set; } =null;


        [Display(Name = "Layer Status")]
        public string layerStatus { get; set;}

    }

    public class layerDtls {
   
        public int? layerDtlsId { get; set; } = null;

        public string layerNo { get; set; }
        public string gridNo {get;set;}
        public int gridId { get; set; }

        public int layerId { get; set; }

        public string fillingDate { get; set; }

        public string fillingMaterial { get; set; }

        public decimal area_layer { get; set; }

        public int totalQuantity { get; set; }

        public string fillType { get; set; }


        public string topFillMaterial { get; set; }

        public string remarks { get; set; }

        public int user_id { get; set; }

        public string CT_RFIno { get; set; }

        public string CT_inspection_date { get; set; }
        public string CT_approval_date { get; set; }
        public string CT_RFI_status { get; set;}
        public string LV_RFIno { get; set; }
        public string LV_inspection_date { get; set; }
        public string LV_approval_date { get; set; }
        public  string status { get; set;}       
        public string LV_RFI_status { get; set;}
        public bool IsBillGenerated {get;set;}
        public bool isApproved {get;set;}
        public int? ClientLayerId { get; set; }
        public int? UserLayerId { get; set; }
        public DateTime createdAt {get;set;}
        public DateTime updatedAt {get;set;}
        public string createdDate {get;set;}
        public string updatedDate {get;set;}

        public ICollection<LayerSubcontractor> layerSubContractor { get; set; }  
        public List<Layer_Docs> layerDocs {get;set;}
       // public List<LayerDocuments> layeDocument { get; set; }

    }



public class UploadLayerImages {
    public int layerDtlsId {get;set;}
    
    public string uploadDocs { get; set; }  
    public string[] remove_docs_filename {get;set;} 
    public string fileName{get;set;}
}
public class LayerNoFilterSkip{
    public int? gridId {get;set;} = null;
    public bool?  isBilled {get;set;} = null;
    public bool?  isApproved {get;set;} = null;
}

   public class UserLayer {
        public int Id{get;set;}
        public string Name{get;set;}
    }

       public class ClientLayer {
        public int Id{get;set;}
        public string Name{get;set;}
    }
}
