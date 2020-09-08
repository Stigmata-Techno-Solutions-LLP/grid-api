
using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using GridManagement.common;
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
                if (_context.Subcontractors.Where(x => x.Code == subContReq.code).Count() > 0) throw new ValueNotFoundException("SubContractorId already exists");             
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
                if (subCont == null ) throw new ValueNotFoundException("SubContrtactorId doesn't exists");
                
             if ( _context.Subcontractors.Where(x => x.Code == subContReq.code && x.Id != Id).Count() >0 ) throw new ValueNotFoundException("new value SubContractor Code already exists, give unique value");                           
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
               if (subCon == null) throw new ValueNotFoundException("SubContrtactorId doesn't exists");
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


      public List<SubContractorName> GetSubContNoList()
        {
            try
            {               
          List<SubContractorName> lstGridDetails = _mapper.Map<List<SubContractorName>>(_context.Subcontractors.ToList());
                return lstGridDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
