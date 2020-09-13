IF OBJECT_ID('gridManagement.dbo.client_billing_layerDetails', 'U') IS NOT NULL 
DROP TABLE gridManagement.dbo.client_billing_layerDetails; 
 
 
IF OBJECT_ID('gridManagement.dbo.client_billing', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.client_billing;
 
 
 IF OBJECT_ID('gridManagement.dbo.layer_subcontractors', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.layer_subcontractors; 

IF OBJECT_ID('gridManagement.dbo.audit_logs', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.audit_logs; 

 IF OBJECT_ID('gridManagement.dbo.layer_documents', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.layer_documents; 

IF OBJECT_ID('gridManagement.dbo.subcontractor_users', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.subcontractor_users; 

IF OBJECT_ID('gridManagement.dbo.subcontractors', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.subcontractors; 
		
 
 IF OBJECT_ID('gridManagement.dbo.clients', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.clients; 


 
IF OBJECT_ID('gridManagement.dbo.layer_details', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.layer_details; 

IF OBJECT_ID('gridManagement.dbo.layers', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.layers; 
 
 
  IF OBJECT_ID('gridManagement.dbo.grid_geolocations', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.grid_geolocations; 

 

IF OBJECT_ID('gridManagement.dbo.grids', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.grids; 

 

IF OBJECT_ID('gridManagement.dbo.roles_applicationforms', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.roles_applicationforms; 
 
IF OBJECT_ID('gridManagement.dbo.application_forms', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.application_forms; 
 


IF OBJECT_ID('gridManagement.dbo.roles', 'U') IS NOT NULL 
  DROP TABLE gridManagement.dbo.roles; 
 


IF OBJECT_ID('gridManagement.dbo.users', 'U') IS NOT NULL 
DROP TABLE gridManagement.dbo.users; 
 



CREATE TABLE gridManagement.dbo.roles (
  id int NOT NULL IDENTITY (1,1),
  "name" varchar(30) NOT NULL,
  "description" varchar(300) NULL, 
  "level" int NULL, 
  updated_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  updated_by int null,

  CONSTRAINT site_roles_name_key UNIQUE (name),
  CONSTRAINT site_roles_pkey PRIMARY KEY (id)
);



CREATE TABLE gridManagement.dbo.application_forms (
  id int NOT NULL IDENTITY (1,1),
  "name" varchar(30) NOT NULL,
  "description" varchar(300) NULL, 
  "isAdd" bit NULL,
  "isUpdate" bit NULL,
  "isDelete" bit NULL,  
  "isView" bit NULL,    
  CONSTRAINT application_forms_name_key UNIQUE (name),
  CONSTRAINT page_pkey PRIMARY KEY (id),  
);


CREATE TABLE gridManagement.dbo.users (
	id int NOT NULL identity(1,1),	
	
	username varchar(100) NOT NULL UNIQUE,
	"password" varchar(500) NOT NULL,
	first_name varchar(100),
	last_name varchar(100),
	phoneno varchar(15),
	email varchar(100) NOT NULL UNIQUE,
	role_id int null,
    is_active bit NULL DEFAULT 0,    
    is_delete bit NULL DEFAULT 0,
  	created_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	updated_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	created_by int NULL,
	updated_by int NULL,
	CONSTRAINT users_pkey PRIMARY KEY (id),
	CONSTRAINT user_roles_id_fkey FOREIGN KEY (role_id) REFERENCES roles(id),
    CONSTRAINT uq_Users_username_email_isdelete UNIQUE(username,email, is_delete)

);


CREATE TABLE gridManagement.dbo.roles_applicationforms (
id int NOT NULL IDENTITY(1,1),
form_id int NOT NULL,
role_id int NOT NULL,
 "isAdd" bit NULL,
  "isUpdate" bit NULL,
  "isDelete" bit NULL,  
  "isView" bit NULL,      
CONSTRAINT roles_forms_pkey PRIMARY KEY (id),
CONSTRAINT rolesforms_forms_id_fkey FOREIGN KEY (form_id) REFERENCES application_forms(id),
CONSTRAINT rolesforms_roles_id_fkey FOREIGN KEY (role_id) REFERENCES roles(id),	
)






CREATE TABLE gridManagement.dbo.subcontractors(
id int not null identity(1,1),
name varchar(200) null,	
code varchar(10) not null,
contact_name varchar(200) null,
email varchar(100) null,
mobile varchar(15) null,

address varchar(2000) null, 
is_delete bit null default 0,
created_at DATETIME default CURRENT_TIMESTAMP,
created_by int null,
update_at DATETIME default CURRENT_TIMESTAMP,
updated_by int null,
CONSTRAINT subcont_pkey PRIMARY KEY (id),
CONSTRAINT subcont_user_id_fkey FOREIGN KEY (created_by) REFERENCES users(id),
CONSTRAINT subcont_updatedby_users__fkey FOREIGN KEY (updated_by) REFERENCES users(id),
CONSTRAINT uq_subCont_code_isdelete UNIQUE(code, is_delete)

)



CREATE TABLE gridManagement.dbo.clients(
id int not null identity(1,1),
name varchar(500) null,
company_name varchar(200) null,
email varchar(100) null,
mobile varchar(15) null,
CONSTRAINT client_pkey PRIMARY KEY (id),
)


CREATE TABLE grids(
    id int not null identity(1,1),
    gridno varchar(10) not null,
    grid_area  decimal(18,2) null,
    status varchar(20),
    is_delete bit NULL DEFAULT 0,
    CG_RFIno varchar(20) null,
    CG_inspection_date date null,
    CG_approval_date date null,
    CG_RFI_status varchar(50),
    marker_latitide varchar(50),
    marker_longitude varchar(50),
    created_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	updated_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	created_by int NULL,
	updated_by int NULL,
	CONSTRAINT grids_pkey PRIMARY KEY (id),
	CONSTRAINT uq_Grids_gridno_isdelete UNIQUE(gridno, is_delete),

	CONSTRAINT grids_createdby_users__fkey FOREIGN KEY (created_by) REFERENCES users(id),
	CONSTRAINT grids_updatedby_users__fkey FOREIGN KEY (updated_by) REFERENCES users(id)
) 



CREATE TABLE grid_geolocations(
    id int not null identity(1,1),
    grid_id int not null,
    latitude varchar(50) not null,
    longitude varchar(50) not null,
    CONSTRAINT gridGeoLocation_pkey PRIMARY KEY (id),
	CONSTRAINT geoloccation__gridId__fkey FOREIGN KEY (grid_id) REFERENCES grids(id)	
)



CREATE TABLE layers(
    id int not null identity(1,1),
    layerno varchar(20) not null unique,
    description VARCHAR(100) null,
	CONSTRAINT layer_pkey PRIMARY KEY (id)
)



CREATE TABLE layer_details(
    id int not null identity(1,1),
    grid_id int not null,
    layer_id int not null,
    filling_date DATE  null,
    filling_material varchar(2000) null,
    area_layer decimal(18,2) null,
    total_quantity int null,
    fill_type varchar(100) null,
    toplevel_fillmaterial varchar(100) null,
    remarks varchar(500),
    status varchar(20),
    CT_RFIno varchar(20) null,
    CT_inspection_date date null,
    CT_approval_date date null,
    CT_RFI_status varchar(50) null,
    LV_RFIno varchar(20) null,
    LV_inspection_date date null,
    LV_approval_date date null,
    LV_RFI_status varchar(50) null,    
    isBillGenerated bit null default 0,    
    created_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	updated_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	created_by int NULL,
	updated_by int NULL,
	CONSTRAINT layerdetails_pkey PRIMARY KEY (id),
    CONSTRAINT layer__gridId__fkey FOREIGN KEY (grid_id) REFERENCES grids(id),
    CONSTRAINT layerDtls__layerId__fkey FOREIGN KEY (layer_id) REFERENCES layers(id),
	CONSTRAINT layer_createdby_users__fkey FOREIGN KEY (created_by) REFERENCES users(id),
	CONSTRAINT layer_updatedby_users__fkey FOREIGN KEY (updated_by) REFERENCES users(id),
	CONSTRAINT uq_layerid_gridid UNIQUE(grid_id, layer_id)

)
	




CREATE TABLE gridManagement.dbo.layer_subcontractors(
    id int not null identity(1,1),
    layerdetails_id int not null,
    subcontractor_id int null,
    quantity int null,
    CONSTRAINT layersubcontractor_pkey PRIMARY KEY (id),
	CONSTRAINT layersubcontractor_layerid_fkey FOREIGN KEY (layerdetails_id) REFERENCES layer_details(id),
	CONSTRAINT layersubcontractor_subcontractorid_fkey FOREIGN KEY (subcontractor_id) REFERENCES subcontractors(id)
)


CREATE TABLE gridManagement.dbo.layer_documents(
    id int not null identity(1,1),
    layerdetails_id int not null,
    "path" varchar(500) null,
    created_by int NULL,
    CONSTRAINT layer_documents_pkey PRIMARY KEY (id),
	CONSTRAINT layer_documents_layerdetailsid_fkey FOREIGN KEY (layerdetails_id) REFERENCES layer_details(id),  
	CONSTRAINT layer_documents_createdby_users__fkey FOREIGN KEY (created_by) REFERENCES users(id)
	
    )


    CREATE TABLE gridManagement.dbo.audit_logs(
        id int not null identity(1,1),
        action varchar(100) null,
        "message" varchar(2000) null,
        created_by int null,
        created_at datetime default CURRENT_TIMESTAMP,
        CONSTRAINT auditlog_pkey PRIMARY KEY (id),
        CONSTRAINT auditlog_createdby_users__fkey FOREIGN KEY (created_by) REFERENCES users(id),
	
    )
       
    
    CREATE TABLE gridManagement.dbo.client_billing(
        id int not null identity(1,1),
        client_id int null,
        IPCno varchar(20) not null UNIQUE,
        bill_month date null,
        created_at DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	created_by int NULL,
	CONSTRAINT client_billing_pkey PRIMARY KEY (id),
    CONSTRAINT client_billing_createdby_users__fkey FOREIGN KEY (created_by) REFERENCES users(id),
   
    CONSTRAINT client_billing_clients_client_id__fkey FOREIGN KEY (client_id) REFERENCES clients(id),
	)
	
	
    
    CREATE TABLE gridManagement.dbo.client_billing_layerDetails(
        id int not null identity(1,1),
        client_billing_id int null,
        layer_details_id int null,
	CONSTRAINT client_billing_layerdetails_pkey PRIMARY KEY (id),
    CONSTRAINT client_billinglayerDtls_clientBilling_id__fkey FOREIGN KEY (client_billing_id) REFERENCES client_billing(id),
	CONSTRAINT client_billinglayerDtls_LayerDetks_id__fkey FOREIGN KEY (layer_details_id) REFERENCES layer_details(id),
	)
	
	
	