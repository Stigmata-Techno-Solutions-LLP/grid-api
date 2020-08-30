
using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;

using System.Threading.Tasks;

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
                if (_context.Grids.Where(x => x.Gridno == gridReq.gridno).Count() > 0)
                {
                    return false;
                }
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
}
