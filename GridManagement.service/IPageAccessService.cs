using GridManagement.Model.Dto;
using System;
using System.Collections.Generic;

namespace GridManagement.service
{
    public interface IPageAccessService
    {
        List<PageAccess> GetPageAccess();
        List<PageAccess> GetPageAccessBasedonRoleId(int roleId);

        ResponseMessage UpdatePageAccess(List<PageAccess> pageAccessDetails);
    }
}