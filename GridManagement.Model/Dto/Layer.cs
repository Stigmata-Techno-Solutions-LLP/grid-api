using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GridManagement.common;


namespace GridManagement.Model.Dto
{
    public class AddLayer
    {
        [Required]
        public int gridId { get; set; }


        [Required]
        public int layerId { get; set; }

        [Display(Name = "Filling Date")]
        public DateTime fillingDate { get; set; }


        [Display(Name = "Filling Material")]
        public string fillingMaterial { get; set; }


        [Required]
        [Display(Name = "Area of layer")]
        public decimal area_layer { get; set; }


        [Required]
        [Display(Name = "Total Quantity")]
        public int totalQuantity { get; set; }



        [Display(Name = "Fill Type")]
        public string fillType { get; set; }

        [Display(Name = "Top Level Fill material")]
        public string topFillMaterial { get; set; }

        
        [Display(Name = "Remarks of layer")]
        public string remarks { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int user_id { get; set; }

        public bool isCompleted {get;set;}

        public bool isApproved {get;set;}

        [Display(Name = "Compact testing RFI No")]
        public string CT_RFIno { get; set; }

        [Display(Name = "Comapct testing RFI Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime CT_inspection_date { get; set; }


        [Display(Name = "Compact testing RFI Approval Date")]
        [DataType(DataType.Date)]
        public DateTime CT_approval_date { get; set; }


        [Display(Name = "Comapct tesing RFI Status")]
        [EnumDataType(typeof(commonEnum.CT_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus CT_RFI_status { get; set;}


        [Display(Name = "Level Verification  RFI No")]
        public string LV_RFIno { get; set; }

        [Display(Name = "Level Verification RFI Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime LV_inspection_date { get; set; }


        [Display(Name = "Level Verification RFI Approval Date")]
        [DataType(DataType.Date)]
        public DateTime LV_approval_date { get; set; }


        [Display(Name = "Level Verification RFI Status")]
        [EnumDataType(typeof(commonEnum.LV_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus LV_RFI_status { get; set;}


        public ICollection<LayerSubcontractor> layerSubContractor { get; set; }  
               public List<LayerDocuments> layeDocument { get; set; }

    }   

    public class LayerSubcontractor
    {
        public int layerDtlsId { get; set; }

        [Required]
        public int subContractorId { get; set; }

        [Required]
        public int quantity { get; set; }
    }

    public class LayerDocuments
    {
        public int layerDtlsId { get; set; }

        [Required]
        public int file { get; set; }

        [Required]
        public int quantity { get; set; }
    }

    public class LayerNo {
        public int Id{get;set;}
        public string layerNo{get;set;}
    }

    public class layerFilter {
         public int layerDtlsId { get; set; }

        
        [Display(Name = "Grid Id")]
        public int gridId { get; set; }


        [Display(Name = "Layer Id")]
        public int layerId { get; set; }


        [Display(Name = "Filling Date")]
        public DateTime fillingDate { get; set; }


        [Display(Name = "Compact testing RFI No")]
        public string CT_RFIno { get; set; }

        [Display(Name = "Comapct tesing RFI Status")]
        [EnumDataType(typeof(commonEnum.CT_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus CT_RFI_status { get; set;}


        [Display(Name = "Level Verification  RFI No")]
        public string LV_RFIno { get; set; }

        [Display(Name = "Level Verification RFI Status")]
        [EnumDataType(typeof(commonEnum.LV_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus LV_RFI_status { get; set;}


        [Display(Name = "Client Billing Generated Status")]
        public bool isBillGenerated {get;set;}

        [Display(Name = "Sub-Contractor Id")]
        public int subContractorId { get;set; }

    }

    public class layerDtls {
   
        public int layerDtlsId { get; set; }
        public int gridId { get; set; }

        public int layerId { get; set; }

        public DateTime fillingDate { get; set; }

        public string fillingMaterial { get; set; }


        public decimal area_layer { get; set; }

        public int totalQuantity { get; set; }

        public string fillType { get; set; }


        public string topFillMaterial { get; set; }

        

        public string remarks { get; set; }

        public int user_id { get; set; }

        public string CT_RFIno { get; set; }

        [DataType(DataType.Date)]
        public DateTime CT_inspection_date { get; set; }


        [DataType(DataType.Date)]
        public DateTime CT_approval_date { get; set; }



        [EnumDataType(typeof(commonEnum.CT_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus CT_RFI_status { get; set;}


        public string LV_RFIno { get; set; }

        public DateTime LV_inspection_date { get; set; }


        public DateTime LV_approval_date { get; set; }


        [EnumDataType(typeof(commonEnum.LV_RFIStatus), ErrorMessage = "RFI SStatus value doesn't exist within enum")]
        public commonEnum.CG_RFIStatus LV_RFI_status { get; set;}
 public DateTime createdAt {get;set;}
        public DateTime updatedAt {get;set;}

        public ICollection<LayerSubcontractor> layerSubContractor { get; set; }  
        public List<LayerDocuments> layeDocument { get; set; }

    }   


}
