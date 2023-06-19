------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Inserindo sistema
					
			Declare @Sistema_CDEP int;
			
			if not exists(select * from SYS_Sistema where sis_nome = 'CDEP')
			begin
				print 'inserindo sistema CDEP'
				insert into SYS_Sistema(sis_id, sis_nome, sis_descricao) 
			      values((select Max(sis_id)+1 from SYS_Sistema), 'CDEP', 'Centro de Documentação da Educação Paulistana');     
			end
			select @Sistema_CDEP = sis_id from SYS_Sistema where sis_nome = 'CDEP'

------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Inserindo módulo
		
			Declare @mod_idPai_cadastros int,
						 @mod_idPai_operacoes int;
			
			--> Cadastros
			if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai is null and mod_nome = 'Cadastros')
			begin
				print 'inserindo módulo Cadastros'
				insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
			      values(@Sistema_CDEP,(select coalesce(Max(mod_id)+1,1) from SYS_Modulo where sis_id = @Sistema_CDEP), 'Cadastros', null,0,1, getdate());
			end
			select @mod_idPai_cadastros = mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai is null and mod_nome = 'Cadastros';
			
				--> Crédito
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_cadastros and mod_nome = 'Crédito')
				begin
					print 'inserindo módulo Crédito'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Crédito', @mod_idPai_cadastros,0,1, getdate());
				end
				
				--> Autor
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_cadastros and mod_nome = 'Autor')
				begin
					print 'inserindo módulo Autor'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Autor', @mod_idPai_cadastros,0,1, getdate());
				end
				
				--> Editora
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_cadastros and mod_nome = 'Editora')
				begin
					print 'inserindo módulo Editora'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Editora', @mod_idPai_cadastros,0,1, getdate());
				end
				
				--> Série/Coleção
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_cadastros and mod_nome = 'Série/Coleção')
				begin
					print 'inserindo módulo Série/Coleção'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Série/Coleção', @mod_idPai_cadastros,0,1, getdate());
				end
				
				--> Assunto
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_cadastros and mod_nome = 'Assunto')
				begin
					print 'inserindo módulo Assunto'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Assunto', @mod_idPai_cadastros,0,1, getdate());
				end
				
				--> Acervo
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_cadastros and mod_nome = 'Acervo')
				begin
					print 'inserindo módulo Acervo'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Acervo', @mod_idPai_cadastros,0,1, getdate());
				end
				
				--> Acervo
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_cadastros and mod_nome = 'Acervo')
				begin
					print 'inserindo módulo Acervo'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Acervo', @mod_idPai_cadastros,0,1, getdate());
				end
				
			--> Operações
			if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai is null and mod_nome = 'Operações')
			begin
				print 'inserindo módulo Operações'
				insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
			      values(@Sistema_CDEP,(select coalesce(Max(mod_id)+1,1) from SYS_Modulo where sis_id = @Sistema_CDEP), 'Operações', null,0,1, getdate());
			end
			select @mod_idPai_operacoes = mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai is null and mod_nome = 'Operações';
			
				--> Atendimento de solicitações
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_operacoes and mod_nome = 'Atendimento de solicitações')
				begin
					print 'inserindo módulo Atendimento de solicitações'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Atendimento de solicitações', @mod_idPai_operacoes,0,1, getdate());
				end
				
				--> Solicitações
				if not exists(select * from SYS_Modulo where sis_id = @Sistema_CDEP and mod_idPai = @mod_idPai_operacoes and mod_nome = 'Solicitações')
				begin
					print 'inserindo módulo Solicitações'
					insert into SYS_Modulo(sis_id, mod_id, mod_nome, mod_idPai,mod_auditoria,mod_situacao, mod_dataCriacao) 
				      values(@Sistema_CDEP,(select Max(mod_id)+1 from SYS_Modulo where sis_id = @Sistema_CDEP), 'Solicitações', @mod_idPai_operacoes,0,1, getdate());
				end					
					 	 
------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Inserindo Grupos
	
			--> Admin geral
			BEGIN TRY
				BEGIN TRAN
					print 'Inserindo permissionamento do perfil Admin Geral'
					
					if not exists(select * from SYS_Grupo where sis_id = @Sistema_CDEP and vis_id = 1 and gru_nome = 'Admin Geral')
					begin
						print 'inserindo perfil Admin Geral'
						insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade) 
					      values('D3766FB4-D753-4398-BFB0-C357724BB0A2', 'Admin Geral', 1, getdate(), getdate(), 1, @Sistema_CDEP, 0);
					end
					
					declare @Telas table (mod_id int, grp_consultar bit, grp_inserir bit, grp_alterar bit, grp_excluir bit)
			
					-- Módulos
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Cadastros'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Crédito'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Autor'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Editora'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Série/Coleção'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Assunto'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Acervo'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Operações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Atendimento de solicitações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Solicitações'), 1, 1,1, 1)
					
					insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
					select 'D3766FB4-D753-4398-BFB0-C357724BB0A2' as gru_id,
					        @Sistema_CDEP as sis_id,
					        tls.mod_id,
					        tls.grp_consultar,
					        tls.grp_inserir,
					        tls.grp_alterar,
					        tls.grp_excluir 
					   from @Telas tls
					  where not EXISTS (select sgp.gru_id 
					                      from SYS_GrupoPermissao sgp
					                     where sgp.gru_id = 'D3766FB4-D753-4398-BFB0-C357724BB0A2'
					                       and sgp.sis_id = @Sistema_CDEP
					                       and sgp.mod_id = tls.mod_id)
			
			    	PRINT 'Permissionamento para perfil Admin Geral definido'
					
			    	delete from @Telas;
			    
				COMMIT TRAN
			END TRY
			BEGIN CATCH
			    PRINT 'Erro ao definir permissionamento do perfil Admin Geral'
			    
			    IF(@@TRANCOUNT > 0)
			        ROLLBACK TRAN;
			END CATCH
			
			--> Admin Biblioteca
			BEGIN TRY
				BEGIN TRAN
					print 'Inserindo permissionamento do perfil Admin Biblioteca'
					
					if not exists(select * from SYS_Grupo where sis_id = @Sistema_CDEP and vis_id = 1 and gru_nome = 'Admin Biblioteca')
					begin
						print 'inserindo perfil Admin Biblioteca'
						insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade) 
					      values('B82673B9-52B9-4E01-9157-E19339B7211A', 'Admin Biblioteca', 1, getdate(), getdate(), 1, @Sistema_CDEP, 0);
					end
										
					-- Módulos
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Cadastros'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Crédito'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Autor'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Editora'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Série/Coleção'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Assunto'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Acervo'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Operações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Atendimento de solicitações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Solicitações'), 1, 1,1, 1)
					
					insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
					select 'B82673B9-52B9-4E01-9157-E19339B7211A' as gru_id,
					        @Sistema_CDEP as sis_id,
					        tls.mod_id,
					        tls.grp_consultar,
					        tls.grp_inserir,
					        tls.grp_alterar,
					        tls.grp_excluir 
					   from @Telas tls
					  where not EXISTS (select sgp.gru_id 
					                      from SYS_GrupoPermissao sgp
					                     where sgp.gru_id = 'B82673B9-52B9-4E01-9157-E19339B7211A'
					                       and sgp.sis_id = @Sistema_CDEP
					                       and sgp.mod_id = tls.mod_id)
			
			    	PRINT 'Permissionamento para perfil Admin Biblioteca definido'
					delete from @Telas;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
			    PRINT 'Erro ao definir permissionamento do perfil Admin Biblioteca'
			    
			    IF(@@TRANCOUNT > 0)
			        ROLLBACK TRAN;
			END CATCH
			
			--> Admin Memória
			BEGIN TRY
				BEGIN TRAN
					print 'Inserindo permissionamento do perfil Admin Memória'
					
					if not exists(select * from SYS_Grupo where sis_id = @Sistema_CDEP and vis_id = 1 and gru_nome = 'Admin Memória')
					begin
						print 'inserindo perfil Admin Memória'
						insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade) 
					      values('35F9D620-49A8-446A-8A75-0A0D26EBD79D', 'Admin Memória', 1, getdate(), getdate(), 1, @Sistema_CDEP, 0);
					end
					
					-- Módulos
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Cadastros'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Crédito'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Autor'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Editora'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Série/Coleção'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Assunto'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Acervo'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Operações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Atendimento de solicitações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Solicitações'), 1, 1,1, 1)
					
					insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
					select '35F9D620-49A8-446A-8A75-0A0D26EBD79D' as gru_id,
					        @Sistema_CDEP as sis_id,
					        tls.mod_id,
					        tls.grp_consultar,
					        tls.grp_inserir,
					        tls.grp_alterar,
					        tls.grp_excluir 
					   from @Telas tls
					  where not EXISTS (select sgp.gru_id 
					                      from SYS_GrupoPermissao sgp
					                     where sgp.gru_id = '35F9D620-49A8-446A-8A75-0A0D26EBD79D'
					                       and sgp.sis_id = @Sistema_CDEP
					                       and sgp.mod_id = tls.mod_id)
			
			    	PRINT 'Permissionamento para perfil Admin Memória definido'
					delete from @Telas;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
			    PRINT 'Erro ao definir permissionamento do perfil Admin Memória'
			    
			    IF(@@TRANCOUNT > 0)
			        ROLLBACK TRAN;
			END CATCH
			
			
			--> Admin Memorial
			BEGIN TRY
				BEGIN TRAN
					print 'Inserindo permissionamento do perfil Admin Memorial'
					
					if not exists(select * from SYS_Grupo where sis_id = @Sistema_CDEP and vis_id = 1 and gru_nome = 'Admin Memorial')
					begin
						print 'inserindo perfil Admin Memorial'
						insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade) 
					      values('89C9D50D-B73B-4DDE-B870-7685FCD88B0C', 'Admin Memorial', 1, getdate(), getdate(), 1, @Sistema_CDEP, 0);
					end
					
					-- Módulos
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Cadastros'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Crédito'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Autor'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Editora'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Série/Coleção'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Assunto'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Acervo'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Operações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Atendimento de solicitações'), 1, 1,1, 1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Solicitações'), 1, 1,1, 1)
					
					insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
					select '89C9D50D-B73B-4DDE-B870-7685FCD88B0C' as gru_id,
					        @Sistema_CDEP as sis_id,
					        tls.mod_id,
					        tls.grp_consultar,
					        tls.grp_inserir,
					        tls.grp_alterar,
					        tls.grp_excluir 
					   from @Telas tls
					  where not EXISTS (select sgp.gru_id 
					                      from SYS_GrupoPermissao sgp
					                     where sgp.gru_id = '89C9D50D-B73B-4DDE-B870-7685FCD88B0C'
					                       and sgp.sis_id = @Sistema_CDEP
					                       and sgp.mod_id = tls.mod_id)
			
			    	PRINT 'Permissionamento para perfil Admin Memorial definido'
					delete from @Telas;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
			    PRINT 'Erro ao definir permissionamento do perfil Admin Memorial'
			    
			    IF(@@TRANCOUNT > 0)
			        ROLLBACK TRAN;
			END CATCH
			
			--> Básico
			BEGIN TRY
				BEGIN TRAN
					print 'Inserindo permissionamento do perfil Básico'
					
					if not exists(select * from SYS_Grupo where sis_id = @Sistema_CDEP and vis_id = 1 and gru_nome = 'Básico')
					begin
						print 'inserindo perfil Básico'
						insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade) 
					      values('064B3481-439B-4C67-8C88-5D1F1E9B91CE', 'Básico', 1, getdate(), getdate(), 1, @Sistema_CDEP, 0);
					end
					
					-- Módulos
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Cadastros'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Crédito'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Autor'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Editora'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Série/Coleção'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Assunto'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Acervo'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Operações'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Atendimento de solicitações'), 1, 0,0, 0)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Solicitações'), 1, 0,0, 0)	
					
					insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
					select '064B3481-439B-4C67-8C88-5D1F1E9B91CE' as gru_id,
					        @Sistema_CDEP as sis_id,
					        tls.mod_id,
					        tls.grp_consultar,
					        tls.grp_inserir,
					        tls.grp_alterar,
					        tls.grp_excluir 
					   from @Telas tls
					  where not EXISTS (select sgp.gru_id 
					                      from SYS_GrupoPermissao sgp
					                     where sgp.gru_id = '064B3481-439B-4C67-8C88-5D1F1E9B91CE'
					                       and sgp.sis_id = @Sistema_CDEP
					                       and sgp.mod_id = tls.mod_id)
			
			    	PRINT 'Permissionamento para perfil Básico definido'
					delete from @Telas;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
			    PRINT 'Erro ao definir permissionamento do perfil Básico'
			    
			    IF(@@TRANCOUNT > 0)
			        ROLLBACK TRAN;
			END CATCH
					
			
			--> Externo
			BEGIN TRY
				BEGIN TRAN
					print 'Inserindo permissionamento do perfil Externo'
					
					if not exists(select * from SYS_Grupo where sis_id = @Sistema_CDEP and vis_id = 1 and gru_nome = 'Externo')
					begin
						print 'inserindo perfil Externo'
						insert into SYS_Grupo(gru_id, gru_nome, gru_situacao, gru_dataCriacao, gru_dataAlteracao, vis_id, sis_id, gru_integridade) 
					      values('3092428D-CA98-4788-9717-E706DF1945A0', 'Externo', 1, getdate(), getdate(), 1, @Sistema_CDEP, 0);
					end
					
					-- Módulos
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Operações'), 1, 1,1,1)
					insert into @Telas values((select mod_id from SYS_Modulo where sis_id = @Sistema_CDEP and mod_nome = 'Solicitações'), 1, 1,1, 1)
					
					insert into SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
					select '3092428D-CA98-4788-9717-E706DF1945A0' as gru_id,
					        @Sistema_CDEP as sis_id,
					        tls.mod_id,
					        tls.grp_consultar,
					        tls.grp_inserir,
					        tls.grp_alterar,
					        tls.grp_excluir 
					   from @Telas tls
					  where not EXISTS (select sgp.gru_id 
					                      from SYS_GrupoPermissao sgp
					                     where sgp.gru_id = '3092428D-CA98-4788-9717-E706DF1945A0'
					                       and sgp.sis_id = @Sistema_CDEP
					                       and sgp.mod_id = tls.mod_id)
			
			    	PRINT 'Permissionamento para perfil Externo definido'
					delete from @Telas;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
			    PRINT 'Erro ao definir permissionamento do perfil Externo'
			    
			    IF(@@TRANCOUNT > 0)
			        ROLLBACK TRAN;
			END CATCH
			
------------------------------------------------------------------------------------------------------------------------------------------------------------------
--> Inserindo visão módulo			

	insert into sys_visaomodulo 
    select 1,sis_id, mod_id from sys_modulo where sis_id = 1006