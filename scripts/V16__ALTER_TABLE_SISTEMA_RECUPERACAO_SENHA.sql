--> Renomear tabela de 'sistema_recuperacao_senha' para 'sistema_acao'
alter table sistema_recuperacao_senha rename to sistema_acao;

--> Renomear coluna de 'pagina_recuperacao_senha' para 'endereco'
alter table sistema_acao rename column pagina_recuperacao_senha to endereco;

--> Adicionar coluna tipo em 'sistema_acao'
alter table sistema_acao add column if not exists tipo int default 1;

--> Adicionar endereço de validação de e-mail
insert into sistema_acao (codigo_sistema, nome_sistema, endereco, tipo)
select '1007', 'Conecta Formação', 'https://conectaformacao.sme.prefeitura.sp.gov.br/validar-email/', 2
where not exists (select id from sistema_acao where codigo_sistema = '1007' and tipo = 2);

--> Renomear tabela de 'usuario_recuperacao_senha' para 'usuario_validacao_token'
alter table usuario_recuperacao_senha rename to usuario_validacao_token;

--> Adicionar coluna tipo em 'usuario_validacao_token'
alter table usuario_validacao_token add column if not exists tipo int default 1;

--> Alterando a constraint por sistema e tipo de ação
alter table usuario_validacao_token drop constraint usuario_recuperacao_senha_un_login;
alter table usuario_validacao_token add constraint usuario_recuperacao_senha_un_login_sistema_tipo UNIQUE (login,codigo_sistema, tipo);