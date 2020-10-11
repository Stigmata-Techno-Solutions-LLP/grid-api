using GridManagement.Model.Dto;
using System;
using System.Collections.Generic;
using GridManagement.domain.Models;

namespace GridManagement.service
{
    public interface IPageAccessService
    {
        List<PageAccess> GetPageAccess();
        List<PageAccess> GetPageAccessBasedonRoleId(int roleId);

        ResponseMessage UpdatePageAccess(List<PageAccess> pageAccessDetails);

        List<Role> GetRoles();
       RolesApplicationforms CheckRoleWiseAccess(int PageFormId, int userId);

    }
}