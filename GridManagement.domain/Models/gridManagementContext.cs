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

        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<ClientBilling> ClientBilling { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<GridGeolocations> GridGeolocations { get; set; }
        public virtual DbSet<Grids> Grids { get; set; }
        public virtual DbSet<LayerDetails> LayerDetails { get; set; }
        public virtual DbSet<LayerDocuments> LayerDocuments { get; set; }
        public virtual DbSet<LayerSubcontractors> LayerSubcontractors { get; set; }
        public virtual DbSet<Layers> Layers { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SubcontractorUsers> SubcontractorUsers { get; set; }
        public virtual DbSet<Subcontractors> Subcontractors { get; set; }
        public virtual DbSet<Userroles> Userroles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=landt.ctxkj3vcelr3.ap-southeast-1.rds.amazonaws.com;Database=gridManagement;User Id=admin;Password=PlH34cwug3tqupePJcAp;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.GridLayerDetails).HasColumnName("grid_layer_details");

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

            modelBuilder.Entity<GridGeolocations>(entity =>
            {
                entity.ToTable("grid_geolocations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GridId).HasColumnName("grid_id");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(10, 6)");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.GridGeolocations)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("geoloccation__gridId__fkey");
            });

            modelBuilder.Entity<Grids>(entity =>
            {
                entity.ToTable("grids");

                entity.HasIndex(e => e.Gridno)
                    .HasName("UQ__grids__4FA6C35D754EE88E")
                    .IsUnique();

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

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.LayerdetailsId).HasColumnName("layerdetails_id");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(500)
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
                    .HasName("UQ__layers__91C38FFC9ECC9F50")
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

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SubcontractorUsers>(entity =>
            {
                entity.ToTable("subcontractor_users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SubcontId).HasColumnName("subcont_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Subcont)
                    .WithMany(p => p.SubcontractorUsers)
                    .HasForeignKey(d => d.SubcontId)
                    .HasConstraintName("subcontuser_subcont_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubcontractorUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("subcontuser_user_id_fkey");
            });

            modelBuilder.Entity<Subcontractors>(entity =>
            {
                entity.ToTable("subcontractors");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ__subcontr__357D4CF98D497620")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(50)
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

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Subcontractors)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("subcont_user_id_fkey");
            });

            modelBuilder.Entity<Userroles>(entity =>
            {
                entity.ToTable("userroles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userroles_role_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userroles_user_id_fkey");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__users__AB6E61641B97A662")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Email)
                    .IsRequired()
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

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
