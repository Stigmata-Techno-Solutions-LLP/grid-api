
using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GridManagement.common;
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
                    geo.Latitude = geoLoc.latitude;
                    geo.Longitude = geoLoc.longitude;
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
                List<GridGeolocations> gridLocList = _context.GridGeolocations.Where(x=>x.GridId == Id).ToList();
                _context.RemoveRange(gridLocList);
                _context.SaveChanges();


                foreach (GridGeoLocation geoLoc in gridReq.gridGeoLocation)
                {
                    GridGeolocations geo = new GridGeolocations();
                    geo.GridId = gridDtls.Id;
                    geo.Latitude = geoLoc.latitude;
                    geo.Longitude = geoLoc.longitude;
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
                 List<GridGeolocations> gridLocList = _context.GridGeolocations.Where(x=>x.GridId == Id).ToList();
                _context.RemoveRange(gridLocList);
                _context.Remove(gridDtls);               
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
        .Include(c => c.GridGeolocations)
       .ToList().Where(x=> (!string.IsNullOrEmpty(filterReq.gridNo) ? x.Gridno == filterReq.gridNo : 
       !string.IsNullOrEmpty(filterReq.status) ? x.Status == filterReq.status : 
       !string.IsNullOrEmpty(filterReq.CG_RFIno) ? x.CgRfino == filterReq.CG_RFIno:
      // !string.IsNullOrEmpty(filterReq.CG_RFI_status.ToString()) ? x.CgRfiStatus == filterReq.CG_RFI_status.ToString():
       true));
          
          List<GridDetails> lstGridDetails = _mapper.Map<List<GridDetails>>(res);

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
                LayerDetails layer = _mapper.Map<LayerDetails>(layerReq);
                _context.LayerDetails.Add(layer);
                _context.SaveChanges();

                foreach (LayerSubcontractor ls in layerReq.layerSubContractor)
                {
                    LayerSubcontractors layerSub = new LayerSubcontractors();
                    layerSub.LayerdetailsId = layer.Id;
                    layerSub.SubcontractorId = ls.subContractorId;
                    layerSub.Quantity = ls.quantity;
                    _context.LayerSubcontractors.Add(layerSub);
                }
               _context.SaveChanges();
                return true;
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
   

    public class GridDataNotFoundException : Exception
{
     public GridDataNotFoundException()
    {
    }

    public GridDataNotFoundException(string message)
        : base(message)
    {
    }

}
}
