using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GridManagement.domain.Models
{
    public partial class gridManagementContext : DbContext
    {
   

        public gridManagementContext(DbContextOptions<gridManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationForms> ApplicationForms { get; set; }
        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<ClientBilling> ClientBilling { get; set; }
        public virtual DbSet<ClientBillingLayerDetails> ClientBillingLayerDetails { get; set; }
        public virtual DbSet<ClientLayerNew> ClientLayerNew { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<GridDocuments> GridDocuments { get; set; }
        public virtual DbSet<GridGeolocations> GridGeolocations { get; set; }
        public virtual DbSet<Grids> Grids { get; set; }
        public virtual DbSet<LayerDetails> LayerDetails { get; set; }
        public virtual DbSet<LayerDocuments> LayerDocuments { get; set; }
        public virtual DbSet<LayerSubcontractors> LayerSubcontractors { get; set; }
        public virtual DbSet<Layers> Layers { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesApplicationforms> RolesApplicationforms { get; set; }
        public virtual DbSet<Subcontractors> Subcontractors { get; set; }
        public virtual DbSet<UserLayerNew> UserLayerNew { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationForms>(entity =>
            {
                entity.ToTable("application_forms");

                entity.HasIndex(e => e.Name)
                    .HasName("application_forms_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IsAdd).HasColumnName("isAdd");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.IsUpdate).HasColumnName("isUpdate");

                entity.Property(e => e.IsView).HasColumnName("isView");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuditLogs>(entity =>
            {
                entity.ToTable("audit_logs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("auditlog_createdby_users__fkey");
            });

            modelBuilder.Entity<ClientBilling>(entity =>
            {
                entity.ToTable("client_billing");

                entity.HasIndex(e => e.Ipcno)
                    .HasName("UQ__client_b__650AE6733AE437E2")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillMonth)
                    .HasColumnName("bill_month")
                    .HasColumnType("date");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Ipcno)
                    .IsRequired()
                    .HasColumnName("IPCno")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientBilling)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("client_billing_clients_client_id__fkey");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClientBilling)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("client_billing_createdby_users__fkey");
            });

            modelBuilder.Entity<ClientBillingLayerDetails>(entity =>
            {
                entity.ToTable("client_billing_layerDetails");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientBillingId).HasColumnName("client_billing_id");

                entity.Property(e => e.LayerDetailsId).HasColumnName("layer_details_id");

                entity.HasOne(d => d.ClientBilling)
                    .WithMany(p => p.ClientBillingLayerDetails)
                    .HasForeignKey(d => d.ClientBillingId)
                    .HasConstraintName("client_billinglayerDtls_clientBilling_id__fkey");

                entity.HasOne(d => d.LayerDetails)
                    .WithMany(p => p.ClientBillingLayerDetails)
                    .HasForeignKey(d => d.LayerDetailsId)
                    .HasConstraintName("client_billinglayerDtls_LayerDetks_id__fkey");
            });

            modelBuilder.Entity<ClientLayerNew>(entity =>
            {
                entity.ToTable("client_layer_new");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Psno)
                    .HasColumnName("PSNo")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.ToTable("clients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GridDocuments>(entity =>
            {
                entity.ToTable("grid_documents");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GridId).HasColumnName("grid_id");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UploadType)
                    .HasColumnName("upload_type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.GridDocuments)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("grid_documents_createdby_users__fkey");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.GridDocuments)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grid_documents_gridid_fkey");
            });

            modelBuilder.Entity<GridGeolocations>(entity =>
            {
                entity.ToTable("grid_geolocations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GridId).HasColumnName("grid_id");

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasColumnName("latitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasColumnName("longitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.GridGeolocations)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("geoloccation__gridId__fkey");
            });

            modelBuilder.Entity<Grids>(entity =>
            {
                entity.ToTable("grids");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CgApprovalDate)
                    .HasColumnName("CG_approval_date")
                    .HasColumnType("date");

                entity.Property(e => e.CgInspectionDate)
                    .HasColumnName("CG_inspection_date")
                    .HasColumnType("date");

                entity.Property(e => e.CgRfiStatus)
                    .HasColumnName("CG_RFI_status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CgRfino)
                    .HasColumnName("CG_RFIno")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.GridArea)
                    .HasColumnName("grid_area")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Gridno)
                    .IsRequired()
                    .HasColumnName("gridno")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MarkerLatitide)
                    .HasColumnName("marker_latitide")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MarkerLongitude)
                    .HasColumnName("marker_longitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.GridsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("grids_createdby_users__fkey");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.GridsUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("grids_updatedby_users__fkey");
            });

            modelBuilder.Entity<LayerDetails>(entity =>
            {
                entity.ToTable("layer_details");

                entity.HasIndex(e => new { e.GridId, e.LayerId })
                    .HasName("uq_layerid_gridid")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AreaLayer)
                    .HasColumnName("area_layer")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClientLayerId).HasColumnName("clientLayerId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CtApprovalDate)
                    .HasColumnName("CT_approval_date")
                    .HasColumnType("date");

                entity.Property(e => e.CtInspectionDate)
                    .HasColumnName("CT_inspection_date")
                    .HasColumnType("date");

                entity.Property(e => e.CtRfiStatus)
                    .HasColumnName("CT_RFI_status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CtRfino)
                    .HasColumnName("CT_RFIno")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FillType)
                    .HasColumnName("fill_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FillingDate)
                    .HasColumnName("filling_date")
                    .HasColumnType("date");

                entity.Property(e => e.FillingMaterial)
                    .HasColumnName("filling_material")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.GridId).HasColumnName("grid_id");

                entity.Property(e => e.IsApproved)
                    .HasColumnName("isApproved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsBillGenerated)
                    .HasColumnName("isBillGenerated")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LayerId).HasColumnName("layer_id");

                entity.Property(e => e.LvApprovalDate)
                    .HasColumnName("LV_approval_date")
                    .HasColumnType("date");

                entity.Property(e => e.LvInspectionDate)
                    .HasColumnName("LV_inspection_date")
                    .HasColumnType("date");

                entity.Property(e => e.LvRfiStatus)
                    .HasColumnName("LV_RFI_status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LvRfino)
                    .HasColumnName("LV_RFIno")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ToplevelFillmaterial)
                    .HasColumnName("toplevel_fillmaterial")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TotalQuantity).HasColumnName("total_quantity");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UserLayerId).HasColumnName("userLayerId");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LayerDetailsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("layer_createdby_users__fkey");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.LayerDetails)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("layer__gridId__fkey");

                entity.HasOne(d => d.Layer)
                    .WithMany(p => p.LayerDetails)
                    .HasForeignKey(d => d.LayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("layerDtls__layerId__fkey");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.LayerDetailsUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("layer_updatedby_users__fkey");
            });

            modelBuilder.Entity<LayerDocuments>(entity =>
            {
                entity.ToTable("layer_documents");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LayerdetailsId).HasColumnName("layerdetails_id");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UploadType)
                    .HasColumnName("uploadType")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LayerDocuments)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("layer_documents_createdby_users__fkey");

                entity.HasOne(d => d.Layerdetails)
                    .WithMany(p => p.LayerDocuments)
                    .HasForeignKey(d => d.LayerdetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("layer_documents_layerdetailsid_fkey");
            });

            modelBuilder.Entity<LayerSubcontractors>(entity =>
            {
                entity.ToTable("layer_subcontractors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LayerdetailsId).HasColumnName("layerdetails_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SubcontractorId).HasColumnName("subcontractor_id");

                entity.HasOne(d => d.Layerdetails)
                    .WithMany(p => p.LayerSubcontractors)
                    .HasForeignKey(d => d.LayerdetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("layersubcontractor_layerid_fkey");

                entity.HasOne(d => d.Subcontractor)
                    .WithMany(p => p.LayerSubcontractors)
                    .HasForeignKey(d => d.SubcontractorId)
                    .HasConstraintName("layersubcontractor_subcontractorid_fkey");
            });

            modelBuilder.Entity<Layers>(entity =>
            {
                entity.ToTable("layers");

                entity.HasIndex(e => e.Layerno)
                    .HasName("UQ__layers__91C38FFCAEEB711F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Layerno)
                    .IsRequired()
                    .HasColumnName("layerno")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => e.Name)
                    .HasName("site_roles_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<RolesApplicationforms>(entity =>
            {
                entity.ToTable("roles_applicationforms");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FormId).HasColumnName("form_id");

                entity.Property(e => e.IsAdd).HasColumnName("isAdd");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.IsUpdate).HasColumnName("isUpdate");

                entity.Property(e => e.IsView).HasColumnName("isView");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.RolesApplicationforms)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolesforms_forms_id_fkey");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolesApplicationforms)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolesforms_roles_id_fkey");
            });

            modelBuilder.Entity<Subcontractors>(entity =>
            {
                entity.ToTable("subcontractors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasColumnName("contact_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("update_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SubcontractorsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("subcont_user_id_fkey");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.SubcontractorsUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("subcont_updatedby_users__fkey");
            });

            modelBuilder.Entity<UserLayerNew>(entity =>
            {
                entity.ToTable("user_layer_new");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phoneno)
                    .HasColumnName("phoneno")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("user_roles_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
