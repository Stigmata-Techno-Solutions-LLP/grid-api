using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using GridManagement.Model.Dto;
using GridManagement.domain.Models;
using System.Collections.Generic;
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

              CreateMap<Grids,GridNo>()                   
                    .ForMember(dest =>
                        dest.gridName,
                        opt => opt.MapFrom(src => src.Gridno))

                    .ForMember(dest =>
                        dest.Id,
                        opt => opt.MapFrom(src => src.Id));   
        

                CreateMap<AddGrid, Grids>()                   
                    .ForMember(dest =>
                        dest.GridArea,
                        opt => opt.MapFrom(src => src.grid_area))

                    .ForMember(dest =>
                        dest.Gridno,
                        opt => opt.MapFrom(src => src.gridno))   
                     .ForMember(dest =>
                        dest.CreatedBy,
                        opt => opt.MapFrom(src => src.user_id))
                    .ForMember(dest =>
                        dest.MarkerLongitude,
                        opt => opt.MapFrom(src => src.marker_longitude.ToString()))
                    .ForMember(dest =>
                        dest.MarkerLatitide,
                        opt => opt.MapFrom(src => src.marker_latitide.ToString()))

                    .ReverseMap();
        
                CreateMap<AddCG_RFI, Grids>()                   
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
                        opt => opt.MapFrom(src => src.CG_approval_date));
        

        CreateMap<GridGeolocations, GridGeoLocation>()
                     .ForMember(dest =>
                        dest.latitude,
                        opt => opt.MapFrom(src => double.Parse( src.Latitude)))
                    .ForMember(dest =>
                        dest.longitude,
                        opt => opt.MapFrom(src => double.Parse(src.Longitude)));

                        
  CreateMap<Grids, GridDetails>()
                     .ForMember(dest =>
                        dest.gridId,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest =>
                        dest.CG_RFIno,
                        opt => opt.MapFrom(src => src.CgRfino))
                    .ForMember(dest =>
                        dest.CG_RFI_status,
                        opt => opt.MapFrom(src => src.CgRfiStatus))
                    .ForMember(dest =>
                        dest.CG_inspection_date,
                        opt => opt.MapFrom(src => src.CgInspectionDate != null ? src.CgInspectionDate.Value.ToString("yyyy-MM-dd"):""))
                    .ForMember(dest =>
                        dest.CG_approval_date,
                        opt => opt.MapFrom(src => src.CgApprovalDate != null ? src.CgApprovalDate.Value.ToString("yyyy-MM-dd"):"" ))
                     .ForMember(dest =>
                        dest.createdAt,
                        opt => opt.MapFrom(src => src.CreatedAt != null ? src.CreatedAt.Value.ToString("yyyy-MM-dd"):"" ))   
                    .ForMember(dest =>
                        dest.grid_area,
                        opt => opt.MapFrom(src => src.GridArea))
                     .ForMember(dest =>
                        dest.createdBy,
                        opt => opt.MapFrom(src => src.CreatedByNavigation.CreatedBy))
                    .ForMember(dest =>
                        dest.updatedBy,
                        opt => opt.MapFrom(src => src.UpdatedByNavigation.UpdatedBy))
                    .ForMember(dest =>
                        dest.gridGeoLocation,
                        opt => opt.MapFrom(src =>  src.GridGeolocations))
                    .ForMember(dest =>
                        dest.lyrDtls,
                        opt => opt.MapFrom(src =>  src.LayerDetails));  
                  

                  CreateMap<Grids, GridDetailsforReport>()
                     .ForMember(dest =>
                        dest.gridId,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest =>
                        dest.CG_RFIno,
                        opt => opt.MapFrom(src => src.CgRfino))
                    .ForMember(dest =>
                        dest.CG_RFI_status,
                        opt => opt.MapFrom(src => src.CgRfiStatus))
                    .ForMember(dest =>
                        dest.CG_inspection_date,
                        opt => opt.MapFrom(src => src.CgInspectionDate != null ? src.CgInspectionDate.Value.ToString("yyyy-MM-dd"):""))
                    .ForMember(dest =>
                        dest.CG_approval_date,
                        opt => opt.MapFrom(src => src.CgApprovalDate != null ? src.CgApprovalDate.Value.ToString("yyyy-MM-dd"):"" ))
                    .ForMember(dest =>
                        dest.grid_area,
                        opt => opt.MapFrom(src => src.GridArea))
                     .ForMember(dest =>
                        dest.createdBy,
                        opt => opt.MapFrom(src => src.CreatedByNavigation.CreatedBy))
                    .ForMember(dest =>
                        dest.updatedBy,
                        opt => opt.MapFrom(src => src.UpdatedByNavigation.UpdatedBy));
                  

                    CreateMap<Layers,LayerNo>()                   
                    .ForMember(dest =>
                        dest.layerName,
                        opt => opt.MapFrom(src => src.Layerno))

                    .ForMember(dest =>
                        dest.Id,
                        opt => opt.MapFrom(src => src.Id));   
        


                    CreateMap<LayerDetails,LayerNo>()                   
                    .ForMember(dest =>
                        dest.layerName,
                        opt => opt.MapFrom(src => src.Layer.Layerno))

                    .ForMember(dest =>
                        dest.Id,
                        opt => opt.MapFrom(src => src.Id));   
        
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
                         opt => opt.MapFrom(src => src.topFillMaterial));

                        //.ForMember(dest =>
                        //   dest.LayerSubcontractors,
                        //   opt => opt.MapFrom(src => src.layerSubContractor))

        CreateMap< LayerSubcontractors,LayerSubcontractor>()            
               .ForMember(dest =>
                dest.quantity,
                opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest =>
                dest.subContractorId,
                opt => opt.MapFrom(src => src.SubcontractorId))
                .ForMember(dest =>
                dest.subContractorName,
                opt => opt.MapFrom(src => src.Subcontractor.Name));

        // CreateMap< dynamic,LayerMonthWiseDashboard>()            
        //        .ForMember(dest =>
        //         dest.Date,
        //         opt => opt.MapFrom(src => src.Date))
        //         .ForMember(dest =>
        //         dest.Completed,
        //         opt => opt.MapFrom(src => src.Completed))
        //         .ForMember(dest =>
        //         dest.Billed,
        //         opt => opt.MapFrom(src => src.Billed));


  CreateMap<LayerDetails, layerDtls>()

                        .ForMember(dest =>
                            dest.layerNo,
                            opt => opt.MapFrom(src => src.Layer.Layerno))
                        .ForMember(dest =>
                            dest.gridNo,
                            opt => opt.MapFrom(src => src.Grid.Gridno))

                        .ForMember(dest =>
                            dest.layerId,
                            opt => opt.MapFrom(src => src.LayerId))
                        .ForMember(dest =>
                            dest.layerDtlsId,
                            opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest =>
                            dest.gridId,
                            opt => opt.MapFrom(src => src.GridId))
                        .ForMember(dest =>
                            dest.area_layer,
                            opt => opt.MapFrom(src => src.AreaLayer))
                        .ForMember(dest =>
                            dest.createdAt,
                            opt => opt.MapFrom(src => src.CreatedAt != null ? src.CreatedAt.Value.ToString("yyyy-MM-dd"):""))
                        .ForMember(dest =>
                            dest.CT_inspection_date,
                            opt => opt.MapFrom(src => src.CtInspectionDate != null ? src.CtInspectionDate.Value.ToString("yyyy-MM-dd"):""))
                         .ForMember(dest =>
                            dest.CT_approval_date,
                            opt => opt.MapFrom(src => src.CtApprovalDate != null ? src.CtApprovalDate.Value.ToString("yyyy-MM-dd"):""))

                        .ForMember(dest =>
                            dest.CT_RFIno,
                            opt => opt.MapFrom(src => src.CtRfino))
                        .ForMember(dest =>
                            dest.CT_RFI_status,
                            opt => opt.MapFrom(src => src.CtRfiStatus))
                        .ForMember(dest =>
                            dest.fillingDate,
                            opt => opt.MapFrom(src =>  src.FillingDate != null ? src.FillingDate.Value.ToString("yyyy-MM-dd"):"")  )
                        .ForMember(dest =>
                            dest.fillingMaterial,
                            opt => opt.MapFrom(src => src.FillingMaterial))
                         .ForMember(dest =>
                            dest.fillType,
                            opt => opt.MapFrom(src => src.FillType))
                        .ForMember(dest =>
                            dest.LV_approval_date,
                            opt => opt.MapFrom(src =>   src.LvApprovalDate != null ? src.LvApprovalDate.Value.ToString("yyyy-MM-dd"):""))
                        .ForMember(dest =>
                            dest.LV_inspection_date ,
                            opt => opt.MapFrom(src =>  src.LvInspectionDate != null ? src.LvInspectionDate.Value.ToString("yyyy-MM-dd"):""))
                        .ForMember(dest =>
                            dest.LV_RFIno ,
                            opt => opt.MapFrom(src => src.LvRfino))
                                               
                        .ForMember(dest =>                                
                            dest.LV_RFI_status,
                            opt => opt.MapFrom(src => src.LvRfiStatus))
                         .ForMember(dest =>
                            dest.remarks,
                            opt => opt.MapFrom(src => src.Remarks))
                         .ForMember(dest =>
                         dest.topFillMaterial,
                         opt => opt.MapFrom(src => src.ToplevelFillmaterial))
                         .ForMember(dest =>
                         dest.layerSubContractor,
                         opt => opt.MapFrom(src => src.LayerSubcontractors))
                         .ForMember(dest =>
                         dest.layerNo,
                         opt => opt.MapFrom(src => src.Layer.Layerno));




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
                .ForMember(dest =>
                   dest.CreatedBy,
                   opt => opt.MapFrom(src => src.createdBy))
                .ForMember(dest =>
                   dest.UpdatedBy,
                   opt => opt.MapFrom(src => src.updatedBy))
               .ReverseMap();

            CreateMap<AddSubContractorModel, Subcontractors>()
            .ForMember(dest =>
                dest.Code,
                opt => opt.MapFrom(src => src.code))
               .ForMember(dest =>
                dest.ContactName,
                opt => opt.MapFrom(src => src.contact_person))
                .ForMember(dest =>
                dest.Address,
                opt => opt.MapFrom(src => src.contact_address))
                
                .ForMember(dest =>
                dest.Email,
                opt => opt.MapFrom(src => src.email))
                .ForMember(dest =>
                dest.Name,
                opt => opt.MapFrom(src => src.name))
               .ForMember(dest =>
                dest.Mobile,
                opt => opt.MapFrom(src => src.phone));

            CreateMap<Subcontractors, SubContractorDetails>()
           .ForMember(dest =>
            dest.SubContractorId,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(dest =>
            dest.code,
            opt => opt.MapFrom(src => src.Code))

             .ForMember(dest =>
            dest.code,
            opt => opt.MapFrom(src => src.Code))
           .ForMember(dest =>
            dest.contact_person,
            opt => opt.MapFrom(src => src.ContactName))
            .ForMember(dest =>
            dest.contact_address,
            opt => opt.MapFrom(src => src.Address))
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
            opt => opt.MapFrom(src => src.CreatedAt != null ? src.CreatedAt.Value.ToString("yyyy-MM-dd"):""));


 CreateMap<Subcontractors ,SubContractorName>()                   
                    .ForMember(dest =>
                        dest.SubContName,
                        opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest =>
                        dest.SubContCode,
                        opt => opt.MapFrom(src => src.Code))
                    .ForMember(dest =>
                        dest.Id,
                        opt => opt.MapFrom(src => src.Id));   
        
            CreateMap<PageAccess, RolesApplicationforms>()
               .ForMember(dest =>
                   dest.Id,
                   opt => opt.MapFrom(src => src.Id))
               .ForMember(dest =>
                   dest.FormId,
                   opt => opt.MapFrom(src => src.PageDetailId))
               .ForMember(dest =>
                   dest.RoleId,
                   opt => opt.MapFrom(src => src.RoleId))
               .ForMember(dest =>
                   dest.IsAdd,
                   opt => opt.MapFrom(src => src.IsAdd))
               .ForMember(dest =>
                   dest.IsUpdate,
                   opt => opt.MapFrom(src => src.IsUpdate))
               .ForMember(dest =>
                   dest.IsDelete,
                   opt => opt.MapFrom(src => src.IsDelete))
               .ForMember(dest =>
                   dest.IsView,
                   opt => opt.MapFrom(src => src.IsView))
               .ReverseMap();

                CreateMap<PageDetails, ApplicationForms>()
               .ForMember(dest =>
                   dest.Id,
                   opt => opt.MapFrom(src => src.Id))
               .ForMember(dest =>
                   dest.Name,
                   opt => opt.MapFrom(src => src.Name))
               .ForMember(dest =>
                   dest.Description,
                   opt => opt.MapFrom(src => src.Description))
               .ForMember(dest =>
                   dest.IsAdd,
                   opt => opt.MapFrom(src => src.IsAdd))
               .ForMember(dest =>
                   dest.IsUpdate,
                   opt => opt.MapFrom(src => src.IsUpdate))
               .ForMember(dest =>
                   dest.IsDelete,
                   opt => opt.MapFrom(src => src.IsDelete))
               .ForMember(dest =>
                   dest.IsView,
                   opt => opt.MapFrom(src => src.IsView))
               .ReverseMap();

                CreateMap<Role, Roles>()
               .ForMember(dest =>
                   dest.Id,
                   opt => opt.MapFrom(src => src.Id))
               .ForMember(dest =>
                   dest.Name,
                   opt => opt.MapFrom(src => src.Name))
               .ForMember(dest =>
                   dest.Description,
                   opt => opt.MapFrom(src => src.Description))
               .ForMember(dest =>
                   dest.Level,
                   opt => opt.MapFrom(src => src.Level))
               .ReverseMap();
        }
    }

}
