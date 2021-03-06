﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GridManagement.Model.Dto;
namespace GridManagement.service {

        public interface IGridService {
                bool AddGrid (AddGrid model);

                bool AddLayer (AddLayer model);

                public bool UpdateGrid (AddGrid gridReq, int Id);

                public List<GridDetails> GetGridList (gridFilter filterReq);
                public bool DeleteGrid (int Id);
                public bool CleaningGrubbingEntry (AddCG_RFI gridReq, int Id);
                public List<GridNo> GetGridNoList ();

                public List<layerDtls> GetLayerList (layerFilter filterReq);
                public bool CreateClientBilling (AddClientBilling billingReq);
                public List<LayerNo> ClientBillingLayersNo (layerNoFilter layerNoFilter);
                public GridDetails GetGridDetails (int Id);
                public string GetCompletedLayerCountByGridNo (int gridId);
                public void ApproveLayer (int layerDtlsId);
                public LayerMonthWiseDashboard LayerMonthDashboard (FilterDashboard filter);
                public DashboardSummary dashboardSummary (FilterDashboard filter);
                public List<MasterReport> MasterReport (FilterReport filter);
                public GridProgressMap GetGridProgress (string layerId);
                public bool UploadLayer (UploadLayerImages model);
                public List<LayerNo> GetLayerNoList (LayerNoFilterSkip lyr);
                public GridProgressMap GetGridProgresswithFilter (string layerId);
                public List<UserLayer> GetUserLayerList ();
                public List<ClientLayer> GetClientLayerList ();

        }
}