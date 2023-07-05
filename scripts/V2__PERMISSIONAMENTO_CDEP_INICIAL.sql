CREATE TABLE if not exists public.grupos (
	id int8 NOT NULL,
	guidperfil uuid NOT NULL,
	nome varchar(50) NOT NULL,
	idabrangencia int4 NOT NULL,
	ehperfilmanual bool NOT NULL DEFAULT false,
	CONSTRAINT grupos_pk PRIMARY KEY (id)
);

CREATE TABLE if not exists public.modulos (
	id int4 NOT NULL,
	descricao varchar(150) NOT NULL,
	idmodcoresso int4 NULL,
	idacao int4 NOT NULL,
	CONSTRAINT modulos_pk PRIMARY KEY (id)
);

CREATE TABLE if not exists public.acoes (
	id int4 NOT NULL,
	descricao varchar(20) NOT NULL,
	CONSTRAINT acoes_pk PRIMARY KEY (id)
);

ALTER TABLE public.modulos DROP CONSTRAINT if exists modulos_acao_fk;
ALTER TABLE public.modulos ADD CONSTRAINT modulos_acao_fk FOREIGN KEY (idacao) REFERENCES public.acoes(id);

CREATE TABLE if not exists public.abrangencia (
	id int4 NOT NULL,
	descricao varchar(30) NOT NULL,
	CONSTRAINT abrangencia_pk PRIMARY KEY (id)
);

CREATE TABLE if not exists public.permissoes (
	idgrupo int8 NOT NULL,
	idmodulo int8 NOT NULL,
	CONSTRAINT permissoes_pk PRIMARY KEY (idgrupo,idmodulo)
);

ALTER TABLE public.permissoes DROP CONSTRAINT if exists permissoes_grupo_fk;
ALTER TABLE public.permissoes ADD CONSTRAINT permissoes_grupo_fk FOREIGN KEY (idgrupo) REFERENCES public.grupos(id);

ALTER TABLE public.permissoes DROP CONSTRAINT if exists permissoes_modulo_fk;
ALTER TABLE public.permissoes ADD CONSTRAINT permissoes_modulo_fk FOREIGN KEY (idmodulo) REFERENCES public.modulos(id);

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Grupos

	insert into grupos (id, guidPerfil, nome, idabrangencia) 	
	select (select COALESCE(max(id)+1,1) from grupos),'D3766FB4-D753-4398-BFB0-C357724BB0A2', 'Admin Geral', 1
	where not exists (select 1 from grupos where nome = 'Admin Geral');
		
	insert into grupos (id, guidPerfil, nome, idabrangencia) 	
	select (select max(id)+1 from grupos),'B82673B9-52B9-4E01-9157-E19339B7211A', 'Admin Biblioteca', 1
	where not exists (select 1 from grupos where nome = 'Admin Biblioteca');

	insert into grupos (id, guidPerfil, nome, idabrangencia) 	
	select (select max(id)+1 from grupos),'35F9D620-49A8-446A-8A75-0A0D26EBD79D', 'Admin Memória', 1
	where not exists (select 1 from grupos where nome = 'Admin Memória');

	insert into grupos (id, guidPerfil, nome, idabrangencia) 	
	select (select max(id)+1 from grupos),'89C9D50D-B73B-4DDE-B870-7685FCD88B0C', 'Admin Memorial', 1
	where not exists (select 1 from grupos where nome = 'Admin Memorial');
	
	insert into grupos (id, guidPerfil, nome, idabrangencia) 	
	select (select max(id)+1 from grupos),'064B3481-439B-4C67-8C88-5D1F1E9B91CE', 'Básico', 1
	where not exists (select 1 from grupos where nome = 'Básico');
	
	insert into grupos (id, guidPerfil, nome, idabrangencia) 	
	select (select max(id)+1 from grupos),'3092428D-CA98-4788-9717-E706DF1945A0', 'Externo', 1
	where not exists (select 1 from grupos where nome = 'Externo');
	
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Módulos

--> Crédito
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

--> Autor
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

--> Editora
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

--> Série/Coleção
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

--> Assunto
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

--> Acervo
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

--> Atendimento de solicitações
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

--> Solicitações
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
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2));

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3));

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4));

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5));

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6));

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7));

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9));

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Geral'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Biblioteca

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2));

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3));

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4));

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5));

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6));

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7));

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9));

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Biblioteca'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Memória

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2));

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3));

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4));

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5));

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6));

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7));

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9));

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memória'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin Memorial

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2));

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3));

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4));

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5));

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6));

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7));

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9));

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin Memorial'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Básico

--> Crédito
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 2 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 2 and idacao = 1));

--> Autor
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 3 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 3 and idacao = 1));

--> Editora
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 4 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 4 and idacao = 1));

--> Série/Coleção
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 5 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 5 and idacao = 1));

--> Assunto
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 6 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idacao = 1));

--> Acervo
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 7 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 7 and idacao = 1));

--> Atendimento de solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 9 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 9 and idacao = 1));

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Básico'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10 and idacao = 1));

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Externo

--> Solicitações
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Externo'),id 
from modulos where idmodcoresso = 10 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 10));