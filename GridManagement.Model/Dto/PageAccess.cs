using System.Collections.Generic;

namespace GridManagement.Model.Dto
{
    public class PageAccess 
    {
        public PageDetails PageDetail {get;set;}
        public int Id {get;set;}
        public int PageDetailId {get;set;}
        public int RoleId {get;set;}
        public bool IsAdd {get;set;}
        public bool IsUpdate {get;set;}
        public bool IsDelete {get;set;}
        public bool IsView {get;set;}
    }

    public class PageDetails
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public bool IsAdd {get;set;}
        public bool IsUpdate {get;set;}
        public bool IsDelete {get;set;}
        public bool IsView {get;set;}
    }

    public class PageAccessDetail
    {
        public List<PageAccess> pageAccessDetails {get;set;}
    }
}