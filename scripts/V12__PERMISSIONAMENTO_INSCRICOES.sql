
--> Permissões - Admin DF
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Admin DF'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  DIEFEM
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DIEFEM'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DIEE
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DIEE'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DIEI
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DIEI'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
  
--> Permissões -  NAAPA SME
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'NAAPA SME'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DIEJA
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DIEJA'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DIEJA
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DIEJA'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DA
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DA'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  Multimeios
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Multimeios'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DC - AEL
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - AEL'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  DC - SAEL
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - SAEL'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DC - NAI
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - NAI'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  DC - NEER
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - NEER'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  DC - NGD
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - NGD'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DC - EDUCOM
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - EDUCOM'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  DC - NEA
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - NEA'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  DC - TPA
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - TPA'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DC - TPA
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DC - TPA'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DF
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DF'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  NAC
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'NAC'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  COCEU
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'COCEU'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  CODAE
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'CODAE'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  SIMPEEM
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'SIMPEEM'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  SINESP
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'SINESP'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  SEDIN
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'SEDIN'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  CET
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'CET'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  APROFEM
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'APROFEM'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  SINDSEP
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'SINDSEP'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  Escola do Parlamento
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Escola do Parlamento'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  UMAPAZ
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'UMAPAZ'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
  
--> Permissões -  Arquivo Histórico Municipal
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'Arquivo Histórico Municipal'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DIPED
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DIPED'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));
  
  
--> Permissões -  DICEU
insert into permissoes (idgrupo, idmodulo) 	
select (select id from grupos where nome = 'DICEU'), id 
from modulos 
where idmodcoresso = 6 
  and idsistemacoresso = 1007 
  and not exists (select 1 from permissoes where idmodulo in (select id from modulos where idmodcoresso = 6 and idsistemacoresso =  1007));