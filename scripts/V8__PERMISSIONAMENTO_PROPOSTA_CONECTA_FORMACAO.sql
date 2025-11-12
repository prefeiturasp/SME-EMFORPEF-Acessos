--> Módulos

--> Proposta
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Proposta - Consulta',4,1,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Proposta - Inclusão',4,2,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Proposta - Exclusão',4,3,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Proposta - Alteração',4,4,1007
where not exists (select 1 from modulos where descricao = 'Proposta - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin DF

--> Proposta
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 4 and idsistemacoresso =  1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4 and idsistemacoresso =  1007));