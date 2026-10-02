--> Módulos

--> Pesquisa de certificados - Consulta
insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select coalesce(max(id)+1,1) from modulos), 'Pesquisa de certificados - Consulta', 40, 1, 1007
where not exists (select 1 from modulos where idsistemacoresso = 1007 and idmodcoresso = 40 and idacao = 1);

update modulos 
set descricao = 'Pesquisa de certificados - Consulta' 
where idsistemacoresso = 1007 and idmodcoresso = 40 and idacao = 1 and descricao <> 'Pesquisa de certificados - Consulta';

--> Pesquisa de certificados - Inclusão
insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos), 'Pesquisa de certificados - Inclusão', 40, 2, 1007
where not exists (select 1 from modulos where idsistemacoresso = 1007 and idmodcoresso = 40 and idacao = 2);

--> Pesquisa de certificados - Exclusão
insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos), 'Pesquisa de certificados - Exclusão', 40, 3, 1007
where not exists (select 1 from modulos where idsistemacoresso = 1007 and idmodcoresso = 40 and idacao = 3);

--> Pesquisa de certificados - Alteração
insert into modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso) 	
select (select max(id)+1 from modulos), 'Pesquisa de certificados - Alteração', 40, 4, 1007
where not exists (select 1 from modulos where idsistemacoresso = 1007 and idmodcoresso = 40 and idacao = 4);

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões - Admin DF e DC - NEER

insert into permissoes (idgrupo, idmodulo) 	
select g.id, m.id
from modulos m 
cross join grupos g
where m.idmodcoresso = 40 and m.idsistemacoresso = 1007
  and g.nome in ('Admin DF', 'DC - NEER')
  and not exists(select 1 from permissoes p where p.idgrupo = g.id and p.idmodulo = m.id);
