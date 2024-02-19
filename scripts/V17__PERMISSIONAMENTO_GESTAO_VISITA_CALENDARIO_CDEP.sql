--> Gestão de visitas (Calendário)
insert into modulos (id, descricao, idmodcoresso,idacao,idsistemacoresso) 	
select (select max(id)+1 from modulos),'Gestão de visitas (Calendário) - Consulta',11,1,1006
where not exists (select 1 from modulos where descricao = 'Gestão de visitas (Calendário) - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao,idsistemacoresso) 	
select (select max(id)+1 from modulos),'Gestão de visitas (Calendário) - Inclusão',11,2,1006
where not exists (select 1 from modulos where descricao = 'Gestão de visitas (Calendário) - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao,idsistemacoresso) 	
select (select max(id)+1 from modulos),'Gestão de visitas (Calendário) - Exclusão',11,3,1006
where not exists (select 1 from modulos where descricao = 'Gestão de visitas (Calendário) - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao,idsistemacoresso) 	
select (select max(id)+1 from modulos),'Gestão de visitas (Calendário) - Alteração',11,4,1006
where not exists (select 1 from modulos where descricao = 'Gestão de visitas (Calendário) - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões

--> Gestão de visitas (Calendário) - Admin Geral
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 11 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 11));

--> Gestão de visitas (Calendário) - Admin Biblioteca
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 11 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 11));

--> Gestão de visitas (Calendário) - Admin Memória
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 11));

--> Gestão de visitas (Calendário) - Admin Memorial
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 11 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 11));

--> Gestão de visitas (Calendário) - Básico
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 11 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 11 and idacao = 1));

update modulos set idsistemacoresso = 1006 where id in (304,303,302,301,300,299,298,297);

