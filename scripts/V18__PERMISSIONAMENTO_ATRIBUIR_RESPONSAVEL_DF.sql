--> Atribuir responsável DF
insert into modulos (id, descricao, idmodcoresso,idacao,idsistemacoresso)
select (select max(id)+1 from modulos),'Atribuir responsável DF - Inclusão',35,2,1007
    where not exists (select 1 from modulos where descricao = 'Atribuir responsável DF - Inclusão');

insert into modulos (id, descricao, idmodcoresso,idacao,idsistemacoresso)
select (select max(id)+1 from modulos),'Atribuir responsável DF - Alteração',35,4,1007
    where not exists (select 1 from modulos where descricao = 'Atribuir responsável DF - Alteração');

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Permissões

--> Atribuir responsável DF - Admin DF
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'Admin DF'),id
from modulos where idmodcoresso = 35 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 35));