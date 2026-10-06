BEGIN TRANSACTION;

-- 1. Garante a criação do Módulo 40 no Conecta (sis_id = 1007) de forma idempotente
IF NOT EXISTS (SELECT 1 FROM SYS_Modulo WHERE sis_id = 1007 AND mod_id = 40)
BEGIN
    INSERT INTO SYS_Modulo (
        sis_id, mod_id, mod_nome, mod_descricao, mod_idPai, 
        mod_auditoria, mod_situacao, mod_dataCriacao, mod_dataAlteracao
    )
    VALUES (
        1007, 40, 'Pesquisa de certificados', NULL, NULL, 
        0, 1, GETDATE(), GETDATE()
    );
END

-- 2. Vincula o módulo 40 a todas as visões (vis_id) utilizadas pelos 3 perfis
-- (Evita que perfis com vis_id diferente de 1, como DICEU, fiquem sem acesso)
INSERT INTO sys_visaomodulo (vis_id, sis_id, mod_id)
SELECT DISTINCT g.vis_id, 1007, 40
FROM sys_grupo g
WHERE g.gru_id IN (
    '7EDA4540-A16C-4FE5-8322-9F75B3414E27', -- Admin DF
    '99E6D374-F85E-42B2-B3AE-EA4410F671D1'  -- DC - NEER
)
AND NOT EXISTS (
    SELECT 1 FROM sys_visaomodulo vm 
    WHERE vm.vis_id = g.vis_id AND vm.sis_id = 1007 AND vm.mod_id = 40
);

-- 3. Concede a permissão de Consulta para os 3 perfis de forma segura e consolidada
INSERT INTO SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
SELECT g.gru_id, 1007, 40, 1, 0, 0, 0
FROM (
    VALUES 
        (CAST('7EDA4540-A16C-4FE5-8322-9F75B3414E27' AS UNIQUEIDENTIFIER)), -- Admin DF
        (CAST('99E6D374-F85E-42B2-B3AE-EA4410F671D1' AS UNIQUEIDENTIFIER))  -- DC - NEER
) AS g(gru_id)
WHERE NOT EXISTS (
    SELECT 1 FROM SYS_GrupoPermissao gp 
    WHERE gp.sis_id = 1007 AND gp.mod_id = 40 AND gp.gru_id = g.gru_id
);

COMMIT TRANSACTION;