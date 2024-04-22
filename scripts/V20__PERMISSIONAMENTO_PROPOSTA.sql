Declare @mod_id int = 3;

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Proposta
--> Permissões

--> DIEFEM
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DIEFEM'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DIEFEM')) and idsistemacoresso = 1007;

--> DIEE
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DIEE'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DIEE')) and idsistemacoresso = 1007;

--> DIEI
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DIEI'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DIEI')) and idsistemacoresso = 1007;

--> NAAPA SME
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'NAAPA SME'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'NAAPA SME')) and idsistemacoresso = 1007;

--> DIEJA
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DIEJA'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DIEJA')) and idsistemacoresso = 1007;

--> DA
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DA'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DA')) and idsistemacoresso = 1007;

--> MULTIMEIOS
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'Multimeios'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'Multimeios')) and idsistemacoresso = 1007;
--> DC - AEL
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - AEL'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - AEL')) and idsistemacoresso = 1007;

--> DC - SAEL
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - SAEL'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - SAEL')) and idsistemacoresso = 1007;

--> DC - NAI
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - NAI'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - NAI')) and idsistemacoresso = 1007;

--> DC - NEER
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - NEER'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - NEER')) and idsistemacoresso = 1007;

--> DC - NGD
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - NGD'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - NGD')) and idsistemacoresso = 1007;

--> DC - EDUCOM
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - EDUCOM'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - EDUCOM')) and idsistemacoresso = 1007;

--> DC - NEA
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - NEA'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - NEA')) and idsistemacoresso = 1007;

--> DC - TPA
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DC - TPA'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DC - TPA')) and idsistemacoresso = 1007;
        
--> DF
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DF'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DF')) and idsistemacoresso = 1007;

--> NAC
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'NAC'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'NAC')) and idsistemacoresso = 1007;

--> COCEU
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'COCEU'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'COCEU')) and idsistemacoresso = 1007;

--> CODAE
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'CODAE'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'CODAE')) and idsistemacoresso = 1007;

--> SINPEEM
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'SINPEEM'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'SINPEEM')) and idsistemacoresso = 1007;

--> SIMPEEM
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'SINESP'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'SINESP')) and idsistemacoresso = 1007;

--> SEDIN
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'SEDIN'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'SEDIN')) and idsistemacoresso = 1007;

--> CET
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'CET'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'CET')) and idsistemacoresso = 1007;

--> APROFEM
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'APROFEM'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'APROFEM')) and idsistemacoresso = 1007;

--> ESCOLA PARLAMENTO
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'Escola do Parlamento'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'Escola do Parlamento')) and idsistemacoresso = 1007;

--> UMAPAZ
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'UMAPAZ'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'UMAPAZ')) and idsistemacoresso = 1007;

--> AHM
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'Arquivo Histórico Municipal'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'Arquivo Histórico Municipal')) and idsistemacoresso = 1007;

--> DIPED
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DIPED'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DIPED')) and idsistemacoresso = 1007;

--> DICEU
insert into permissoes (idgrupo, idmodulo)
select (select id from grupos where nome = 'DICEU (DRE)'),id
from modulos where idmodcoresso = 4 and idsistemacoresso = 1007 and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = @mod_id and idsistemacoresso = 1007)
                                                                                                           and idgrupo in (select id from grupos where nome = 'DICEU (DRE)')) and idsistemacoresso = 1007;
