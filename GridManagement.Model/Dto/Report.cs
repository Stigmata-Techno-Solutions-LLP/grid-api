using System;
using System.ComponentModel.DataAnnotations;
using GridManagement.common;

namespace GridManagement.Model.Dto
{
   public class SubContractorReport
    {             
        public string subContractorId{get;set;}
        public string name {get;set;}           
        public string code {get;set;}

        public int quantity {get;set;}
        public string materialDesc {get;set;}
         

    }

    public class MasterReport {
        public GridDetailsforReport gridDetails {get;set;}
        public layerDtls layerDtls {get;set;}
        public LayerNo layerNo{get;set;}
        public string subContractorsCode {get;set;}
    }

    public class FilterReport {
        public DateTime? startDate {get;set;} = null;
        public DateTime? endDate {get;set;} = null;
    }

        public class FilterDashboard {
        public bool? isTillDate {get;set;} = false;
        public bool? isYearly {get;set;} = false;
        public bool? isMonthly {get;set;} = false;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? startDate {get;set;} = null;
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? endDate {get;set;} = null;
    }


    public class GridDetailsforReport
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

        [EnumDataType(typeof(commonEnum.CG_RFIStatus), ErrorMessage = "RFI Status value doesn't exist within enum")]
        public string CG_RFI_status { get; set;}
        public DateTime createdAt {get;set;}
        public DateTime updatedAt {get;set;}
          public string createdBy {get;set;}
        public string updatedBy {get;set;}

    }
}