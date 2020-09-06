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
namespace GridManagement.service
{
    public class PageAccessService : IPageAccessService
    {
        IPageAccessRepository _pageAccessRepository;

        private readonly AppSettings _appSettings;

        public PageAccessService(IOptions<AppSettings> appSettings, IPageAccessRepository pageAccessRepository)
        {
            _pageAccessRepository = pageAccessRepository;
            _appSettings = appSettings.Value;
        }

        public List<PageAccess> GetPageAccess()
        {
            List<PageAccess> pageAccess = _pageAccessRepository.GetPageAccess();
            if (pageAccess == null || pageAccess.Count == 0) return null;
            return pageAccess;
        }
        public List<PageAccess> GetPageAccessBasedonRoleId(int roleId)
        {
            List<PageAccess> pageAccess = _pageAccessRepository.GetPageAccessBasedOnRoleId(roleId);
            if (pageAccess == null || pageAccess.Count == 0) return null;
            return pageAccess;
        }

        public ResponseMessage UpdatePageAccess(List<PageAccess> pageAccessDetails)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage = _pageAccessRepository.UpdatePageAccess(pageAccessDetails);
            return responseMessage;
        }
    }
}
