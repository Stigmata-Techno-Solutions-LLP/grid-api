
insert into roles values('Admin','Admin',1,getdate(),null);
insert into roles values('Project Manager','Project Manager',2,getdate(),null);
insert into roles values('Manager','Manager',3,getdate(),null);
insert into roles values('Site Engineer','Site Engineer',4,getdate(),null);


insert into users(username,password,email,is_active,role_id,created_at,updated_by,created_by ,updated_at ) values('amal90','amal90','amal.chinnah@gmail.com',1,1,getdate(),null,null,null)

insert into clients values ('kunal', 'MUMBAI CORP','abc@gmail.com','9934w4qwer' )
insert into layers  values ('Layer3','Layer3')
insert into layers  values ('Layer4','Layer4')
 insert into layers  values ('Layer5','Layer5')
 insert into layers  values ('Layer6','Layer6')
 insert into layers  values ('Layer7','Layer8')
 insert into layers  values ('Layer8','Layer8')
 insert into layers  values ('Layer9','Layer9')
 insert into layers  values ('Layer10','Layer10')
 insert into layers  values ('Layer11','Layer11')

insert into subcontractors(name,code,created_by) values('contractor1','CONTR1',1)


select * from subcontractors s 


select * from users u 

select * from layer_details ld 



select * from subcontractors s2 
select * from clients c 

select * from client_billing cb 

select * from client_billing_layerdetails
select * from grids g2 
-- select * from layers
-- select * from layer_details 
-- select * from layer_subcontractors ls 
-- select * from grid_geolocationsa

-- select * from users