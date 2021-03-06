using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GridManagement.common;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace GridManagement.repository

{
    public class SubContractorRepository : ISubContractorRepository {
        private readonly gridManagementContext _context;
        private readonly IMapper _mapper;

        public SubContractorRepository (gridManagementContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public bool InsertNewSubContractor (AddSubContractorModel subContReq) {
            try {
                if (_context.Subcontractors.Where (x => x.Code == subContReq.code && x.IsDelete == false).Count () > 0) throw new ValueNotFoundException ("SubContractorId already exists");
                Subcontractors subCont = _mapper.Map<Subcontractors> (subContReq);
                subCont.CreatedBy = subContReq.user_id;
                subCont.CreatedAt = DateTime.Now;
                _context.Subcontractors.Add (subCont);
                _context.SaveChanges ();
                AuditLogs audit = new AuditLogs() {
                     Action ="SubContractor",
                     Message="Insert Subcontractor Succussfully",
                     CreatedAt = DateTime.Now,
                     CreatedBy = subContReq.user_id
            };
            AudtitLog(audit);
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool UpdateSubContractor (AddSubContractorModel subContReq, int Id) {
            try {
                Subcontractors subCont = _context.Subcontractors.Where (x => x.Id == Id && x.IsDelete == false).FirstOrDefault ();
                if (subCont == null) throw new ValueNotFoundException ("SubContrtactorId doesn't exists");

                if (_context.Subcontractors.Where (x => x.Code == subContReq.code && x.Id != Id && x.IsDelete == false).Count () > 0) throw new ValueNotFoundException ("new value SubContractor Code already exists, give unique value");
                // subCont = _mapper.Map<Subcontractors>(subContReq);
                subCont.Email = subContReq.email;
                subCont.Mobile = subContReq.phone;
                subCont.Name = subContReq.name;
                subCont.Code = subContReq.code;
                subCont.ContactName = subContReq.contact_person;
                subCont.Address = subContReq.contact_address;
                subCont.UpdatedBy = subContReq.user_id;
                subCont.UpdateAt = DateTime.Now;
                //    _context.Entry(subCont).State = EntityState.Modified;            
                _context.SaveChanges ();
                AuditLogs audit = new AuditLogs() {
                     Action ="SubContractor",
                     Message="Update Subcontractor Succussfully",
                     CreatedAt = DateTime.Now,
                     CreatedBy = subContReq.user_id
            };
            AudtitLog(audit);
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool DeleteSubContractor (int Id) {
            try {
                Subcontractors subCon = _context.Subcontractors.Where (x => x.Id == Id).FirstOrDefault ();
                if (subCon == null) throw new ValueNotFoundException ("SubContrtactorId doesn't exists");
                subCon.IsDelete = true;
                _context.Update (subCon);
                _context.SaveChanges ();
                    AuditLogs audit = new AuditLogs() {
                     Action ="SubContractor",
                     Message="Delete Subcontractor Succussfully",
                     CreatedAt = DateTime.Now,  
                     CreatedBy = null               
            };
              AudtitLog(audit);
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<SubContractorDetails> GetSubContractorsList (int? SubId) {
            try {
                var res = _context.Subcontractors
                    .Include (c => c.CreatedByNavigation)
                    .ToList ().Where (x => x.IsDelete == false);
                List<SubContractorDetails> lstSubContDetails = _mapper.Map<List<SubContractorDetails>> (res);
                if (!string.IsNullOrEmpty (SubId.ToString ())) lstSubContDetails = lstSubContDetails.Where (x => x.SubContractorId == SubId.ToString ()).ToList ();

                return lstSubContDetails;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<SubContractorName> GetSubContNoList () {
            try {
                List<SubContractorName> lstGridDetails = _mapper.Map<List<SubContractorName>> (_context.Subcontractors.ToList ().Where (x => x.IsDelete == false));
                return lstGridDetails;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<SubContractorReport> SubContractorReports (FilterReport filterReport) {
            try {

                if (!string.IsNullOrEmpty (filterReport.startDate.ToString ()) && !string.IsNullOrEmpty (filterReport.startDate.ToString ())) {
                    if (filterReport.startDate >= filterReport.endDate) throw new ValueNotFoundException ("start date should not be greater than end date");
                }
                List<SubContractorReport> ltssubContrdata = new List<SubContractorReport> ();
                if (filterReport.startDate != null && filterReport.endDate != null) {

                    ltssubContrdata = _context.Subcontractors.Where (x => x.IsDelete == false && x.CreatedAt >= filterReport.startDate && x.CreatedAt < filterReport.endDate.Value.AddDays (1)).ToList ()
                        .Select (y => new SubContractorReport {
                            code = y.Code, name = y.Name, subContractorId = y.Id.ToString (), quantity = _context.LayerSubcontractors.Include (c => c.Layerdetails).Where (z => z.SubcontractorId == y.Id && z.Layerdetails.Grid.IsDelete == false).Select (x => x.Quantity).Sum ().Value,
                               // materialDesc = String.Join (',', _context.LayerSubcontractors.Include (c => c.Layerdetails).Where (w => w.SubcontractorId == y.Id).ToList ().Select (r => r.Layerdetails.FillingMaterial).ToArray ().Distinct ()).ToString (),
                                createdAt = y.CreatedAt != null ? y.CreatedAt.Value.ToString ("yyyy-MM-dd") : ""

                        }).ToList ();

                } else {

                    ltssubContrdata = _context.Subcontractors.Where (x => x.IsDelete == false).ToList ()
                        .Select (y => new SubContractorReport {
                            code = y.Code, name = y.Name, subContractorId = y.Id.ToString (), quantity = _context.LayerSubcontractors.Include (c => c.Layerdetails).Where (z => z.SubcontractorId == y.Id && z.Layerdetails.Grid.IsDelete == false).Select (x => x.Quantity).Sum ().Value,
                              // materialDesc = String.Join (',', _context.LayerSubcontractors.Include (c => c.Layerdetails).Where (w => w.SubcontractorId == y.Id).ToList ().Select (r => r.Layerdetails.FillingMaterial).ToArray ().Distinct ()).ToString (),
                                createdAt = y.CreatedAt != null ? y.CreatedAt.Value.ToString ("yyyy-MM-dd") : ""

                        }).ToList ();

                }

                return ltssubContrdata;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void AudtitLog(AuditLogs audit) {
            _context.AuditLogs.Add(audit);
            _context.SaveChanges();
        }

    }
}