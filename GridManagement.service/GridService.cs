using System;
using GridManagement.Model.Dto;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using GridManagement.repository;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using GridManagement.common;
namespace GridManagement.service
{

    public class GridService : IGridService
    {
        IGridRepository _gridRepo;       

        public GridService(IGridRepository gridRepo )
        {
            _gridRepo = gridRepo;
        }

        public  bool AddGrid(AddGrid model)
        {
            try
            {
                return _gridRepo.InsertNewGrid(model) ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool AddLayer(AddLayer model)
        {
            try
            {
                return _gridRepo.InsertNewLayer(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateGrid( AddGrid gridReq, int Id) {
            return _gridRepo.UpdateGrid(gridReq, Id);
        }

         public bool CleaningGrubbingEntry( AddCG_RFI gridReq, int Id) {
            return _gridRepo.CleaningGrubEntry(gridReq, Id);
        }
        public bool DeleteGrid( int Id) {
            return _gridRepo.DeleteGrid( Id);
        }

         public List<GridDetails> GetGridList(gridFilter filterReq) {
            return _gridRepo.GetGridList(filterReq);
        }

        public List<GridNo> GetGridNoList() {
            return _gridRepo.GetGridNoList();
        }

        public List<LayerNo> GetLayerNoList() {
            return _gridRepo.GetLayerNoList();
        }
         public List<layerDtls> GetLayerList(layerFilter filterReq) {
            return _gridRepo.GetLayerList(filterReq);
        }
         public List<LayerNo> ClientBillingLayersNo(layerNoFilter filterReq) {
            return _gridRepo.clientBillingLayerByGridId(filterReq);
        }

         public bool CreateClientBilling(AddClientBilling billingReq) {
            return _gridRepo.CreateClientBilling(billingReq);
        }

        public GridDetails GetGridDetails(int Id) {
            return _gridRepo.GetGridDetails(Id);
        }

public string GetCompletedLayerCountByGridNo(int gridId) {
    try{
return _gridRepo.GetCompletedLayerCountByGridNo(gridId);
  
    }
    catch (Exception ex) {
        throw ex;
    }
}

    public LayerMonthWiseDashboard LayerMonthDashboard(FilterDashboard filter) {
    try{
        return _gridRepo.LayerMonthDashboard(filter);
    }
    catch (Exception ex) {
        throw ex;
    }
}
    public DashboardSummary dashboardSummary(FilterDashboard filter) {
        return this._gridRepo.dashboardSummary(filter);
    }


public void ApproveLayer(int layerDtlsId) {
 try {
_gridRepo.ApproveLayer(layerDtlsId);
 }
  catch(Exception ex) {
throw ex;
  }
}
     public List<MasterReport> MasterReport(FilterReport filter){
         return _gridRepo.MasterReport(filter);
     }
           public GridProgressMap GetGridProgress() {
               return _gridRepo.GetGridProgress();
           }


   
}


}
