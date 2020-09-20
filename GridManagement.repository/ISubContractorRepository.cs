
using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;


namespace GridManagement.repository
{
     public interface ISubContractorRepository
 {
        bool InsertNewSubContractor(AddSubContractorModel gridReq);
        bool UpdateSubContractor(AddSubContractorModel subContReq, int Id);
        bool DeleteSubContractor (int Id);
        List<SubContractorDetails> GetSubContractorsList(int? subId);

      public List<SubContractorName> GetSubContNoList();
      public List<SubContractorReport> SubContractorReports(FilterReport filterRep); 
    }
}