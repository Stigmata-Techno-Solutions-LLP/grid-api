using System;
using System.Threading.Tasks;
using GridManagement.Model.Dto;
using System.Collections.Generic;
namespace GridManagement.service
{
   
        public interface IGridService
        {
         bool AddGrid(AddGrid model);

        bool AddLayer(AddLayer model);

        public bool UpdateGrid(AddGrid gridReq, int Id);

        public List<GridDetails> GetGridList(); 
        public bool DeleteGrid( int Id);
        }



}
