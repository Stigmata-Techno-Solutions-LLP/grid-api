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
using Microsoft.AspNetCore.Hosting;  
using Microsoft.AspNetCore.Http;  
using System.Collections;
using System.Linq;

namespace GridManagement.service
{

    public class GridService : IGridService
    {
                private readonly IWebHostEnvironment webHostEnvironment;  
            string prefixPath = "Images";

        IGridRepository _gridRepo;       

        public GridService(IGridRepository gridRepo,IWebHostEnvironment hostEnvironment )
        {
            _gridRepo = gridRepo;
            webHostEnvironment = hostEnvironment;  

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
                int layerId =  _gridRepo.InsertNewLayer(model);
            RemoveLayerDocs(model.remove_docs_filename);
            if (model.uploadDocs != null) {

            
             foreach(IFormFile file in model.uploadDocs) {                
                 Layer_Docs layerDoc = new Layer_Docs();
                 layerDoc.fileName = file.FileName;
                 layerDoc.filepath =  UploadedFile(file);             
                 layerDoc.uploadType = "Docs";    
                 layerDoc.fileType = Path.GetExtension(file.FileName);
                 _gridRepo.LayerDocsUpload(layerDoc, layerId);
             }
            }
             return true;                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UploadLayer(UploadLayerImages model)
        {
            try
            {
            RemoveLayerDocs(model.remove_docs_filename);
            if (model.uploadDocs != null) {
             foreach(IFormFile file in model.uploadDocs) {                
                 Layer_Docs layerDoc = new Layer_Docs();
                 layerDoc.fileName = file.FileName;
                 layerDoc.filepath =  UploadedFile(file);                 
                 layerDoc.fileType = Path.GetExtension(file.FileName);
                 layerDoc.uploadType = "Images";
                 _gridRepo.LayerDocsUpload(layerDoc, model.layerDtlsId);
             }
            }
             return true;                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string UploadedFile(IFormFile file)  
        { 
            try {
            string uniqueFileName = null;  
            if (file != null)  
            {  
                string uploadsFolder = Path.Combine(webHostEnvironment.ContentRootPath, prefixPath);  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    file.CopyTo(fileStream);  
                }  
            }  
            return Path.Combine(prefixPath, uniqueFileName);  
            } catch (Exception ex) {
                throw ex;
            }
        }

        private bool RemoveGridDocs(string[] fileslist)  
        {  
            try {            
            if (fileslist == null) return true;
            string uniqueFileName = null;  
            foreach(string filepath in fileslist) {
                this._gridRepo.CleaningGrubEntryRemoveDocs(Path.Combine(prefixPath, filepath ));
            string docsFolder = Path.Combine(webHostEnvironment.ContentRootPath, prefixPath);
            var fileInfo = new System.IO.FileInfo( Path.Combine(docsFolder,filepath));
            fileInfo.Delete();
            }            
            return true; }
            catch (Exception ex) {
                throw ex; 
            } 
        }  


        private bool RemoveLayerDocs(string[] fileslist)  
        {  
            try {            
            if (fileslist == null) return true;
            string uniqueFileName = null;  
            foreach(string filepath in fileslist) {
                this._gridRepo.LayerRemoveDocs(Path.Combine(prefixPath, filepath ));
            string docsFolder = Path.Combine(webHostEnvironment.ContentRootPath, prefixPath);
            var fileInfo = new System.IO.FileInfo( Path.Combine(docsFolder,filepath));
            fileInfo.Delete();
            }            
            return true; }
            catch (Exception ex) {
                throw ex; 
            } 
        }  

        public bool UpdateGrid( AddGrid gridReq, int Id) {
            return _gridRepo.UpdateGrid(gridReq, Id);
        }

         public bool CleaningGrubbingEntry( AddCG_RFI gridReq, int Id) {
             try {
                 RemoveGridDocs(gridReq.remove_docs_filename);
             
             if (gridReq.uploadDocs != null) {                
                 foreach(IFormFile file in gridReq.uploadDocs) {                
                 Grid_Docs gridDoc = new Grid_Docs();
                 gridDoc.fileName = file.FileName;
                 gridDoc.filepath =  UploadedFile(file);                 
                 gridDoc.fileType = Path.GetExtension(file.FileName);
                 _gridRepo.CleaningGrubEntryUploadDocs(gridDoc, Id);
             }
             }
                return _gridRepo.CleaningGrubEntry(gridReq, Id);
             }
             catch(Exception ex) {
                  throw ex;  
             }
        }
        public bool DeleteGrid( int Id) {
            return _gridRepo.DeleteGrid( Id);
        }

         public List<GridDetails> GetGridList(gridFilter filterReq) {

List<GridDetails> grdList = _gridRepo.GetGridList(filterReq);
//grdList.ForEach(x =>x.gridDocuments.ForEach(x=>x.filepath = Convert.ToBase64String( File.ReadAllBytes( Path.Combine(webHostEnvironment.ContentRootPath,x.filepath)))));


            return grdList;
        }

        public List<GridNo> GetGridNoList() {
            return _gridRepo.GetGridNoList();
        }

        public List<LayerNo> GetLayerNoList(LayerNoFilterSkip lyr) {
            return _gridRepo.GetLayerNoList(lyr);
        }

               
         public List<layerDtls> GetLayerList(layerFilter filterReq) {
            return _gridRepo.GetLayerList(filterReq);
        }
         public List<LayerNo> ClientBillingLayersNo(layerNoFilter filterReq) {
            return _gridRepo.clientBillingLayerByGridId(filterReq);
        }

         public bool CreateClientBilling(AddClientBilling billingReq) {
            return _gridRepo.CreateClientBilling(billingReq);
        }

        public GridDetails GetGridDetails(int Id) {
            return _gridRepo.GetGridDetails(Id);
        }

public string GetCompletedLayerCountByGridNo(int gridId) {
    try{
return _gridRepo.GetCompletedLayerCountByGridNo(gridId);
  
    }
    catch (Exception ex) {
        throw ex;
    }
}

    public LayerMonthWiseDashboard LayerMonthDashboard(FilterDashboard filter) {
    try{
        return _gridRepo.LayerMonthDashboard(filter);
    }
    catch (Exception ex) {
        throw ex;
    }
}
    public DashboardSummary dashboardSummary(FilterDashboard filter) {
        return this._gridRepo.dashboardSummary(filter);
    }


public void ApproveLayer(int layerDtlsId) {
 try {
_gridRepo.ApproveLayer(layerDtlsId);
 }
  catch(Exception ex) {
throw ex;
  }
}
     public List<MasterReport> MasterReport(FilterReport filter){
         return _gridRepo.MasterReport(filter);
     }
           public GridProgressMap GetGridProgress() {
               return _gridRepo.GetGridProgress();
           }


   
}


}
