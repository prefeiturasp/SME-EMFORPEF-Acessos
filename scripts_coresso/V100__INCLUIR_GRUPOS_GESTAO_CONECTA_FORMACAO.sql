Declare @sistema_id INT;
	   
BEGIN TRY
	BEGIN TRAN
		select @sistema_id = sis_id		
		from SYS_Sistema ss 
		where ss.sis_nome = 'Conecta Formação';
			  
	    insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'DF',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'DF');
		
		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DIEFEM',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DIEFEM');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DIEE',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DIEE');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DIEI',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DIEI');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão NAAPA SME',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão NAAPA SME');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DIEJA',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DIEJA');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DA',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DA');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão Multimeios',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão Multimeios');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DC',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DC');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão NAC',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão NAC');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão COCEU',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão COCEU');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão CODAE',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão CODAE');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DIPED',1,getdate(),3,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DIPED');

		insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'Gestão DICEU',1,getdate(),3,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'Gestão DICEU');
				
	 COMMIT TRAN;
END TRY
BEGIN CATCH
    PRINT 'Erro ao definir permissionamento'
    
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH