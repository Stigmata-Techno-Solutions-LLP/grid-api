using System.Collections.Generic;
using GridManagement.Model.Dto;

namespace GridManagement.repository
{
    public interface IPageAccessRepository
    {
        List<PageAccess> GetPageAccess();
        List<PageAccess> GetPageAccessBasedOnRoleId(int roleId);
        ResponseMessage UpdatePageAccess(List<PageAccess> pageAccessDetails);

        List<Role> GetRoles();
    }
}
