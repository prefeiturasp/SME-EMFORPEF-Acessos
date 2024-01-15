--Correção Perfil DICEU(DRE)							     
INSERT INTO CoreSSO_hom.dbo.SYS_VisaoModulo
(vis_id, sis_id, mod_id)
VALUES(3, 1007, 6);


-- Correção Perfil SINPEEM
INSERT INTO CoreSSO_hom.dbo.SYS_GrupoPermissao
(gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
VALUES(N'AAA08B83-5DEC-4930-BF5B-44DE9876843F', 1007, 6, 1, 1, 1, 1);