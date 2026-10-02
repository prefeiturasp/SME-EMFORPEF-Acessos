Declare @sistema_id int;
declare @grupo_admin_df_id uniqueidentifier;
declare @grupo_dc_neer_id uniqueidentifier;
Declare @mod_id int;

BEGIN TRY
	BEGIN TRAN
		select @sistema_id = sis_id 
		from SYS_Sistema ss 
		where ss.sis_nome = 'Conecta Formação';
		
		select @grupo_admin_df_id = gru_id 
		from SYS_Grupo sg 
		where sg.sis_id = @sistema_id 
		  and gru_nome = 'Admin DF';

		select @grupo_dc_neer_id = gru_id 
		from SYS_Grupo sg 
		where sg.sis_id = @sistema_id 
		  and gru_nome = 'DC - NEER';
		 
		--##### Inserindo módulo #####--
		if not exists(select * from SYS_Modulo where sis_id = @sistema_id and mod_nome = 'Pesquisa de certificados')
		begin
			print 'inserindo módulo Pesquisa de certificados'
			insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai, mod_auditoria, mod_situacao, mod_dataCriacao) 
		    values(@sistema_id, (select Max(mod_id)+1 from SYS_Modulo where sis_id = @sistema_id), 'Pesquisa de certificados', null, 0, 1, getdate());
		end
		
		select @mod_id = mod_id from SYS_Modulo where sis_id = @sistema_id and mod_nome = 'Pesquisa de certificados';					 
		------------------------------------------------------------------------------------------------------------------------------------------------------------------
		--> Inserindo visão módulo			
		
		if not exists(select * from sys_visaomodulo where sis_id = @sistema_id and mod_id = @mod_id)
		begin
			print 'inserindo visão módulos'
			insert into sys_visaomodulo (vis_id, sis_id, mod_id)
			values (1, @sistema_id, @mod_id);
		end
		------------------------------------------------------------------------------------------------------------------------------------------------------------------
		--> Inserindo permissionamento grupo Admin DF
				
		if not exists(select 1 from SYS_GrupoPermissao gp where gp.sis_id = @sistema_id and mod_id = @mod_id and gru_id = @grupo_admin_df_id)
		BEGIN 
			print 'Inserindo permissionamento do perfil Admin DF'
			
			insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
			values(@grupo_admin_df_id, @sistema_id, @mod_id, 1, 1, 1, 1);
		END

		--> Inserindo permissionamento grupo DC - NEER
		if not exists(select 1 from SYS_GrupoPermissao gp where gp.sis_id = @sistema_id and mod_id = @mod_id and gru_id = @grupo_dc_neer_id)
		BEGIN 
			print 'Inserindo permissionamento do perfil DC - NEER'
			
			insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
			values(@grupo_dc_neer_id, @sistema_id, @mod_id, 1, 1, 1, 1);
		END
	 COMMIT TRAN
END TRY
BEGIN CATCH
    PRINT 'Erro ao definir permissionamento de Pesquisa de certificados'
    
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH
