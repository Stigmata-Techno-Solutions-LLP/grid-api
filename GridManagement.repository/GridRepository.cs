
using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GridManagement.common;
using System.Reflection;
namespace GridManagement.repository
{

    public class GridRepository : IGridRepository
    {
        private readonly gridManagementContext _context;
        private readonly IMapper _mapper;

        public GridRepository(gridManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public bool InsertNewGrid(AddGrid gridReq)
        {
            try
            {
                if (_context.Grids.Where(x => x.Gridno == gridReq.gridno).Count() > 0) throw new ValueNotFoundException("GridNo already exists");            
                Grids grid = _mapper.Map<Grids>(gridReq);
               _context.Grids.Add(grid);
                _context.SaveChanges();
                foreach (GridGeoLocation geoLoc in gridReq.gridGeoLocation)
                {
                    GridGeolocations geo = new GridGeolocations();
                    geo.GridId = grid.Id;
                    geo.Latitude = geoLoc.latitude.ToString();
                    geo.Longitude = geoLoc.longitude.ToString();
                    _context.GridGeolocations.Add(geo);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateGrid(AddGrid gridReq, int Id)
        {
            try
            {
                Grids gridDtls = _context.Grids.Where(x => x.Id == Id).FirstOrDefault();
                if (gridDtls == null ) throw new ValueNotFoundException("GridId doesn't exists");
                if ( _context.Grids.Where(x => x.Gridno == gridReq.gridno && x.Id != Id).Count() >0 )  throw new ValueNotFoundException("new GridNo value already exists, should be unique");           
                gridDtls.GridArea = gridReq.grid_area;
                gridDtls.Gridno = gridReq.gridno;
                gridDtls.MarkerLatitide = gridReq.marker_latitide.ToString();
                gridDtls.MarkerLongitude = gridReq.marker_longitude.ToString();
                List<GridGeolocations> gridLocList = _context.GridGeolocations.Where(x=>x.GridId == Id).ToList();
                _context.RemoveRange(gridLocList);
                _context.SaveChanges();


                foreach (GridGeoLocation geoLoc in gridReq.gridGeoLocation)
                {
                    GridGeolocations geo = new GridGeolocations();
                    geo.GridId = gridDtls.Id;
                    geo.Latitude = geoLoc.latitude.ToString();
                    geo.Longitude = geoLoc.longitude.ToString();
                    _context.GridGeolocations.Add(geo);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public bool CleaningGrubEntry(AddCG_RFI gridCGReq, int Id)
        {
            try
            {
                Grids gridDtls = _context.Grids.Where(x => x.Id == Id).FirstOrDefault();
                if (gridDtls == null ) throw new ValueNotFoundException("GridId doesn't exists");               
                gridDtls.CgRfino = gridCGReq.CG_RFIno;
                gridDtls.CgInspectionDate = gridCGReq.CG_inspection_date;
                gridDtls.CgApprovalDate = gridCGReq.CG_approval_date;
                gridDtls.Status = gridCGReq.CG_RFI_status.ToString();
                _context.SaveChanges();              
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteGrid(int Id)
        {
            try
            {
                Grids gridDtls = _context.Grids.Where(x => x.Id == Id).FirstOrDefault();
                if (gridDtls == null ) throw new ValueNotFoundException("GridId doesn't exists");
                gridDtls.IsDelete = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GridDetails> GetGridList(gridFilter filterReq)
        {
            try
            {     

        var res = _context.Grids
        .Include(c => c.GridGeolocations).ToList();        
       
       if (!string.IsNullOrEmpty(filterReq.gridNo)) res = res.Where(x=> x.Gridno == filterReq.gridNo).ToList();
       if (!string.IsNullOrEmpty(filterReq.status)) res = res.Where(x=> x.Status == filterReq.status).ToList();
       if (!string.IsNullOrEmpty(filterReq.CG_RFIno)) res = res.Where(x=> x.CgRfino == filterReq.CG_RFIno).ToList();
       if (!string.IsNullOrEmpty(filterReq.CG_RFI_status.ToString())) res = res.Where(x=> x.CgRfiStatus == filterReq.CG_RFI_status.ToString()).ToList();
       if (!string.IsNullOrEmpty(filterReq.gridId.ToString())) res = res.Where(x=> x.Id == filterReq.gridId).ToList();
       
       
          
          List<GridDetails> lstGridDetails = _mapper.Map<List<GridDetails>>(res);

                return lstGridDetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    

      public GridDetails GetGridDetails(int Id)
        {
            try
            {     

        var res = _context.Grids
        .Include(c => c.GridGeolocations)
        
      //  .Include(c =>c.LayerDetails)
       .FirstOrDefault();
       //.Where(x=> (x.Id == Id));
          
          GridDetails lstGridDetails = _mapper.Map<GridDetails>(res);

lstGridDetails.lyrDtls =  _mapper.Map<List<layerDtls>>(_context.LayerDetails.Include(c => c.Layer).Include(c =>c.LayerSubcontractors).Where(x=>x.GridId==Id).ToList()); //  _mapper.Map<List<LayerDetails>>(_context.LayerDetails.Where(x=>x.GridId == Id).ToList());
            return lstGridDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    




        public List<layerDtls> GetLayerList(layerFilter filterReq)
        {
            try
            {     
bool isFilterReqEmpty = true;


            Layers layer = _context.Layers.Where(x=>x.Layerno == filterReq.layerNo).FirstOrDefault();
            Grids grid = _context.Grids.Where(x=>x.Gridno == filterReq.gridNo).FirstOrDefault();

var res = _context.LayerDetails
        .Include(c => c.LayerSubcontractors)
        .Include(c =>c.LayerDocuments)
        .Include(c => c.Layer).ToList();       
       
         if (!string.IsNullOrEmpty(filterReq.gridNo)) res = res.Where(x=> x.GridId == (grid ==null ? 0 : grid.Id)).ToList();
         if (!string.IsNullOrEmpty(filterReq.layerNo)) res = res.Where(x=> x.LayerId == (layer ==null ? 0 : layer.Id)).ToList();
         if (!string.IsNullOrEmpty(filterReq.layerDtlsId.ToString())) res = res.Where(x=> x.Id == filterReq.layerDtlsId).ToList();
         if (!string.IsNullOrEmpty(filterReq.CT_RFIno)) res = res.Where(x=> x.CtRfino == filterReq.CT_RFIno).ToList();                                    
         if (!string.IsNullOrEmpty(filterReq.CT_RFI_status.ToString())) res = res.Where(x=> x.CtRfiStatus == filterReq.CT_RFI_status.ToString()).ToList();                                    
         if (!string.IsNullOrEmpty(filterReq.LV_RFIno)) res = res.Where(x=> x.LvRfino == filterReq.LV_RFIno).ToList();                                    

         if (!string.IsNullOrEmpty(filterReq.LV_RFI_status.ToString())) res = res.Where(x=> x.LvRfiStatus == filterReq.LV_RFI_status.ToString()).ToList();                                    
         if (!string.IsNullOrEmpty(filterReq.isBillGenerated.ToString())) res = res.Where(x=> x.IsBillGenerated == filterReq.isBillGenerated).ToList();                                    
     
       
         if (!string.IsNullOrEmpty(filterReq.layerStatus)) res = res.Where(x=> x.Status == filterReq.layerStatus).ToList();                                    
         if (!string.IsNullOrEmpty(filterReq.subContractorId.ToString())) res = res.Where(x=>  x.LayerSubcontractors == x.LayerSubcontractors.Where(x=>x.SubcontractorId ==  filterReq.subContractorId)).ToList();                                    
     
 
          List<layerDtls> lstGridDetails = _mapper.Map<List<layerDtls>>(res);

                return lstGridDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

      public List<GridNo> GetGridNoList()
        {
            try
            {     
          
          List<GridNo> lstGridDetails = _mapper.Map<List<GridNo>>(_context.Grids.ToList());

                return lstGridDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    


        public bool InsertNewLayer(AddLayer layerReq)
        {
            try
            {             
                LayerDetails layerDtls = _context.LayerDetails.Where(x=>x.GridId == layerReq.gridId && x.LayerId == layerReq.layerId).FirstOrDefault();
                int layerId;
                if (layerDtls != null) {
                    layerId = layerDtls.Id;
                layerDtls.AreaLayer = layerReq.area_layer;
                layerDtls.UpdatedBy = layerReq.user_id;
                layerDtls.CtApprovalDate = layerReq.CT_inspection_date ;
                layerDtls.CtInspectionDate = layerReq.CT_inspection_date;
                layerDtls.CtRfino = layerReq.CT_RFIno;
                layerDtls.CtRfiStatus = layerReq.CT_RFI_status.ToString();
                layerDtls.FillingDate = layerReq.fillingDate;
                layerDtls.FillingMaterial = layerReq.fillingMaterial;
                layerDtls.FillType = layerReq.fillType;
                layerDtls.LvApprovalDate = layerReq.LV_approval_date;
                layerDtls.LvInspectionDate = layerReq.LV_inspection_date;
                layerDtls.LvRfino = layerReq.LV_RFIno;
                layerDtls.LvRfiStatus = layerReq.LV_RFI_status.ToString();
                layerDtls.Remarks = layerReq.remarks;
                layerDtls.ToplevelFillmaterial = layerReq.topFillMaterial;
                layerDtls.TotalQuantity = layerReq.totalQuantity;   
                 layerDtls.Status = layerReq.status.ToString();                   
                }  
                else {
                LayerDetails layer = _mapper.Map<LayerDetails>(layerReq);
                _context.LayerDetails.Add(layer);
                _context.SaveChanges();
                layerId= layer.Id;
                }
                if (layerReq.layerSubContractor != null) {
                
                foreach (LayerSubcontractor ls in layerReq.layerSubContractor)
                {
                    LayerSubcontractors layerSub = new LayerSubcontractors();
                    layerSub.LayerdetailsId = layerId;
                    layerSub.SubcontractorId = ls.subContractorId;
                    layerSub.Quantity = ls.quantity;
                    _context.LayerSubcontractors.Add(layerSub);
                }
                }
               _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool CreateClientBilling(AddClientBilling billingReq)
        {
            try
            {        

                
if (_context.ClientBilling.Where(x=>x.Ipcno == billingReq.IPCNo).Count()>0 ) throw new ValueNotFoundException("IPC No already exists"); 

using (var transaction = _context.Database.BeginTransaction())
    {
        try
        {

ClientBilling clBill = new ClientBilling();
clBill.ClientId = _context.Clients.FirstOrDefault().Id;
clBill.CreatedBy = billingReq.userId;
clBill.Ipcno = billingReq.IPCNo;
clBill.BillMonth = billingReq.billingMonth;

                _context.ClientBilling.Add(clBill);
                              _context.SaveChanges();

  foreach (BillingLayerGrid  blGrid in billingReq.billingLayerGrid)
                {
                    ClientBillingLayerDetails clBillingLayerDtls = new ClientBillingLayerDetails();
                     foreach(int lyId in blGrid.layerDtlsId) {
                        clBillingLayerDtls.ClientBillingId =clBill.Id;
                       clBillingLayerDtls.LayerDetailsId = lyId;
                    _context.ClientBillingLayerDetails.Add(clBillingLayerDtls);

LayerDetails layerDtls = _context.LayerDetails.Where(x=>x.Id == lyId).FirstOrDefault();
layerDtls.IsBillGenerated = true;
                     }                  
                }
               _context.SaveChanges();
              
            transaction.Commit();
        } catch(Exception ex) {
transaction.Rollback();
throw ex;
        }
    }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public List<LayerNo> GetLayerNoList()
        {
            try
            {        
           List<LayerNo> lstLayer = new List<LayerNo>();
             lstLayer = _mapper.Map<List<LayerNo>>(_context.Layers.ToList());
         
            return lstLayer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public List<LayerNo> clientBillingLayerByGridId(layerNoFilter filterReq)
        {
            try
            {        
           List<LayerNo> lstLayer = new List<LayerNo>();
    
          lstLayer =_mapper.Map<List<LayerNo>>(_context.LayerDetails.Include(x=>x.Layer )
           .Where(x=> x.GridId == filterReq.gridId && x.IsBillGenerated == false && x.Status == commonEnum.LayerStatus.Completed.ToString())
          ).ToList();
                return lstLayer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
   




}
