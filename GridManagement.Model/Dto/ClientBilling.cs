using System;
using System.Collections.Generic;
namespace GridManagement.Model.Dto
{
    public class AddClientBilling
    {
        public DateTime billingMonth{get;set;}
        public string IPCNo {get;set;}
        public List<BillingLayerGrid> billingLayerGrid {get;set;}
        public int? userId {get;set;}

    }

    public class BillingLayerGrid{
        public int gridId {get;set;}
        public int[] layerDtlsId{get;set;}
    }
}