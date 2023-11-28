Declare @sistema_id INT;
	   
BEGIN TRY
	BEGIN TRAN
		select @sistema_id = sis_id		
		from SYS_Sistema ss 
		where ss.sis_nome = 'Conecta Formação';
			  
	    insert into SYS_Grupo(gru_nome, gru_situacao,gru_dataCriacao, vis_id, sis_id)
	    select 'DF',1,getdate(),1,@sistema_id where not exists (select * from sys_grupo where gru_nome = 'DF');		
	 COMMIT TRAN;
END TRY
BEGIN CATCH
    PRINT 'Erro ao definir permissionamento'
    
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH