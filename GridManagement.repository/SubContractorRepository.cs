
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
                Subcontractors subCont = _context.Subcontractors.Where(x => x.Id == Id).FirstOrDefault();
                if (subCont == null ) return false;
                
             if ( _context.Subcontractors.Where(x => x.Code == subContReq.code && x.Id != Id).Count() >0 ) return false;
                if (subCont == null ) return false;
                
                // subCont = _mapper.Map<Subcontractors>(subContReq);
                subCont.Email = subContReq.email;
                subCont.Mobile = subContReq.phone;
                subCont.Name = subContReq.name;
            subCont.Code = subContReq.code;
            subCont.ContactName = subContReq.contact_person;
            subCont.Address = subContReq.contact_address;
                subCont.UpdatedBy= subContReq.user_id;  
                            //    _context.Entry(subCont).State = EntityState.Modified;            
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
               Subcontractors subCon =  _context.Subcontractors.Where(x=>x.Id == Id).FirstOrDefault(); 

               if (subCon == null) return false;
                _context.Remove(subCon);  
                 _context.SaveChanges();        
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

                var res = _context.Subcontractors
        .Include(c => c.CreatedByNavigation)
        .ToList();
          List<SubContractorDetails> lstSubContDetails = _mapper.Map<List<SubContractorDetails>>(res);

                return lstSubContDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
