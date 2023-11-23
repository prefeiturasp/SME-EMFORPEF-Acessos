--> Módulos

--> Atribuir proposta para gestão - Consulta
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Atribuir proposta para gestão - Consulta',5,1,1007
where not exists (select 1 from modulos where descricao = 'Atribuir proposta para gestão - Consulta');

--> Atribuir proposta para gestão - Inclusão
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Atribuir proposta para gestão - Inclusão',5,2,1007
where not exists (select 1 from modulos where descricao = 'Atribuir proposta para gestão - Inclusão');

--> Atribuir proposta para gestão - Exclusão
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Atribuir proposta para gestão - Exclusão',5,3,1007
where not exists (select 1 from modulos where descricao = 'Atribuir proposta para gestão - Exclusão');

--> Atribuir proposta para gestão - Alteração
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Atribuir proposta para gestão - Alteração',5,4,1007
where not exists (select 1 from modulos where descricao = 'Atribuir proposta para gestão - Alteração');


--> Dar parecer da proposta - Consulta
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Dar parecer da proposta - Consulta',6,1,1007
where not exists (select 1 from modulos where descricao = 'Dar parecer da proposta - Consulta');

--> Dar parecer da proposta - Inclusão
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Dar parecer da proposta - Inclusão',6,2,1007
where not exists (select 1 from modulos where descricao = 'Dar parecer da proposta - Inclusão');

--> Dar parecer da proposta - Exclusão
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Dar parecer da proposta - Exclusão',6,3,1007
where not exists (select 1 from modulos where descricao = 'Dar parecer da proposta - Exclusão');

--> Dar parecer da proposta - Alteração
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Dar parecer da proposta - Alteração',6,4,1007
where not exists (select 1 from modulos where descricao = 'Dar parecer da proposta - Alteração');


--> Devolver proposta - Consulta
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Devolver proposta - Consulta',7,1,1007
where not exists (select 1 from modulos where descricao = 'Devolver proposta - Consulta');

--> Devolver proposta - Inclusão
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Devolver proposta - Inclusão',7,2,1007
where not exists (select 1 from modulos where descricao = 'Devolver proposta - Inclusão');

--> Devolver proposta - Exclusão
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Devolver proposta - Exclusão',7,3,1007
where not exists (select 1 from modulos where descricao = 'Devolver proposta - Exclusão');

--> Devolver proposta - Alteração
insert into modulos (id, descricao, idmodcoresso,idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos),'Devolver proposta - Alteração',7,4,1007
where not exists (select 1 from modulos where descricao = 'Devolver proposta - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin DF

--> Atribuir proposta para gestão
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 5 and idsistemacoresso =  1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5 and idsistemacoresso =  1007));

--> Dar parecer da proposta
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 6 and idsistemacoresso =  1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));

--> Devolver proposta
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 7 and idsistemacoresso =  1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7 and idsistemacoresso =  1007));