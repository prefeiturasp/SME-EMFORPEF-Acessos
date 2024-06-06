BEGIN TRY
	BEGIN TRAN
		Declare @SistemaNome varchar(100) = 'Conecta Formação'
		Declare @SistemaId int
		
		select @SistemaId = sis_id
		from SYS_Sistema
		where sis_nome = @SistemaNome
		
		print 'Inserindo perfil do sistema Conecta formação'
		
  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'Parecerista')
		begin
			print 'inserindo perfil Parecerista'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('E98E06D1-0556-4156-832A-613DF54E6096', 'Parecerista', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
		PRINT 'Inserção de perfil do sistema Conecta formação finalizado'
		
	COMMIT TRAN
END TRY
BEGIN
CATCH
    PRINT 'Erro ao criar grupo do Conecta formação'
    
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH