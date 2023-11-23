Declare @sistema_id INT;
	   
BEGIN TRY
	BEGIN TRAN
		select @sistema_id = sis_id		
		from SYS_Sistema ss 
		where ss.sis_nome = 'Conecta Formação';		  
	   
	   --> Atribuir proposta para gestão
	   insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
	   select @sistema_id,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @sistema_id), 'Atribuir proposta para gestão', null,0,1, getdate() 
	   where not exists (select * from SYS_Modulo where mod_nome = 'Atribuir proposta para gestão');
	  
	  --> Dar parecer da proposta
	   insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
	   select @sistema_id,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @sistema_id), 'Dar parecer da proposta', null,0,1, getdate() 
	   where not exists (select * from SYS_Modulo where mod_nome = 'Dar parecer da proposta');
	  
	  --> Devolver proposta
	   insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
	   select @sistema_id,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @sistema_id), 'Devolver proposta', null,0,1, getdate() 
	   where not exists (select * from SYS_Modulo where mod_nome = 'Devolver proposta');	   
				
	 COMMIT TRAN;
END TRY
BEGIN CATCH
    PRINT 'Erro ao definir módulos do CoreSSO'
    
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH