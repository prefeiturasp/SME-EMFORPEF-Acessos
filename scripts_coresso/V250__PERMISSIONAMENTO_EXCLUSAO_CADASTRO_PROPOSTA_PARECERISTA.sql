update SYS_GrupoPermissao 
set grp_excluir = 0
where gru_id = 'e98e06d1-0556-4156-832a-613df54e6096' --Parecerista
and mod_id = (select sm.mod_id from SYS_Modulo sm 
  				  where sm.mod_nome = 'Cadastro' 
				  and sm.mod_idPai = (select sm.mod_id from SYS_Modulo sm 
										where sm.sis_id = (select sis_id from SYS_Sistema ss where ss.sis_nome like '%Conecta formação%')
										and sm.mod_nome = 'Propostas'));
