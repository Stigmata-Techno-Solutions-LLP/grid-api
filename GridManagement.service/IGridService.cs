using System;
using System.Threading.Tasks;
using GridManagement.Model.Dto;

namespace GridManagement.service
{
   
        public interface IGridService
        {
         bool AddGrid(AddGrid model);

        bool AddLayer(AddLayer model);
        }



}
