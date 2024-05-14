--> Módulos

--> Rede de Parceria (Usuários)
insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Rede de Parceria (Usuários) - Consulta',39,1,1007
where not exists (select 1 from modulos where descricao = 'Rede de Parceria (Usuários) - Consulta');

insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Rede de Parceria (Usuários) - Inclusão',39,2,1007
where not exists (select 1 from modulos where descricao = 'Rede de Parceria (Usuários) - Inclusão');

insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Rede de Parceria (Usuários) - Exclusão',39,3,1007
where not exists (select 1 from modulos where descricao = 'Rede de Parceria (Usuários) - Exclusão');

insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Rede de Parceria (Usuários) - Alteração',39,4,1007
where not exists (select 1 from modulos where descricao = 'Rede de Parceria (Usuários) - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin DF

--> Rede de Parceria (Usuários)
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 39 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2));