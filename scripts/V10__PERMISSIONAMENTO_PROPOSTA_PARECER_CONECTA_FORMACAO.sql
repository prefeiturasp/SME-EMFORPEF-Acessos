--> Módulos

--> Atribuir proposta para gestão
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select coalesce(max(id)+1,1) from modulos),'Atribuir proposta para gestão',5,2
where not exists (select 1 from modulos where descricao = 'Atribuir proposta para gestão');

--> Dar parecer da proposta
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Dar parecer da proposta',6,2
where not exists (select 1 from modulos where descricao = 'Dar parecer da proposta');

--> Devolver proposta
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Devolver proposta',7,2
where not exists (select 1 from modulos where descricao = 'Devolver proposta');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin DF

--> Atribuir proposta para gestão
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5));

--> Dar parecer da proposta
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6));

--> Devolver proposta
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7));