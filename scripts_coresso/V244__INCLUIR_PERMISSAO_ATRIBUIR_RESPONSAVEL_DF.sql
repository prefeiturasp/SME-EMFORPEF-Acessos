BEGIN TRY
	BEGIN TRAN					
	------------------------------------------------------------------------------------------------------------------------------------------------------------------
	--> Obtendo o sistema						
		Declare @Sistema_Conecta int;	
		select @Sistema_Conecta = sis_id from SYS_Sistema where sis_nome = 'Conecta Formação';	
	
	------------------------------------------------------------------------------------------------------------------------------------------------------------------
	--> Inserindo módulo			
		Declare @mod_idPai_propostas int,
	            @mod_id_atribuir_responsavel_df int;	
					
		--> Propostas
		select @mod_idPai_propostas = mod_id from SYS_Modulo where sis_id = @Sistema_Conecta and mod_idPai is null and mod_nome = 'Propostas';
				
		--> Atribuir Responável DF
		if not exists(select * from SYS_Modulo where sis_id = @Sistema_Conecta and mod_idPai = @mod_idPai_propostas and mod_nome = 'Atribuir responsável DF')
		begin
			print 'inserindo módulo Atribuir responsável DF'
			insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
			  values(@Sistema_Conecta,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_Conecta), 'Atribuir responsável DF', @mod_idPai_propostas,0,1, getdate());
			  
			select @mod_id_atribuir_responsavel_df = mod_id from SYS_Modulo where sis_id = @Sistema_Conecta and mod_idPai = @mod_idPai_propostas and mod_nome = 'Atribuir responsável DF';		
			
			--> Inserindo grupo_permissao
			insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
			select '7EDA4540-A16C-4FE5-8322-9F75B3414E27',@Sistema_Conecta as sis_id,@mod_id_atribuir_responsavel_df,1,1,1,1;
			
		end			
	COMMIT TRAN
END TRY
BEGIN CATCH
	PRINT 'Erro ao definir permissionamento para Atribuir responsável DF'
	
	IF(@@TRANCOUNT > 0)
		ROLLBACK TRAN;
END CATCH	