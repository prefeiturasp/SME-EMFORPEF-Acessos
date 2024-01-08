--> Módulos

--> Incrições
insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Inscrições - Consulta', 6, 1, 1007
where not exists (select 1 from modulos where descricao = 'Inscrições - Consulta');

insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Inscrições - Inclusão', 6, 2, 1007
where not exists (select 1 from modulos where descricao = 'Inscrições - Inclusão');

insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Inscrições - Exclusão', 6, 3, 1007
where not exists (select 1 from modulos where descricao = 'Inscrições - Exclusão');

insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos),'Inscrições - Alteração', 6, 4, 1007
where not exists (select 1 from modulos where descricao = 'Inscrições - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Cursista

--> Proposta
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Cursista'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
