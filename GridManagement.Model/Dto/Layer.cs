using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GridManagement.Model.Dto
{
    public class AddLayer
    {
        public int layerDtlsId { get; set; }

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

        [Display(Name = "Compact testing RFI No")]
        public string CT_RFIno { get; set; }

        [Display(Name = "Comapct testing RFI Inspection Date")]
        public DateTime CT_inspection_date { get; set; }


        [Display(Name = "Compact testing RFI Approval Date")]
        public DateTime CT_approval_date { get; set; }


        [Display(Name = "Comapct tesing RFI Status")]
        public string CT_RFI_status { get; set; }



        [Display(Name = "Level Verification  RFI No")]
        public string LV_RFIno { get; set; }

        [Display(Name = "Level Verification RFI Inspection Date")]
        public DateTime LV_inspection_date { get; set; }


        [Display(Name = "Level Verification RFI Approval Date")]
        public DateTime LV_approval_date { get; set; }


        [Display(Name = "Level Verification RFI Status")]
        public string LV_RFI_status { get; set; }

        public ICollection<LayerSubcontractor> layerSubContractor { get; set; }
      //  public List<LayerDocuments> layeDocument { get; set; }

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

}
