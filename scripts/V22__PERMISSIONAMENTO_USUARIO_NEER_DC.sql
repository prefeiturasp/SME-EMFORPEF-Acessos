BEGIN;

-- 1. Garante os 4 módulos (com suas respectivas ações 1=Consulta, 2=Inclusão, 3=Exclusão, 4=Alteração)
INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT (SELECT COALESCE(MAX(id) + 1, 1) FROM modulos), 'Pesquisa de certificados - Consulta', 40, 1, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE descricao = 'Pesquisa de certificados - Consulta');

INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT (SELECT COALESCE(MAX(id) + 1, 1) FROM modulos), 'Pesquisa de certificados - Inclusão', 40, 2, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE descricao = 'Pesquisa de certificados - Inclusão');

INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT (SELECT COALESCE(MAX(id) + 1, 1) FROM modulos), 'Pesquisa de certificados - Exclusão', 40, 3, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE descricao = 'Pesquisa de certificados - Exclusão');

INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT (SELECT COALESCE(MAX(id) + 1, 1) FROM modulos), 'Pesquisa de certificados - Alteração', 40, 4, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE descricao = 'Pesquisa de certificados - Alteração');


-- 2. Concede as permissões para o perfil DC - NEER
INSERT INTO permissoes (idgrupo, idmodulo)
SELECT g.id, m.id
FROM modulos m
JOIN grupos g ON g.nome = 'DC - NEER'
WHERE m.idmodcoresso = 40 AND m.idsistemacoresso = 1007
AND NOT EXISTS (
    SELECT 1 FROM permissoes p WHERE p.idgrupo = g.id AND p.idmodulo = m.id
);

-- 3. Concede as permissões para o perfil Admin DF
INSERT INTO permissoes (idgrupo, idmodulo)
SELECT g.id, m.id
FROM modulos m
JOIN grupos g ON g.nome = 'Admin DF'
WHERE m.idmodcoresso = 40 AND m.idsistemacoresso = 1007
AND NOT EXISTS (
    SELECT 1 FROM permissoes p WHERE p.idgrupo = g.id AND p.idmodulo = m.id
);

COMMIT;