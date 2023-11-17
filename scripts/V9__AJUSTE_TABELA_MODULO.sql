-- adiciona campo idsistemacoresso para identificar de qual sistema pertence o módulo
alter table modulos add if not exists idsistemacoresso int;

-- vincula os modulos ao sistema conecta
update modulos m set idsistemacoresso = 1007
where idsistemacoresso is null
	and (
		(m.descricao like 'Área promotora -%' and m.idmodcoresso = 2) or 
		(m.descricao like 'Proposta -%' and m.idmodcoresso = 3)
	);

-- corrige idmodcoresso menu proposta
update modulos m set idmodcoresso = 4
where idsistemacoresso = 1007
	and (m.descricao like 'Proposta -%' and m.idmodcoresso = 3);

-- vincula os modulos ao sistema CDEP
update modulos m set idsistemacoresso = 1006
where idsistemacoresso is null
	and (
		(m.descricao like 'Cadastros -%' and m.idmodcoresso = 1) or	
		(m.descricao like 'Crédito -%' and m.idmodcoresso = 2) or 
		(m.descricao like 'Autor -%' and m.idmodcoresso = 3) or
		(m.descricao like 'Editora -%' and m.idmodcoresso = 4) or
		(m.descricao like 'Série/Coleção -%' and m.idmodcoresso = 5) or
		(m.descricao like 'Assunto -%' and m.idmodcoresso = 6) or
		(m.descricao like 'Acervo -%' and m.idmodcoresso = 7) or
		(m.descricao like 'Operações -%' and m.idmodcoresso = 8) or
		(m.descricao like 'Atendimento de solicitações -%' and m.idmodcoresso = 9) or
		(m.descricao like 'Solicitações -%' and m.idmodcoresso = 10)
	);

-- vincula os modulos ao sistema SGP
update modulos m set idsistemacoresso = 1000
where idsistemacoresso is null;