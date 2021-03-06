﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using GridManagement.common;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
namespace GridManagement.repository {

    public class GridRepository : IGridRepository {
        private readonly gridManagementContext _context;
        private readonly IMapper _mapper;

        public GridRepository (gridManagementContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public bool InsertNewGrid (AddGrid gridReq) {
            try {
                if (_context.Grids.Where (x => x.Gridno == gridReq.gridno && x.IsDelete == false).Count () > 0) throw new ValueNotFoundException ("GridNo already exists");
                if (gridReq.gridGeoLocation.Count()<3) throw new ValueNotFoundException ("Latitude & longitide value should be greater than 3");
                if (gridReq.gridGeoLocation.Count()>6) throw new ValueNotFoundException ("Latitude & longitide value should not be greater than 6");
                if (gridReq.marker_latitide ==0 ) throw new ValueNotFoundException ("Please give valid latitude.");
                if (gridReq.marker_longitude ==0 ) throw new ValueNotFoundException ("Please give valid longitude.");

                Grids grid = _mapper.Map<Grids> (gridReq);
                grid.Status = commonEnum.GridStatus.New.ToString ();
                _context.Grids.Add (grid);
                _context.SaveChanges ();
                foreach (GridGeoLocation geoLoc in gridReq.gridGeoLocation) {
                    GridGeolocations geo = new GridGeolocations ();
                    geo.GridId = grid.Id;
                    geo.Latitude = geoLoc.latitude.ToString ();
                    geo.Longitude = geoLoc.longitude.ToString ();
                    _context.GridGeolocations.Add (geo);
                }
                _context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool UpdateGrid (AddGrid gridReq, int Id) {
            try {
                Grids gridDtls = _context.Grids.Where (x => x.Id == Id && x.IsDelete == false).FirstOrDefault ();
                if (gridDtls == null) throw new ValueNotFoundException ("GridId doesn't exists");
                if (_context.Grids.Where (x => x.Gridno == gridReq.gridno && x.Id != Id && x.IsDelete == false).Count () > 0) throw new ValueNotFoundException ("new GridNo value already exists, should be unique");
                gridDtls.GridArea = gridReq.grid_area;
                gridDtls.Gridno = gridReq.gridno;
                gridDtls.MarkerLatitide = gridReq.marker_latitide.ToString ();
                gridDtls.MarkerLongitude = gridReq.marker_longitude.ToString ();
                gridDtls.UpdatedAt = DateTime.Now;
                gridDtls.UpdatedBy = gridReq.user_id;
                List<GridGeolocations> gridLocList = _context.GridGeolocations.Where (x => x.GridId == Id).ToList ();
                _context.RemoveRange (gridLocList);
                _context.SaveChanges ();

                foreach (GridGeoLocation geoLoc in gridReq.gridGeoLocation) {
                    GridGeolocations geo = new GridGeolocations ();
                    geo.GridId = gridDtls.Id;
                    geo.Latitude = geoLoc.latitude.ToString ();
                    geo.Longitude = geoLoc.longitude.ToString ();
                    _context.GridGeolocations.Add (geo);
                }
                _context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool CleaningGrubEntry (AddCG_RFI gridCGReq, int Id) {
            try {
                Grids gridDtls = _context.Grids.Where (x => x.Id == Id).FirstOrDefault ();
                if (gridDtls == null) throw new ValueNotFoundException ("GridId doesn't exists");
                gridDtls.CgRfino = gridCGReq.CG_RFIno;
                gridDtls.CgInspectionDate = gridCGReq.CG_inspection_date;
                gridDtls.CgApprovalDate = gridCGReq.CG_approval_date;
                gridDtls.CgRfiStatus = gridCGReq.CG_RFI_status.ToString ();
                _context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool CleaningGrubEntryUploadDocs (Grid_Docs gridCGReq, int Id) {
            try {
                GridDocuments gridDocs = new GridDocuments ();
                gridDocs.FileName = gridCGReq.fileName;
                gridDocs.FileType = gridCGReq.fileType;
                gridDocs.Path = gridCGReq.filepath;
                gridDocs.UploadType = gridCGReq.uploadType;
                gridDocs.GridId = Id;
                _context.GridDocuments.Add (gridDocs);
                _context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool LayerDocsUpload (Layer_Docs layerReq, int Id) {
            try {
                LayerDocuments lyrDocs = new LayerDocuments ();
                lyrDocs.FileName = layerReq.fileName;
                lyrDocs.FileType = layerReq.fileType;
                lyrDocs.Path = layerReq.filepath;
                lyrDocs.UploadType = layerReq.uploadType;
                lyrDocs.LayerdetailsId = Id;
                _context.LayerDocuments.Add (lyrDocs);
                _context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool LayerRemoveDocs (string filePath) {
            try {
                LayerDocuments gridDocs = _context.LayerDocuments.Where (x => x.Path.Contains (filePath)).FirstOrDefault ();
                _context.LayerDocuments.Remove (gridDocs);
                _context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool CleaningGrubEntryRemoveDocs (string filePath) {
            try {
                GridDocuments gridDocs = _context.GridDocuments.Where (x => x.Path.Contains (filePath)).FirstOrDefault ();
                _context.GridDocuments.Remove(gridDocs);
                _context.SaveChanges();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool DeleteGrid (int Id) {
            try {
                Grids gridDtls = _context.Grids.Where (x => x.Id == Id).FirstOrDefault ();
                if (gridDtls == null) throw new ValueNotFoundException ("GridId doesn't exists");
                gridDtls.IsDelete = true;
                _context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<GridDetails> GetGridList (gridFilter filterReq) {
            try {

                var res = _context.Grids
                    .Include (c => c.GridGeolocations).Include (c => c.GridDocuments).Where (x => x.IsDelete == false).ToList ();

                if (!string.IsNullOrEmpty (filterReq.gridNo)) res = res.Where (x => x.Gridno == filterReq.gridNo).ToList ();
                if (!string.IsNullOrEmpty (filterReq.status)) res = res.Where (x => x.Status == filterReq.status).ToList ();
                if (!string.IsNullOrEmpty (filterReq.CG_RFIno)) res = res.Where (x => x.CgRfino == filterReq.CG_RFIno).ToList ();
                if (!string.IsNullOrEmpty (filterReq.CG_RFI_status.ToString ())) res = res.Where (x => x.CgRfiStatus == filterReq.CG_RFI_status.ToString ()).ToList ();
                if (!string.IsNullOrEmpty (filterReq.gridId.ToString ())) res = res.Where (x => x.Id == filterReq.gridId).ToList ();

                List<GridDetails> lstGridDetails = _mapper.Map<List<GridDetails>> (res);

                return lstGridDetails;
            } catch (Exception ex) {
                throw ex;

            }
        }

        public GridProgressMap GetGridProgress (string layerId) {
            try {

                var res = _context.Grids
                    .Include (c => c.GridGeolocations).Where (x => x.IsDelete == false).ToList ();

                List<GridDetails> lstGridDetails = _mapper.Map<List<GridDetails>> (res);
                                List<GridDetails> lstNewGridDetails = new List<GridDetails>();
                double gLat = lstGridDetails.Sum (x => x.marker_latitide) / lstGridDetails.Count ();
                double gLong = lstGridDetails.Sum (x => x.marker_longitude) / lstGridDetails.Count ();
                GridProgressMap grdMap = new GridProgressMap ();
                int layerCount = _context.Layers.Count ();
                lstGridDetails.ForEach (x => x.isBilled = (x.lyrDtls.Where (x => x.IsBillGenerated == true).Count () == layerCount ? true : false));

                grdMap.lstGridDtls = lstGridDetails;
                grdMap.gLatitide = gLat; 
                grdMap.gLongitude = gLong;
                return grdMap;
            } catch (Exception ex) {
                throw ex;

            }
        }


    public GridProgressMap GetGridProgresswithFilter (string layerId) {
            try {

                var res = _context.Grids.Include(c => c.GridGeolocations).Include(c=>c.LayerDetails).Where(x => x.IsDelete == false).ToList();

                List<GridDetails> lstGridDetails = _mapper.Map<List<GridDetails>> (res);
                                List<GridDetails> lstNewGridDetails = new List<GridDetails>();
                double gLat = lstGridDetails.Sum (x => x.marker_latitide) / lstGridDetails.Count ();
                double gLong = lstGridDetails.Sum (x => x.marker_longitude) / lstGridDetails.Count ();
                GridProgressMap grdMap = new GridProgressMap ();
                int layerCount = _context.Layers.Count();
            //    lstGridDetails.ForEach (x => x.isBilled = (x.lyrDtls.Where (x => x.IsBillGenerated == true).Count () == layerCount ? true : false));
if (!string.IsNullOrEmpty(layerId)) {
    string[] layerIdArr = layerId.Split(',');

    List<LayerDetails> lyrDtls = _context.LayerDetails.Where(x=> layerIdArr.Contains(x.LayerId.ToString())).ToList();
   // int[] gridIdArr = lyrDtls.Select(x=>x.GridId).ToArray();
        // lstGridDetails  = lstGridDetails.Where(x=> Array.IndexOf(gridIdArr,x.gridId) >=0).ToList();
          //  lstGridDetails.ForEach(x=> x.lyrDtls.Where(z=> gridIdArr.Contains(z.layerId.ToString()) == true  ).ToList());

foreach(GridDetails grid in lstGridDetails) {
if (grid.lyrDtls.Where(x=>x.status == commonEnum.LayerStatus.Completed.ToString() && layerIdArr.Contains(x.layerId.ToString())).Count()>0) {
grid.status= "LayerCompleted";
lstNewGridDetails.Add(grid);
}
else {
    lstNewGridDetails.Add(grid);
}
}
} else {
    lstNewGridDetails = lstGridDetails;
}
    lstNewGridDetails.ForEach(x=>x.lyrDtls= null);

                grdMap.lstGridDtls = lstNewGridDetails;
                grdMap.gLatitide = gLat;
                grdMap.gLongitude = gLong;
                return grdMap;
            } catch (Exception ex) {
                throw ex;

            }
        }

               public GridDetails GetGridDetails (int Id) {
            try {

                var res = _context.Grids
                    .Include (c => c.GridGeolocations).Where (x => x.IsDelete == false && x.Id == Id)

                    //  .Include(c =>c.LayerDetails)
                    .FirstOrDefault ();
                if (res == null) throw new ValueNotFoundException ("Grid Id doesn't exists");

                GridDetails lstGridDetails = _mapper.Map<GridDetails> (res);

                lstGridDetails.lyrDtls = _mapper.Map<List<layerDtls>> (_context.LayerDetails.Include (c => c.Layer).Include (c => c.LayerSubcontractors).Where (x => x.GridId == Id).ToList ()); //  _mapper.Map<List<LayerDetails>>(_context.LayerDetails.Where(x=>x.GridId == Id).ToList());
                // LayerSubcontractors layerSubcontractors= new LayerSubcontractors();
                //        lstGridDetails.lyrDtls = lstGridDetails.lyrDtls.Select(x=>x.layerSubContractor.Select(y=>y.subContractorName = _context.Subcontractors.Where(z=>z.Id == Convert.ToInt32(y.subContractorId)).Select(a=>a.Name; return a;)))
                lstGridDetails.lyrDtls.ForEach (x => x.layerSubContractor.ToList ().ForEach (y => y.subContractorName = _context.Subcontractors.Where (z => z.Id == Convert.ToInt32 (y.subContractorId)).FirstOrDefault ().Name));
                //            foreach(layerDtls lyr in lstGridDetails.lyrDtls) {
                //                foreach (LayerSubcontractor subcon in  lyr.layerSubContractor) {
                // subcon.subContractorName = _context.Subcontractors.Where(x=>x.Id == subcon.subContractorId).FirstOrDefault().Name;
                //                }
                //            }
                return lstGridDetails;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<layerDtls> GetLayerList (layerFilter filterReq) {
            try {
                bool isFilterReqEmpty = true;
                // filterReq.layerNo = filterReq.layerNo.Replace("  "," ");
                Layers layer = _context.Layers.Where (x => x.Layerno == filterReq.layerNo).FirstOrDefault ();
                Grids grid = _context.Grids.Where (x => x.Gridno == filterReq.gridNo).FirstOrDefault ();

                var res = _context.LayerDetails
                    .Include (c => c.LayerSubcontractors)
                    .Include (c => c.LayerDocuments)
                    .Include (c => c.Grid)
                    .Include (c => c.Layer).Where (x => x.Grid.IsDelete == false).ToList ();

                if (!string.IsNullOrEmpty (filterReq.gridNo)) res = res.Where (x => x.GridId == (grid == null ? 0 : grid.Id)).ToList ();
                if (!string.IsNullOrEmpty (filterReq.layerNo)) res = res.Where (x => x.LayerId == (layer == null ? 0 : layer.Id)).ToList ();
                if (!string.IsNullOrEmpty (filterReq.gridId.ToString ())) res = res.Where (x => x.GridId == filterReq.gridId).ToList ();
                if (!string.IsNullOrEmpty (filterReq.layerId.ToString ())) res = res.Where (x => x.LayerId == filterReq.layerId).ToList ();
                if (!string.IsNullOrEmpty (filterReq.layerDtlsId.ToString ())) res = res.Where (x => x.Id == filterReq.layerDtlsId).ToList ();
                if (!string.IsNullOrEmpty (filterReq.CT_RFIno)) res = res.Where (x => x.CtRfino == filterReq.CT_RFIno).ToList ();
                if (!string.IsNullOrEmpty (filterReq.CT_RFI_status.ToString ())) res = res.Where (x => x.CtRfiStatus == filterReq.CT_RFI_status.ToString ()).ToList ();
                if (!string.IsNullOrEmpty (filterReq.LV_RFIno)) res = res.Where (x => x.LvRfino == filterReq.LV_RFIno).ToList ();

                if (!string.IsNullOrEmpty (filterReq.LV_RFI_status.ToString ())) res = res.Where (x => x.LvRfiStatus == filterReq.LV_RFI_status.ToString ()).ToList ();
                if (!string.IsNullOrEmpty (filterReq.isBillGenerated.ToString ())) res = res.Where (x => x.IsBillGenerated == filterReq.isBillGenerated).ToList ();
                if (!string.IsNullOrEmpty (filterReq.isApproved.ToString ())) res = res.Where (x => x.IsApproved == filterReq.isApproved).ToList ();

                if (!string.IsNullOrEmpty (filterReq.layerStatus)) res = res.Where (x => x.Status == filterReq.layerStatus).ToList ();

                // res = res.Where(x=> x.Status == filterReq.layerStatus).ToList();                                    
                if (!string.IsNullOrEmpty (filterReq.subContractorId.ToString ())) res = res.Where (x => x.LayerSubcontractors == x.LayerSubcontractors.Where (x => x.SubcontractorId == filterReq.subContractorId)).ToList ();

                List<layerDtls> lstGridDetails = _mapper.Map<List<layerDtls>> (res);
                lstGridDetails.ForEach (x => x.layerSubContractor.ToList ().ForEach (y => y.subContractorName = _context.Subcontractors.Where (z => z.Id == Convert.ToInt32 (y.subContractorId)).FirstOrDefault ().Name));
                return lstGridDetails;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<GridNo> GetGridNoList () {
            try {

                List<GridNo> lstGridDetails = _mapper.Map<List<GridNo>> (_context.Grids.Where (x => x.IsDelete == false).ToList ());

                return lstGridDetails;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public int InsertNewLayer (AddLayer layerReq) {
            try {
                LayerDetails layerDtls = _context.LayerDetails.Where (x => x.GridId == layerReq.gridId && x.LayerId == layerReq.layerId).FirstOrDefault ();
                int layerId;
                //  int roleLevel = Convert.ToInt32(commonEnum.RolesLevel.Level3);
                //  Users userData = _context.Users.Include(c=>c.Role).Where(x=>x.Id==layerReq.user_id && x.Role.Level == roleLevel ).FirstOrDefault();

                if (layerDtls != null) {
                    layerId = layerDtls.Id;
                    layerDtls.AreaLayer = layerReq.area_layer;
                    layerDtls.UpdatedBy = layerReq.user_id;
                    layerDtls.CtApprovalDate = layerReq.CT_inspection_date;
                    layerDtls.CtInspectionDate = layerReq.CT_inspection_date;
                    layerDtls.CtRfino = layerReq.CT_RFIno;
                    layerDtls.CtRfiStatus = layerReq.CT_RFI_status.ToString ();
                    layerDtls.FillingDate = layerReq.fillingDate;
                    layerDtls.FillingMaterial = layerReq.fillingMaterial;
                    layerDtls.FillType = layerReq.fillType;
                    layerDtls.LvApprovalDate = layerReq.LV_approval_date;
                    layerDtls.LvInspectionDate = layerReq.LV_inspection_date;
                    layerDtls.LvRfino = layerReq.LV_RFIno;
                    layerDtls.LvRfiStatus = layerReq.LV_RFI_status.ToString ();
                    layerDtls.Remarks = layerReq.remarks;
                    layerDtls.ToplevelFillmaterial = layerReq.topFillMaterial;
                    layerDtls.TotalQuantity = layerReq.totalQuantity;
                    layerDtls.Status = layerReq.status.ToString ();
                    layerDtls.UpdatedAt = DateTime.Now;
                    layerDtls.UpdatedBy = layerReq.user_id;
                    layerDtls.ClientLayerId = layerReq.ClientLayerId;
                    layerDtls.UserLayerId= layerReq.UserLayerId;
                    // if (userData != null) {
                    //     layerDtls.IsApproved = true;                   
                    // }       
                    if (layerDtls.LvRfiStatus == commonEnum.LV_RFIStatus.Yes.ToString () && layerDtls.CtRfiStatus == commonEnum.CT_RFIStatus.Yes.ToString ()) {
                        if (layerDtls.Status != commonEnum.LayerStatus.Completed.ToString ()) {
                            throw new ValueNotFoundException ("Layer status should be Completed only , if both RFI status is Yes");
                        }
                    }
                } else {
                    LayerDetails layer = _mapper.Map<LayerDetails> (layerReq);
                    layer.CreatedAt = DateTime.Now;
                    layer.CreatedBy = layerReq.user_id;
                    //  if (userData != null) {
                    //     layer.IsApproved = true;                   
                    // }
                    layer.IsApproved = false;
                    layer.IsBillGenerated = false;
                    _context.LayerDetails.Add (layer);
                    Grids grid = _context.Grids.Where (x => x.Id == layerReq.gridId).FirstOrDefault ();
                    grid.Status = commonEnum.GridStatus.InProgress.ToString();
                    _context.SaveChanges ();

                    layerId = layer.Id;

                }
                List<LayerSubcontractors> lstlayerSub = _context.LayerSubcontractors.Where (x => x.LayerdetailsId == layerId).ToList ();
                _context.RemoveRange (lstlayerSub);
                _context.SaveChanges ();
                if (layerReq.layerSubContractor != null) {

                    foreach (LayerSubcontractor ls in layerReq.layerSubContractor) {
                        LayerSubcontractors layerSub = new LayerSubcontractors ();
                        layerSub.LayerdetailsId = layerId;
                        layerSub.SubcontractorId = ls.subContractorId;
                        layerSub.Quantity = ls.quantity;
                        _context.LayerSubcontractors.Add (layerSub);
                    }
                }
                _context.SaveChanges ();

                if (_context.Layers.Count () == _context.LayerDetails.Where (x => x.GridId == layerReq.gridId).Count ()) {
                    Grids grid = _context.Grids.Where (x => x.Id == layerReq.gridId).FirstOrDefault ();
                    grid.Status = commonEnum.GridStatus.Completed.ToString ();
                    _context.SaveChanges ();
                }
                                AuditLogs audit = new AuditLogs() {
                     Action ="Layer",
                     Message =string.Format( "Layer updated Succussfully.LayerId {0}",layerId),
                     CreatedAt = DateTime.Now,
                     CreatedBy = layerReq.user_id
            };
            AudtitLog(audit);
                return layerId;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void ApproveLayer (int layerDtlsId) {
            try {
                // int roleLevel = Convert.ToInt32(commonEnum.RolesLevel.Level3);
                //    Users userData = _context.Users.Include(c=>c.Role).Where(x=>x.Id==layerReq.user_id && x.Role.Level == roleLevel ).FirstOrDefault();
                LayerDetails lyrDtls = _context.LayerDetails.Where (x => x.Id == layerDtlsId && x.Status == commonEnum.LayerStatus.Completed.ToString () && x.IsApproved == false).FirstOrDefault();
                if (lyrDtls == null) throw new ValueNotFoundException ("LayerDetailsId doesn't exists in below criteria(should be completed & non-approved layers)");
                lyrDtls.IsApproved = true;
                lyrDtls.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                AuditLogs audit = new AuditLogs() {
                     Action ="Layer",
                     Message =string.Format( "Layer Approved Succussfully.LayerId {0}",layerDtlsId),
                     CreatedAt = DateTime.Now,
                     CreatedBy = null
            };
            AudtitLog(audit);
            } catch (Exception ex) {
                throw ex;
            }
        }
        public bool CreateClientBilling (AddClientBilling billingReq) {
            try {                
                if (_context.ClientBilling.Where (x => x.Ipcno == billingReq.IPCNo).Count () > 0) throw new ValueNotFoundException ("IPC No already exists");

                using (var transaction = _context.Database.BeginTransaction ()) {
                    try {
                        ClientBilling clBill = new ClientBilling ();
                        clBill.ClientId = _context.Clients.FirstOrDefault ().Id;
                        clBill.CreatedBy = billingReq.userId;
                        clBill.Ipcno = billingReq.IPCNo;
                        clBill.BillMonth = billingReq.billingMonth;
                        clBill.CreatedAt = DateTime.Now;
                        clBill.CreatedBy = billingReq.userId;
                        _context.ClientBilling.Add (clBill);
                        _context.SaveChanges();

                        foreach (BillingLayerGrid blGrid in billingReq.billingLayerGrid) {
                            ClientBillingLayerDetails clBillingLayerDtls = new ClientBillingLayerDetails ();
                            foreach (int lyId in blGrid.layerDtlsId) {
                                clBillingLayerDtls.ClientBillingId = clBill.Id;
                                clBillingLayerDtls.LayerDetailsId = lyId;
                                _context.ClientBillingLayerDetails.Add (clBillingLayerDtls);

                                LayerDetails layerDtls = _context.LayerDetails.Where (x => x.Id == lyId).FirstOrDefault ();
                                layerDtls.IsBillGenerated = true;
                            }
                        }
                        _context.SaveChanges ();
                    AuditLogs audit = new AuditLogs() {
                     Action ="Layer",
                     Message =string.Format( "Client Billing Created Succussfully.IPC No {0}",billingReq.IPCNo),
                     CreatedAt = DateTime.Now,
                     CreatedBy = billingReq.userId
            };
            AudtitLog(audit);
                        transaction.Commit ();
                    } catch (Exception ex) {
                        transaction.Rollback ();
                        throw ex;
                    }
                }
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<LayerNo> GetLayerNoList (LayerNoFilterSkip layerFltr) {
            try {
                List<LayerNo> lstLayer = new List<LayerNo> ();
  
                if (layerFltr.isApproved != null && layerFltr.isBilled != null &&  !string.IsNullOrEmpty(layerFltr.gridId.ToString())) {
                    int[] skipLyr = _context.LayerDetails.Where (y => (y.IsBillGenerated == layerFltr.isBilled || y.IsApproved == layerFltr.isApproved) && y.GridId == layerFltr.gridId).Select (x => x.LayerId).ToArray ();
                    lstLayer = _mapper.Map<List<LayerNo>> (_context.Layers).ToList ();
                    List<LayerNo> resultLstLayer = new List<LayerNo> ();

                    foreach (LayerNo lyr in lstLayer) {
                        if (skipLyr.Where (x => x == lyr.Id).Count () <= 0) {
                            LayerNo lyrnew = new LayerNo ();
                            lyrnew.Id = lyr.Id;
                            lyrnew.layerName = lyr.layerName;
                            resultLstLayer.Add (lyrnew);
                        }
                    }
                    lstLayer = resultLstLayer;
                } else {
                    lstLayer = _mapper.Map<List<LayerNo>> (_context.Layers.ToList ());
                }

                return lstLayer;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public string GetCompletedLayerCountByGridNo (int gridId) {
            try {
                return _context.LayerDetails.Where (x => x.GridId == gridId && x.Status == commonEnum.LayerStatus.Completed.ToString ()).Count ().ToString ();

            } catch (Exception ex) {
                throw ex;
            }
        }
        public List<LayerNo> clientBillingLayerByGridId (layerNoFilter filterReq) {
            try {
                List<LayerNo> lstLayer = new List<LayerNo> ();

                lstLayer = _mapper.Map<List<LayerNo>> (_context.LayerDetails.Include (x => x.Layer)
                    .Where (x => x.GridId == filterReq.gridId && x.IsBillGenerated == false && x.Status == commonEnum.LayerStatus.Completed.ToString ())
                ).ToList ();
                return lstLayer;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public LayerMonthWiseDashboard LayerMonthDashboard (FilterDashboard filter) {

            try {
                var compltCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.ToList ());
                var billedCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.ToList ());

                if (filter.isMonthly == true) {
                    compltCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.Where (x => x.UpdatedAt >= DateTime.Now.AddMonths (-1)));
                    billedCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.Where (x => x.UpdatedAt >= DateTime.Now.AddMonths (-1)).ToList ());
                } else if (filter.isYearly == true) {
                    compltCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.Where (x => x.UpdatedAt >= DateTime.Now.AddYears (-1)));
                    billedCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.Where (x => x.UpdatedAt >= DateTime.Now.AddYears (-1)).ToList ());

                } else if (filter.startDate != null && filter.endDate != null) {
                    compltCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.Where (x => x.UpdatedAt >= filter.startDate && x.UpdatedAt < filter.endDate.Value.AddDays (1)));
                    billedCountData = _mapper.Map<List<layerDtls>> (_context.LayerDetails.Where (x => x.UpdatedAt >= filter.startDate && x.UpdatedAt < filter.endDate.Value.AddDays (1)).ToList ());

                }
                var compltCountDataGrp = compltCountData.GroupBy (s => s.updatedAt.ToString ("MMM-yyyy"))
                    .Select (g => new {
                        Date = g.Key.ToString (),
                            Completed = g.Where (s => s.status == commonEnum.LayerStatus.Completed.ToString ()).Count ()

                    })
                    .ToList ();

                var billedCountDataGrp = billedCountData.GroupBy (s => s.updatedAt.ToString ("MMM-yyyy"))
                    .Select (g => new {
                        Date = g.Key.ToString (),
                            Billed = g.Where (s => s.IsBillGenerated == true).Count ()
                    })
                    .ToList ();
                LayerMonthWiseDashboard layerMonthlyChart = new LayerMonthWiseDashboard ();
                layerMonthlyChart.Completed = compltCountDataGrp.Select (x => x.Completed).ToArray ();
                layerMonthlyChart.Billed = billedCountDataGrp.Select (x => x.Billed).ToArray ();
                layerMonthlyChart.Date = compltCountDataGrp.Select (x => x.Date).ToArray ();
                return layerMonthlyChart;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public DashboardSummary dashboardSummary (FilterDashboard filterDash) {
            try {
                List<Grids> lstGrid = new List<Grids> ();
                List<LayerDetails> lstLayer = new List<LayerDetails> ();

                lstGrid = _context.Grids.Where (x => x.IsDelete == false).ToList ();
                lstLayer = _context.LayerDetails.Include (c => c.Grid).Where (x => x.Grid.IsDelete == false).ToList ();
                if (filterDash.isMonthly == true) {
                    lstGrid = lstGrid.Where (x => x.CreatedAt.Value >= DateTime.Now.AddMonths (-1)).ToList ();
                    lstLayer = lstLayer.Where (x => x.CreatedAt.Value >= DateTime.Now.AddMonths (-1)).ToList ();
                } else if (filterDash.isYearly == true) {
                    lstGrid = lstGrid.Where (x => x.CreatedAt.Value >= DateTime.Now.AddYears (-1)).ToList ();
                    lstLayer = lstLayer.Where (x => x.CreatedAt.Value >= DateTime.Now.AddYears (-1)).ToList ();
                } else if (filterDash.startDate != null && filterDash.endDate != null) {
                    if (filterDash.startDate >= filterDash.endDate) throw new ValueNotFoundException ("start date should not greater than end date");
                    lstGrid = lstGrid.Where (x => x.CreatedAt.Value >= filterDash.startDate && x.CreatedAt.Value < filterDash.endDate.Value.AddDays (1)).ToList ();
                    lstLayer = lstLayer.Where (x => x.CreatedAt.Value >= filterDash.startDate && x.CreatedAt.Value < filterDash.endDate.Value.AddDays (1)).ToList ();
                }

                DashboardSummary dshSummary = new DashboardSummary ();
                dshSummary.CompletedGrid = lstGrid.Where (x => x.Status == commonEnum.GridStatus.Completed.ToString ()).Count ().ToString ();
                dshSummary.InProgresssGrid = lstGrid.Where (x => x.Status == commonEnum.GridStatus.InProgress.ToString ()).Count ().ToString ();
                dshSummary.NewGrid = lstGrid.Where (x => x.Status == commonEnum.GridStatus.New.ToString ()).Count ().ToString ();
                dshSummary.TotalGrid = lstGrid.Count ().ToString ();

                int totalLayerCount = lstGrid.Count () * _context.Layers.Count ();
                int inProgLayerCount = lstLayer.Where (x => x.Status == commonEnum.LayerStatus.InProgress.ToString ()).Count ();
                int compLayerCount = lstLayer.Where (x => x.Status == commonEnum.LayerStatus.Completed.ToString ()).Count ();
                int newLayerCount = totalLayerCount - (inProgLayerCount + compLayerCount);

                dshSummary.CompletedLayer = compLayerCount.ToString ();
                dshSummary.InProgressLayer = inProgLayerCount.ToString ();
                dshSummary.TotalLayer = totalLayerCount.ToString ();
                dshSummary.NewLayer = newLayerCount.ToString ();
                dshSummary.UnBilledLayer = lstLayer.Where (x => x.IsBillGenerated == false).Count ().ToString ();

                dshSummary.BilledLayer = lstLayer.Where (x => x.IsBillGenerated == true).Count ().ToString ();
                return dshSummary;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<MasterReport> MasterReport (FilterReport filterReport) {
            try {

                if (!string.IsNullOrEmpty (filterReport.startDate.ToString ()) && !string.IsNullOrEmpty (filterReport.startDate.ToString ())) {
                    if (filterReport.startDate >= filterReport.endDate) throw new ValueNotFoundException ("start date should not greater than end date");
                }

                List<MasterReport> masterRpt = new List<MasterReport> ();
                masterRpt = (from grid in _context.Grids join lyr in _context.LayerDetails on grid.Id equals lyr.GridId
                    //join subc in _context.LayerSubcontractors on lyr.Id equals subc.LayerdetailsId
                    join l in _context.Layers on lyr.LayerId equals l.Id where grid.IsDelete == false select new MasterReport {
                        layerDtls = _mapper.Map<layerDtls> (lyr),
                            gridDetails = _mapper.Map<GridDetailsforReport> (grid),
                            layerNo = _mapper.Map<LayerNo> (l),
                            subContractorsCode = String.Join (',', _context.LayerSubcontractors.Where (x => x.LayerdetailsId == lyr.Id).Select (x => x.Subcontractor.Code).ToArray ()),

                    }).ToList ();

                if (!string.IsNullOrEmpty (filterReport.startDate.ToString ())) masterRpt = masterRpt.Where (x => x.layerDtls.createdAt >= filterReport.startDate).ToList ();
                if (!string.IsNullOrEmpty (filterReport.endDate.ToString ())) masterRpt = masterRpt.Where (x => x.layerDtls.createdAt < filterReport.endDate.Value.AddDays (1)).ToList ();

                return masterRpt;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void Dispose () {
            Dispose (true);
            GC.SuppressFinalize (this);
        }

        protected virtual void Dispose (bool disposing) {
            if (disposing) {
                _context.Dispose ();
            }
        }

          public void AudtitLog(AuditLogs audit) {
            _context.AuditLogs.Add(audit);
            _context.SaveChanges();
        }

        public List<UserLayer> GetUserLayerList () {
            try {
                List<UserLayer> lstGridDetails = _mapper.Map<List<UserLayer>> (_context.UserLayerNew.ToList ());
                return lstGridDetails;
            } catch (Exception ex) {
                throw ex;
            }
        }


        public List<ClientLayer> GetClientLayerList () {
            try {
                List<ClientLayer> lstGridDetails = _mapper.Map<List<ClientLayer>> (_context.ClientLayerNew.ToList());
                return lstGridDetails;
            } catch (Exception ex) {
                throw ex;
            }
        }

    }

}