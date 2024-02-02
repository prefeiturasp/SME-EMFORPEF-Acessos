--> Renomear tabela de 'sistema_recuperacao_senha' para 'sistema_acao'
alter table sistema_recuperacao_senha rename to sistema_acao;

--> Renomear coluna de 'pagina_recuperacao_senha' para 'endereco'
alter table sistema_acao rename column pagina_recuperacao_senha to endereco;

--> Adicionar coluna tipo em 'sistema_acao'
alter table sistema_acao add column if not exists tipo int default 1;

--> Adicionar endereço de validação de e-mail
insert into sistema_acao (codigo_sistema, nome_sistema, endereco, tipo)
select '1007', 'Conecta Formação', 'https://conectaformacao.sme.prefeitura.sp.gov.br/validar-email/{token}', 2
where not exists (select id from sistema_acao where codigo_sistema = '1007' and tipo = 2);

--> Colocar token no sistema_acao
update sistema_acao set endereco = 'https://novosgp.sme.prefeitura.sp.gov.br/redefinir-senha/{0}' where codigo_sistema = 1 and tipo = 1;
update sistema_acao set endereco = 'https://intranet.sme.prefeitura.sp.gov.br/index.php/nova-senha/?token={0}' where codigo_sistema = 2 and tipo = 1;
update sistema_acao set endereco = 'https://sigpae.sme.prefeitura.sp.gov.br/redefinir-senha/{0}' where codigo_sistema = 3 and tipo = 1;
update sistema_acao set endereco = 'https://cdep.sme.prefeitura.sp.gov.br/redefinir-senha/{0}' where codigo_sistema = 1006 and tipo = 1;
update sistema_acao set endereco = 'https://conectaformacao.sme.prefeitura.sp.gov.br/redefinir-senha/{0}' where codigo_sistema = 1007 and tipo = 1;
update sistema_acao set endereco = 'https://conectaformacao.sme.prefeitura.sp.gov.br/validar-email/{0}' where codigo_sistema = 1007 and tipo = 2;

--> Renomear tabela de 'usuario_recuperacao_senha' para 'usuario_validacao_token'
alter table usuario_recuperacao_senha rename to usuario_validacao_token;

--> Adicionar coluna tipo em 'usuario_validacao_token'
alter table usuario_validacao_token add column if not exists tipo int default 1;

--> Alterando a constraint por sistema e tipo de ação
alter table usuario_validacao_token drop constraint usuario_recuperacao_senha_un_login;
alter table usuario_validacao_token add constraint usuario_recuperacao_senha_un_login_sistema_tipo UNIQUE (login,codigo_sistema, tipo);