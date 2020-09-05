using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using GridManagement.Model.Dto;
using GridManagement.domain.Models;

namespace GridManagement.Api.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /// Hero Map
            
            CreateMap<AddUser, Users>()
                    .ForMember(dest =>
                        dest.CreatedBy,
                        opt => opt.MapFrom(src => src.user_id))                   
                    .ReverseMap();
        
                CreateMap<AddGrid, Grids>()
                   
                    .ForMember(dest =>
                        dest.CgRfino,
                        opt => opt.MapFrom(src => src.CG_RFIno))
                    .ForMember(dest =>
                        dest.CgRfiStatus,
                        opt => opt.MapFrom(src => src.CG_RFI_status))
                    .ForMember(dest =>
                        dest.CgInspectionDate,
                        opt => opt.MapFrom(src => src.CG_inspection_date))
                    .ForMember(dest =>
                        dest.CgApprovalDate,
                        opt => opt.MapFrom(src => src.CG_approval_date))
                    .ForMember(dest =>
                        dest.GridArea,
                        opt => opt.MapFrom(src => src.grid_area))
                     .ForMember(dest =>
                        dest.CreatedBy,
                        opt => opt.MapFrom(src => src.user_id))
                    .ReverseMap();
            


        CreateMap<AddLayer, LayerDetails>()
                    
                    .ForMember(dest =>
                        dest.GridId,
                        opt => opt.MapFrom(src => src.gridId))
                    .ForMember(dest =>
                        dest.LayerId,
                        opt => opt.MapFrom(src => src.layerId))
                    .ForMember(dest =>
                        dest.AreaLayer,
                        opt => opt.MapFrom(src => src.area_layer))
                    .ForMember(dest =>
                        dest.CreatedBy,
                        opt => opt.MapFrom(src => src.user_id))
                    .ForMember(dest =>
                        dest.CtApprovalDate,
                        opt => opt.MapFrom(src => src.CT_approval_date))
                     .ForMember(dest =>
                        dest.CtInspectionDate,
                        opt => opt.MapFrom(src => src.CT_inspection_date))

                    .ForMember(dest =>
                        dest.CtRfino,
                        opt => opt.MapFrom(src => src.CT_RFIno))
                    .ForMember(dest =>
                        dest.CtRfiStatus,
                        opt => opt.MapFrom(src => src.CT_RFI_status))
                    .ForMember(dest =>
                        dest.FillingDate,
                        opt => opt.MapFrom(src => src.fillingDate))
                    .ForMember(dest =>
                        dest.FillingMaterial,
                        opt => opt.MapFrom(src => src.fillingMaterial))
                     .ForMember(dest =>
                        dest.FillType,
                        opt => opt.MapFrom(src => src.fillType))


                    .ForMember(dest =>
                        dest.LvApprovalDate,
                        opt => opt.MapFrom(src => src.LV_approval_date))
                    .ForMember(dest =>
                        dest.LvInspectionDate,
                        opt => opt.MapFrom(src => src.LV_inspection_date))
                    .ForMember(dest =>
                        dest.LvRfino,
                        opt => opt.MapFrom(src => src.LV_RFIno))
                    .ForMember(dest =>
                        dest.LvRfiStatus,
                        opt => opt.MapFrom(src => src.LV_RFI_status))
                     .ForMember(dest =>
                        dest.Remarks,
                        opt => opt.MapFrom(src => src.remarks))
                     .ForMember(dest =>
                     dest.ToplevelFillmaterial,
                     opt => opt.MapFrom(src=> src.topFillMaterial))

                     //.ForMember(dest =>
                     //   dest.LayerSubcontractors,
                     //   opt => opt.MapFrom(src => src.layerSubContractor))

                    .ReverseMap();

                    CreateMap< AddSubContractorModel, Subcontractors>()
                    .ForMember(dest =>
                        dest.Code,
                        opt => opt.MapFrom(src => src.code))
                       .ForMember(dest =>
                        dest.ContactName,
                        opt => opt.MapFrom(src => src.contact_person))
                        .ForMember(dest =>
                        dest.Email,
                        opt => opt.MapFrom(src => src.email))
                        .ForMember(dest =>
                        dest.Name,
                        opt => opt.MapFrom(src => src.name))                           
                       .ForMember(dest =>
                        dest.Mobile,
                        opt => opt.MapFrom(src => src.phone));


                        CreateMap<  Subcontractors,SubContractorDetails>()                                              
                       .ForMember(dest =>
                        dest.SubContrtactorId,
                        opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest =>
                        dest.code,
                        opt => opt.MapFrom(src => src.Code))
                       .ForMember(dest =>
                        dest.contact_person,
                        opt => opt.MapFrom(src => src.ContactName))
                        .ForMember(dest =>
                        dest.email,
                        opt => opt.MapFrom(src => src.Email))
                        .ForMember(dest =>
                        dest.name,
                        opt => opt.MapFrom(src => src.Name))                           
                       .ForMember(dest =>
                        dest.phone,
                        opt => opt.MapFrom(src => src.Mobile))
                        .ForMember(dest =>
                        dest.createdBy,
                        opt => opt.MapFrom(src => src.CreatedBy)) 
                        .ForMember(dest =>
                        dest.createdDate,
                        opt => opt.MapFrom(src => src.CreatedAt));                   
        
    }
}
}
