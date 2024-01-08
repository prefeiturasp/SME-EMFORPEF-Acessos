--> Remover permissionamento do CDEP
delete from permissoes where idmodulo in (select id from modulos  where idsistemacoresso = 1006);

--> Remover módulos não mais necessários
delete from modulos  where idsistemacoresso = 1006 and idmodcoresso in (1,8);

	
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos de Crédito
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select coalesce(max(id)+1,1) from modulos),'Crédito - Consulta',2,1
where not exists (select 1 from modulos where descricao = 'Crédito - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Crédito - Inclusão',2,2
where not exists (select 1 from modulos where descricao = 'Crédito - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Crédito - Exclusão',2,3
where not exists (select 1 from modulos where descricao = 'Crédito - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Crédito - Alteração',2,4
where not exists (select 1 from modulos where descricao = 'Crédito - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos de Autor
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Autor - Consulta',3,1
where not exists (select 1 from modulos where descricao = 'Autor - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Autor - Inclusão',3,2
where not exists (select 1 from modulos where descricao = 'Autor - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Autor - Exclusão',3,3
where not exists (select 1 from modulos where descricao = 'Autor - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Autor - Alteração',3,4
where not exists (select 1 from modulos where descricao = 'Autor - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos de Editora
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Editora - Consulta',4,1
where not exists (select 1 from modulos where descricao = 'Editora - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Editora - Inclusão',4,2
where not exists (select 1 from modulos where descricao = 'Editora - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Editora - Exclusão',4,3
where not exists (select 1 from modulos where descricao = 'Editora - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Editora - Alteração',4,4
where not exists (select 1 from modulos where descricao = 'Editora - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos de Série/Coleção
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Série/Coleção - Consulta',5,1
where not exists (select 1 from modulos where descricao = 'Série/Coleção - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Série/Coleção - Inclusão',5,2
where not exists (select 1 from modulos where descricao = 'Série/Coleção - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Série/Coleção - Exclusão',5,3
where not exists (select 1 from modulos where descricao = 'Série/Coleção - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Série/Coleção - Alteração',5,4
where not exists (select 1 from modulos where descricao = 'Série/Coleção - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos de Assunto
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Assunto - Consulta',6,1
where not exists (select 1 from modulos where descricao = 'Assunto - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Assunto - Inclusão',6,2
where not exists (select 1 from modulos where descricao = 'Assunto - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Assunto - Exclusão',6,3
where not exists (select 1 from modulos where descricao = 'Assunto - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Assunto - Alteração',6,4
where not exists (select 1 from modulos where descricao = 'Assunto - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Modulos de Acervo
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Acervo - Consulta',7,1
where not exists (select 1 from modulos where descricao = 'Acervo - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Acervo - Inclusão',7,2
where not exists (select 1 from modulos where descricao = 'Acervo - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Acervo - Exclusão',7,3
where not exists (select 1 from modulos where descricao = 'Acervo - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Acervo - Alteração',7,4
where not exists (select 1 from modulos where descricao = 'Acervo - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos de Atendimento de solicitações
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Atendimento de solicitações - Consulta',9,1
where not exists (select 1 from modulos where descricao = 'Atendimento de solicitações - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Atendimento de solicitações - Inclusão',9,2
where not exists (select 1 from modulos where descricao = 'Atendimento de solicitações - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Atendimento de solicitações - Exclusão',9,3
where not exists (select 1 from modulos where descricao = 'Atendimento de solicitações - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Atendimento de solicitações - Alteração',9,4
where not exists (select 1 from modulos where descricao = 'Atendimento de solicitações - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos de Solicitações
insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Solicitações - Consulta',10,1
where not exists (select 1 from modulos where descricao = 'Solicitações - Consulta');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Solicitações - Inclusão',10,2
where not exists (select 1 from modulos where descricao = 'Solicitações - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Solicitações - Exclusão',10,3
where not exists (select 1 from modulos where descricao = 'Solicitações - Exclusão');

insert into modulos (id, descricao, idmodcoresso,idacao) 	
select (select max(id)+1 from modulos),'Solicitações - Alteração',10,4
where not exists (select 1 from modulos where descricao = 'Solicitações - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Geral

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Geral')) and idsistemacoresso= 1006;

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Biblioteca

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Biblioteca')) and idsistemacoresso= 1006;

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Memória

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memória')) and idsistemacoresso= 1006;

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Memorial

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Admin Memorial')) and idsistemacoresso= 1006;

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Básico

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10 and idacao = 1 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Básico')) and idsistemacoresso= 1006 and idacao = 1;

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Externo - Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Externo'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10 and idsistemacoresso= 1006)
and idgrupo in (select id from grupos where nome = 'Externo')) and idsistemacoresso= 1006;