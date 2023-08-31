--> Módulos

--> Área promotora
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select coalesce(max(id)+1,1) from modulos),'Área promotora - Consulta',2,1
where not exists (select 1 from modulos where descricao = 'Área promotora - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Área promotora - Inclusão',2,2
where not exists (select 1 from modulos where descricao = 'Área promotora - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Área promotora - Exclusão',2,3
where not exists (select 1 from modulos where descricao = 'Área promotora - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Área promotora - Alteração',2,4
where not exists (select 1 from modulos where descricao = 'Área promotora - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin DF

--> Área promotora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2));