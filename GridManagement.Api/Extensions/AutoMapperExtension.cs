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
                         opt => opt.MapFrom(src => src.topFillMaterial))

                        //.ForMember(dest =>
                        //   dest.LayerSubcontractors,
                        //   opt => opt.MapFrom(src => src.layerSubContractor))

                        .ReverseMap();
                 CreateMap<UserDetails, Users>()
                    .ForMember(dest =>
                        dest.FirstName,
                        opt => opt.MapFrom(src => src.firstName))
                    .ForMember(dest =>
                        dest.LastName,
                        opt => opt.MapFrom(src => src.lastName))
                    .ForMember(dest =>
                        dest.Id,
                        opt => opt.MapFrom(src => src.userId))
                    .ForMember(dest =>
                        dest.Email,
                        opt => opt.MapFrom(src => src.email))
                    .ForMember(dest =>
                        dest.Phoneno,
                        opt => opt.MapFrom(src => src.mobileNo))
                    .ForMember(dest =>
                        dest.Username,
                        opt => opt.MapFrom(src => src.userName))
                    .ForMember(dest =>
                        dest.Password,
                        opt => opt.MapFrom(src => src.password))
                    .ForMember(dest =>
                        dest.RoleId,
                        opt => opt.MapFrom(src => src.roleId))
                    .ForMember(dest =>
                        dest.IsActive,
                        opt => opt.MapFrom(src => src.isActive))
                    .ReverseMap();
        }
    }
}
