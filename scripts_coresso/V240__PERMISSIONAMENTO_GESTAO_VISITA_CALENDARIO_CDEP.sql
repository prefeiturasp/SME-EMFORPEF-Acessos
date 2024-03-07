BEGIN TRY
BEGIN TRAN
				
------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Inserindo sistema
					
	Declare @Sistema_CDEP int;

	select @Sistema_CDEP = sis_id from SYS_Sistema where sis_nome = 'CDEP';	

------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Inserindo módulo
		
	Declare @mod_idPai_operacoes int,
            @mod_id_gestao_visita_calendario int;	
				
	--> Operações
	select @mod_idPai_operacoes = mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai is null and mod_nome = 'Operações';
			
	--> Gestão de visitas (Calendário)
	if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_operacoes and mod_nome = 'Gestão de visitas (Calendário)')
	begin
		print 'inserindo módulo Gestão de visitas (Calendário)'
		insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
		  values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Gestão de visitas (Calendário)', @mod_idPai_operacoes,0,1, getdate());
		  
		select @mod_id_gestao_visita_calendario = mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_operacoes and mod_nome = 'Gestão de visitas (Calendário)';
	
		 print 'inserindo visão módulos'
		insert into sys_visaomodulo 
    	select 1,sis_id, mod_id from sys_modulo where sis_id = @Sistema_CDEP and mod_id = @mod_id_gestao_visita_calendario;  
		
		--> Inserindo grupo_permissao
		insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
		select 'D3766FB4-D753-4398-BFB0-C357724BB0A2',@Sistema_CDEP as sis_id,@mod_id_gestao_visita_calendario,1,1,1,1 union all
		select 'B82673B9-52B9-4E01-9157-E19339B7211A',@Sistema_CDEP as sis_id,@mod_id_gestao_visita_calendario,1,1,1,1 union all
		select '35F9D620-49A8-446A-8A75-0A0D26EBD79D',@Sistema_CDEP as sis_id,@mod_id_gestao_visita_calendario,1,1,1,1 union all
		select '89C9D50D-B73B-4DDE-B870-7685FCD88B0C',@Sistema_CDEP as sis_id,@mod_id_gestao_visita_calendario,1,1,1,1 union all
		select '064B3481-439B-4C67-8C88-5D1F1E9B91CE',@Sistema_CDEP as sis_id,@mod_id_gestao_visita_calendario,1,0,0,0;
		
	end			
COMMIT TRAN
END TRY
BEGIN CATCH
	PRINT 'Erro ao definir permissionamento para Gestão de visitas (Calendário)'
	
	IF(@@TRANCOUNT > 0)
		ROLLBACK TRAN;
END CATCH		