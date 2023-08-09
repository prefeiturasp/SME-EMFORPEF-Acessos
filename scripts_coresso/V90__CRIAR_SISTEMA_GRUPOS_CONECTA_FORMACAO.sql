BEGIN TRY
	BEGIN TRAN
	
		Declare @SistemaId int
		Declare @SistemaNome varchar(100) = 'Conecta Formação'
		Declare @SistemaDescricao varchar(100) = 'Sistema de Gestão de Formação'
		
		--> Inserindo sistema
		if not exists(select * from SYS_Sistema where sis_nome = 'Conecta Formação')
		begin
			print 'inserindo sistema ' + @SistemaNome
			insert into SYS_Sistema(sis_id,	sis_nome, sis_descricao)
			values((select Max(sis_id)+ 1 from SYS_Sistema), @SistemaNome, @SistemaDescricao)
		end
		
		select @SistemaId = sis_id
		from SYS_Sistema
		where sis_nome = @SistemaNome
		
	------------------------------------------------------------------------------------------------------------------------------------------------------------------
	--> Inserindo Grupos	
		print 'Inserindo perfis do sistema ' + @SistemaNome
		
  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - SAEL')
		begin
			print 'inserindo perfil DC - SAEL'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('2258698A-D07B-471C-A76B-0AC8324C2FEE', 'DC - SAEL', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end

  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'NAC')
		begin
			print 'inserindo perfil NAC'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('60CF15A9-26A9-4845-B8A0-0FBE53074B87', 'NAC', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - NGD')
		begin
			print 'inserindo perfil DC - NGD'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('2AA46DA5-E9DA-4BD5-B2AD-119D1E6D60B4', 'DC - NGD', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DIEFEM')
		begin
			print 'inserindo perfil DIEFEM'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('CA8D4F09-F7D2-4CFC-9198-13B0B365D635', 'DIEFEM', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		
		
  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'CODAE')
		begin
			print 'inserindo perfil CODAE'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('7FCBFE72-957A-4216-936B-174DDA6917C4', 'CODAE', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'Escola do Parlamento')
		begin
			print 'inserindo perfil Escola do Parlamento'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('B88BA8AE-28A4-416E-BF18-2329F1EEECB8', 'Escola do Parlamento', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		

  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'Multimeios')
		begin
			print 'inserindo perfil Multimeios'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('1507D96B-CF5D-454E-AEDC-25F41BD9FFC8', 'Multimeios', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		
		
  	    if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DIEE')
		begin
			print 'inserindo perfil DIEE'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('2F6218F5-9F79-450D-9BA9-3308F58D130F', 'DIEE', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end				
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'SINDSEP')
		begin
			print 'inserindo perfil SINDSEP'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('0C51EFEC-CFF8-4C92-A815-37FFE6838AB7', 'SINDSEP', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'APROFEM')
		begin
			print 'inserindo perfil APROFEM'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('3B8CBE5E-7A5A-46C2-9EB1-38BFBC7A2F79', 'APROFEM', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end

		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'SINPEEM')
		begin
			print 'inserindo perfil SINPEEM'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('AAA08B83-5DEC-4930-BF5B-44DE9876843F', 'SINPEEM', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		

		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'CET')
		begin
			print 'inserindo perfil CET'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('77B621E4-7DA5-4603-A3ED-482023266CC1', 'CET', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end	
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'Arquivo Histórico Municipal')
		begin
			print 'inserindo perfil Arquivo Histórico Municipal'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('EBD0970B-1737-4E63-A629-4E9910033E93', 'Arquivo Histórico Municipal', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'COCEU')
		begin
			print 'inserindo perfil COCEU'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('4640EFFA-A578-41A2-972A-5F5D82F59B3D', 'COCEU', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - AEL')
		begin
			print 'inserindo perfil DC - AEL'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('480EF9B8-2390-4B1D-BD3D-7135B8A89E43', 'DC - AEL', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end	
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DICEU (DRE)')
		begin
			print 'inserindo perfil DICEU (DRE)'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('7ED757C4-D556-4456-B283-75CD6BC03B38', 'DICEU (DRE)', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		

		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - NAI')
		begin
			print 'inserindo perfil DC - NAI'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('5124DEFA-4D40-4CA0-9768-793CE7159E6F', 'DC - NAI', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DIPED (DRE)')
		begin
			print 'inserindo perfil DIPED (DRE)'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('7BAB7FFD-400E-4216-986E-7FEF28ECC61B', 'DIPED (DRE)', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DIEJA')
		begin
			print 'inserindo perfil DIEJA'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('7026F06A-2AF5-482C-9BF0-8F174C8CE6D1', 'DIEJA', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'SINESP')
		begin
			print 'inserindo perfil SINESP'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('E2D5CE82-5BA0-4591-91CF-9AA1B2FA8206', 'SINESP', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'Admin DF')
		begin
			print 'inserindo perfil Admin DF'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('7EDA4540-A16C-4FE5-8322-9F75B3414E27', 'Admin DF', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end	
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - TPA')
		begin
			print 'inserindo perfil DC - TPA'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('601720F7-F4BE-4327-9485-A048FC1031EB', 'DC - TPA', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end				
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'UMAPAZ')
		begin
			print 'inserindo perfil UMAPAZ'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('1F897E79-6B51-43B1-94C7-A21EB9B8C5B0', 'UMAPAZ', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'SEDIN')
		begin
			print 'inserindo perfil SEDIN'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('C520F757-3EE2-43E8-8229-A7BEEECD3672', 'SEDIN', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		

		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'Cursista')
		begin
			print 'inserindo perfil Cursista'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('651914B6-C4B6-4463-B773-B0960F4A148B', 'Cursista', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'NAAPA SME')
		begin
			print 'inserindo perfil NAAPA SME'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('8AADF069-D576-4A96-90CF-B3B601E9A7AC', 'NAAPA SME', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end		
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - EDUCOM')
		begin
			print 'inserindo perfil DC - EDUCOM'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('82126909-F0CB-48DB-BC4E-CABBFCF1E766', 'DC - EDUCOM', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - NEA')
		begin
			print 'inserindo perfil DC - NEA'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('D4A6C2C6-99A1-4B75-8D35-DEED2D38EDA3', 'DC - NEA', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end	
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DIEI')
		begin
			print 'inserindo perfil DIEI'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('69832C57-03B3-4D86-BBFB-E158AB83D719', 'DIEI', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end			

		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DC - NEER')
		begin
			print 'inserindo perfil DC - NEER'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('99E6D374-F85E-42B2-B3AE-EA4410F671D1', 'DC - NEER', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end	
		
		if not exists( select 1 from SYS_Grupo where sis_id = @SistemaId and vis_id = 1 and gru_nome = 'DA')
		begin
			print 'inserindo perfil DA'
		
			insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade)
			values('DF3E65FB-A897-44A4-BCDA-F30801455C88', 'DA', 1, getdate(), getdate(), 1, @SistemaId, 0);
		end			
		
		PRINT 'Inserção dos perfis do sistema ' + @SistemaNome + ' finalizado'
		
	COMMIT TRAN
END TRY
BEGIN
CATCH
    PRINT 'Erro ao criar sistema e grupos do Conecta formação'
    
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH