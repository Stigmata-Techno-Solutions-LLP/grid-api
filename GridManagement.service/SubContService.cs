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
    public class SubContService: ISubContService
    {
        ISubContractorRepository _subContRepo;       

        public SubContService(ISubContractorRepository subContRepo )
        {
            _subContRepo = subContRepo;
        }

        public bool AddSubCont(AddSubContractorModel subCont) {
            return _subContRepo.InsertNewSubContractor(subCont);
        }

        public bool UpdateSubCont(AddSubContractorModel subCont, int Id) {
            return _subContRepo.UpdateSubContractor(subCont, Id);
        }
        public bool DeleteSubCont( int Id) {
            return _subContRepo.DeleteSubContractor( Id);
        }

         public List<SubContractorDetails> GetSubContList(int? subId) {
            return _subContRepo.GetSubContractorsList(subId);
        }

        public List<SubContractorName> GetSubContNoList() {
            return _subContRepo.GetSubContNoList();
        }

    public List<SubContractorReport> SubContReport(FilterReport filterReq) {
        return this._subContRepo.SubContractorReports(filterReq);
    }
    }
}