BEGIN;

-- 1. Garante os 4 módulos (com suas respectivas ações 1=Consulta, 2=Inclusão, 3=Exclusão, 4=Alteração)
INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT 307, 'Pesquisa de certificados - Consulta', 40, 1, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE id = 307);

INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT 308, 'Pesquisa de certificados - Inclusão', 40, 2, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE id = 308);

INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT 309, 'Pesquisa de certificados - Exclusão', 40, 3, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE id = 309);

INSERT INTO modulos (id, descricao, idmodcoresso, idacao, idsistemacoresso)
SELECT 310, 'Pesquisa de certificados - Alteração', 40, 4, 1007
WHERE NOT EXISTS (SELECT 1 FROM modulos WHERE id = 310);


-- 2. Concede as permissões para o perfil DC - NEER - Grupo 83
INSERT INTO permissoes (idgrupo, idmodulo)
SELECT 83, m.id
FROM (VALUES (307), (308), (309), (310)) AS m(id)
WHERE NOT EXISTS (
    SELECT 1 FROM permissoes WHERE idgrupo = 83 AND idmodulo = m.id
);

-- 3. Concede as permissões para o perfil Admin DF - Grupo 75
INSERT INTO permissoes (idgrupo, idmodulo)
SELECT 75, m.id
FROM (VALUES (307), (308), (309), (310)) AS m(id)
WHERE NOT EXISTS (
    SELECT 1 FROM permissoes WHERE idgrupo = 75 AND idmodulo = m.id
);

COMMIT;