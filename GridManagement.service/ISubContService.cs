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
    public interface ISubContService
    {        
        public bool AddSubCont(AddSubContractorModel model);
        public bool UpdateSubCont(AddSubContractorModel subCont, int Id);
        public List<SubContractorDetails> GetSubContList(); 
        public bool DeleteSubCont( int Id);
        public List<SubContractorName> GetSubContNoList();
    }
}