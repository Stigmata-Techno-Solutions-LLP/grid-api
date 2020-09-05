
using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace GridManagement.repository

{
  public class SubContractorRepository: ISubContractorRepository
    {
        private readonly gridManagementContext _context;
        private readonly IMapper _mapper;

        public SubContractorRepository(gridManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public bool InsertNewSubContractor(AddSubContractorModel subContReq)
        {
            try
            {
                if (_context.Subcontractors.Where(x => x.Code == subContReq.code).Count() > 0)
                {
                    return false;
                }
                Subcontractors subCont = _mapper.Map<Subcontractors>(subContReq);
                subCont.CreatedBy = subContReq.user_id;
               _context.Subcontractors.Add(subCont);
                _context.SaveChanges();             
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateSubContractor(AddSubContractorModel subContReq, int Id)
        {
            try
            {
                if (_context.Subcontractors.Where(x => x.Id == Id).Count() > 0)
                {
                    return false;
                }
                Subcontractors subCont = _mapper.Map<Subcontractors>(subContReq);
                //will add updateby field
                // subCont. = subContReq.user_id;
               _context.Subcontractors.Update(subCont);
                _context.SaveChanges();             
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public bool DeleteSubContractor(int Id)
        {
            try
            {                     
                _context.Remove(_context.Subcontractors.Where(x=>x.Id == Id));          
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

          public List<SubContractorDetails> GetSubContractorsList()
        {
            try
            {     
                List<Subcontractors> lstSubCont = new List<Subcontractors>();
                lstSubCont  = _context.Subcontractors.ToList();             
                List<SubContractorDetails> lstSubContDetails = _mapper.Map<List<SubContractorDetails>>(lstSubCont);
                return lstSubContDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
