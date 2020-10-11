using System.Collections.Generic;
using GridManagement.Model.Dto;
using GridManagement.domain.Models;
namespace GridManagement.repository
{
    public interface IPageAccessRepository
    {
        List<PageAccess> GetPageAccess();
        List<PageAccess> GetPageAccessBasedOnRoleId(int roleId);
        ResponseMessage UpdatePageAccess(List<PageAccess> pageAccessDetails);

        List<Role> GetRoles();
         RolesApplicationforms CheckRoleWiseAccess(int PageFormId, int userId);

    }
}
