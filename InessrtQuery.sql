
insert into roles values('Admin','Admin',1,getdate(),null);
insert into roles values('Project Manager','Project Manager',2,getdate(),null);
insert into roles values('Manager','Manager',3,getdate(),null);
insert into roles values('Site Engineer','Site Engineer',4,getdate(),null);


insert into users(username,password,email,is_active,role_id,created_at,updated_by,created_by ,updated_at ) values('admin','admin','admin@gmail.com',1,1,getdate(),null,null,null)

insert into clients values ('kunal', 'MUMBAI CORP','abc@gmail.com','9934w4qwer' )

insert into layers  values ('At +4.67m CD','Layer1')
insert into layers  values ('At +4.74m CD','Layer2')
insert into layers  values ('At +4.92m CD','Layer3')
insert into layers  values ('At +5.10m CD','Layer4')
 insert into layers  values ('At +5.35m CD','Layer5')
 insert into layers  values ('At +5.6m CD','Layer6')
 insert into layers  values ('At +5.85m CD','Layer7')
 insert into layers  values ('At +6.0m CD','Layer8')
 insert into layers  values ('At +6.25m CD','Layer9')
 insert into layers  values ('At +6.5m CD','Layer10')
 insert into layers  values ('At +6.75m CD','Layer11')
 insert into layers  values ('At +7.0m CD','Layer12')
 insert into layers  values ('At +7.25m CD','Layer13')
 insert into layers  values ('At +7.5m CD','Layer14')
 insert into layers  values ('At +7.75m CD','Layer15')
  insert into layers  values ('At +8.0m CD','Layer16')
  


INSERT INTO application_forms VALUES (1,'GridManagement', 0,0,0,0)
INSERT INTO application_forms VALUES (2,'LayerManagement', 0,0,0,0)
INSERT INTO application_forms VALUES (3,'SubContractorManagement', 0,0,0,0)
INSERT INTO application_forms VALUES (4,'UserManagement', 0,0,0,0)
INSERT INTO application_forms VALUES (5,'RoleManagement', 0,0,0,0)
INSERT INTO application_forms VALUES (6,'ClientBilling', 0,0,0,1)
INSERT INTO application_forms VALUES (7,'Report', 0,0,0,1)
INSERT INTO application_forms VALUES (8,'Dashboard', 0,0,0,1)
INSERT INTO application_forms VALUES (9,'CleaningAndGrubbing', 0,0,0,1)
INSERT INTO application_forms VALUES (10,'LayerPhotograph', 0,0,0,1)
INSERT INTO application_forms VALUES (11,'PlanningManager', 1,1,1,1)



INSERT INTO roles_applicationforms VALUES (1,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (2,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (3,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (4,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (5,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (6,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (7,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (8,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (9,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (10,1,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (11,1,1,1,1,1) 

INSERT INTO roles_applicationforms VALUES (1,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (2,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (3,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (4,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (5,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (6,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (7,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (8,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (9,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (10,2,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (11,2,1,1,1,1) 

INSERT INTO roles_applicationforms VALUES (1,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (2,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (3,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (4,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (5,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (6,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (7,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (8,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (9,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (10,3,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (11,3,1,1,1,1) 

INSERT INTO roles_applicationforms VALUES (1,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (2,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (3,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (4,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (5,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (6,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (7,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (8,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (9,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (10,4,1,1,1,1) 
INSERT INTO roles_applicationforms VALUES (11,4,1,1,1,1) 

