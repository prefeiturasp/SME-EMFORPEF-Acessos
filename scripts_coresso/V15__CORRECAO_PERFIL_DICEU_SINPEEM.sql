IF NOT EXISTS (select 1 from CoreSSO_hom.dbo.SYS_VisaoModulo where vis_id =3 and sis_id =1007 and mod_id =6)
BEGIN
	--Correção Perfil DICEU(DRE)							     
	INSERT INTO CoreSSO_hom.dbo.SYS_VisaoModulo
	(vis_id, sis_id, mod_id)
	VALUES(3, 1007, 6);
END

IF NOT EXISTS (select 1 from CoreSSO_hom.dbo.SYS_GrupoPermissao where gru_id ='AAA08B83-5DEC-4930-BF5B-44DE9876843F' and sis_id =1007 and mod_id =6)
BEGIN
	-- Correção Perfil SINPEEM
	INSERT INTO CoreSSO_hom.dbo.SYS_GrupoPermissao
	(gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
	VALUES(N'AAA08B83-5DEC-4930-BF5B-44DE9876843F', 1007, 6, 1, 1, 1, 1);
END