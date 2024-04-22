Declare @sistema_id int;
declare @grupo_perfil_id uniqueidentifier;
Declare @mod_id int = 3;
Declare @grupo_nome varchar(max) = 'CET'


BEGIN TRY
	BEGIN TRAN
		
		select @sistema_id = sis_id 
		from SYS_Sistema ss 
		where ss.sis_nome = 'Conecta Formação';
		
		select @grupo_perfil_id = gru_id 
		from SYS_Grupo sg 
		where sg.sis_id = @sistema_id 
		  and gru_nome = @grupo_nome;
		 
		 
		--##### Inserindo módulo #####--
		
		if not exists(select * from SYS_Modulo where sis_id = @sistema_id and mod_idPai is null and mod_nome = @grupo_nome)
		begin
			print 'inserindo módulo'
			insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
		  	values(@sistema_id,(select coalesce(Max(mod_id)+1,1) from SYS_Modulo where sis_id = @sistema_id), @grupo_nome, null,0,1, getdate());
		end
		
		------------------------------------------------------------------------------------------------------------------------------------------------------------------
		--> Inserindo visão módulo			

		print 'inserindo visão módulos'
		insert into sys_visaomodulo 
		select 1,m.sis_id,m.mod_id 
		from sys_modulo m 
	    where m.sis_id = @sistema_id 
	      and not exists(select 1 from sys_visaomodulo v where v.sis_id = m.sis_id and v.mod_id = m.mod_id);
	     
		------------------------------------------------------------------------------------------------------------------------------------------------------------------
		--> Inserindo permissionamento  
	    
		if not exists(select 1 from SYS_GrupoPermissao gp where gp.sis_id = @sistema_id and mod_id = @mod_id and gru_id = @grupo_perfil_id)
		BEGIN 
			print 'Inserindo permissionamento do perfil ' + @grupo_nome
			
			insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
			values(@grupo_perfil_id, @sistema_id, @mod_id, 1, 1, 1, 1);
		END  
	 COMMIT TRAN
END TRY
BEGIN CATCH
    PRINT 'Erro ao definir permissionamento do perfil ' + @grupo_nome
    
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH