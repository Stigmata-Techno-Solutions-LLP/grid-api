using System;
using GridManagement.Model.Dto;
namespace GridManagement.repository

{
 public interface IGridRepository
 {
        bool InsertNewGrid(AddGrid gridReq);
        bool InsertNewLayer(AddLayer layerReq);
    }
}
