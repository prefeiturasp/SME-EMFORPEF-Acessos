--> Módulos

--> Proposta
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Proposta - Consulta',3,1,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Proposta - Inclusão',3,2,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Proposta - Exclusão',3,3,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Proposta - Alteração',3,4,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin DF

--> Proposta
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 3 and idsistemacoresso =  1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3 and idsistemacoresso =  1007));