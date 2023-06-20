------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos

--> Operações
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Operações - Consulta',8,1
where not exists (select 1 from modulos where descricao = 'Operações - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Operações - Inclusão',8,2
where not exists (select 1 from modulos where descricao = 'Operações - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Operações - Exclusão',8,3
where not exists (select 1 from modulos where descricao = 'Operações - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Operações - Alteração',8,4
where not exists (select 1 from modulos where descricao = 'Operações - Alteração');

--> Cadastros
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Cadastros - Consulta',1,1
where not exists (select 1 from modulos where descricao = 'Cadastros - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Cadastros - Inclusão',1,2
where not exists (select 1 from modulos where descricao = 'Cadastros - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Cadastros - Exclusão',1,3
where not exists (select 1 from modulos where descricao = 'Cadastros - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Cadastros - Alteração',1,4
where not exists (select 1 from modulos where descricao = 'Cadastros - Alteração');


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Geral

--> Cadastros
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 1 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 1));

--> Operações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 8 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 8));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Biblioteca

--> Cadastros
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 1 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 1));

--> Operações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 8 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 8));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Memória

--> Cadastros
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 1 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 1));

--> Operações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 8 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 8));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Memorial

--> Cadastros
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 1 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 1));

--> Operações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 8 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 8));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Básico

--> Cadastros
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 1 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 1 and idacao = 1));

--> Operações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 8 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 8 and idacao = 1));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Externo

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Externo'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10));

--> Cadastros
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Externo'),id 
from modulos where idmodcoresso = 1 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 1));

--> Operações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Externo'),id 
from modulos where idmodcoresso = 8 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 8));