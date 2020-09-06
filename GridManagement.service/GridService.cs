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

    }


}
