using System;
using GridManagement.Model.Dto;
using System.Collections.Generic;
namespace GridManagement.repository

{
 public interface IGridRepository
 {
        bool InsertNewGrid(AddGrid gridReq);
        bool InsertNewLayer(AddLayer layerReq);
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
        
    }
}
