using System;
using GridManagement.Model.Dto;
using System.Collections.Generic;
namespace GridManagement.repository

{
 public interface IGridRepository
 {
        bool InsertNewGrid(AddGrid gridReq);
        int InsertNewLayer(AddLayer layerReq);
         bool UpdateGrid(AddGrid gridReq, int Id);
        bool DeleteGrid (int Id);
        List<GridDetails> GetGridList(gridFilter filterReq);
         bool CleaningGrubEntry(AddCG_RFI gridCGReq, int Id);

        public List<GridNo> GetGridNoList();


        public List<LayerNo> GetLayerNoList();

        public List<layerDtls> GetLayerList(layerFilter filterReq);
        public bool CreateClientBilling(AddClientBilling billingReq);
        public List<LayerNo> clientBillingLayerByGridId(layerNoFilter filterReq);

        public GridDetails GetGridDetails(int Id);
        public string GetCompletedLayerCountByGridNo(int gridId);

        public void ApproveLayer(int layerId);
       public LayerMonthWiseDashboard LayerMonthDashboard(FilterDashboard filter);
        public DashboardSummary dashboardSummary(FilterDashboard filterDash); 
        public List<MasterReport> MasterReport(FilterReport filter);
        public GridProgressMap GetGridProgress();
        public bool CleaningGrubEntryRemoveDocs(string filePath);

        public bool CleaningGrubEntryUploadDocs(Grid_Docs gridCGReq, int Id);

        public bool LayerDocsUpload(Layer_Docs layerReq, int Id);
                   public bool LayerRemoveDocs(string filePath);



    }
}
