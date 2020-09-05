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
        List<GridDetails> GetGridList();
    }
}
