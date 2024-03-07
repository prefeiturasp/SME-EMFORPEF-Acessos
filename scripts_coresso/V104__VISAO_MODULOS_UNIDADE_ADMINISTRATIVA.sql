insert into sys_visaomodulo 
select 3, m.sis_id, m.mod_id from sys_modulo m where m.sis_id = 1007 and not exists(select 1 from sys_visaomodulo v where v.sis_id = m.sis_id and v.mod_id = m.mod_id);