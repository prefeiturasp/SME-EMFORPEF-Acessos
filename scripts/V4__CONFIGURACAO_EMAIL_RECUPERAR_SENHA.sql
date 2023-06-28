--> Criação das tabelas
create table if not exists public.configuracao_email (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1 NO CYCLE),
	codigo_sistema int4 NOT NULL,
	email varchar(100) NOT NULL,
	nome varchar(100) NOT NULL,
	smtp varchar(100) NOT NULL,
	usuario varchar(50) NOT NULL,
	senha varchar(50) NOT NULL,
	porta int4 NOT NULL,
	tls bool NOT NULL DEFAULT false,
	CONSTRAINT configuracao_email_pk PRIMARY KEY (id)
);

create table if not exists  public.sistema_recuperacao_senha (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1 NO CYCLE),
	codigo_sistema int4 NOT NULL,
	nome_sistema varchar(20) NOT NULL,
	pagina_recuperacao_senha varchar(200) NOT NULL,
	CONSTRAINT sistema_recuperacao_senha_pkey PRIMARY KEY (id)
);


create table if not exists  public.usuario_recuperacao_senha (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1 NO CYCLE),
	login varchar(50) NULL,
	expiracao timestamp NULL,
	token uuid NULL,
	codigo_sistema int4 NOT NULL,
	CONSTRAINT usuario_recuperacao_senha_pkey PRIMARY KEY (id),
	CONSTRAINT usuario_recuperacao_senha_un_login UNIQUE (login)
);
CREATE INDEX uusuario_recuperacao_senha_login_idx ON public.usuario_recuperacao_senha USING btree (login);

--> Informações da configuração do e-mail
INSERT INTO public.configuracao_email (codigo_sistema,email,nome,smtp,usuario,senha,porta,tls) 
select 1,'sgp-nao_responder@sme.prefeitura.sp.gov.br','Novo SGP - Não responder','smtp.office365.com','sgp-nao_responder@sme.prefeitura.sp.gov.br','247@Vzi#00',587,false 
where not exists (select email from configuracao_email where email = 'sgp-nao_responder@sme.prefeitura.sp.gov.br');

INSERT INTO public.configuracao_email (codigo_sistema,email,nome,smtp,usuario,senha,porta,tls) 
select 1006,'cdep-nao_responder@sme.prefeitura.sp.gov.br','CDEP - Não responder','smtp.office365.com','cdep-nao_responder@sme.prefeitura.sp.gov.br','247@Vzi#00',587,false 
where not exists (select email from configuracao_email where email = 'cdep-nao_responder@sme.prefeitura.sp.gov.br');

--> Informações da recuperação da senha
INSERT INTO public.sistema_recuperacao_senha (codigo_sistema,nome_sistema,pagina_recuperacao_senha) 
select 1,'SGP','https://novosgp.sme.prefeitura.sp.gov.br/redefinir-senha/' 
where not exists (select nome_sistema from sistema_recuperacao_senha where nome_sistema = 'SGP');

INSERT INTO public.sistema_recuperacao_senha (codigo_sistema,nome_sistema,pagina_recuperacao_senha) 
select 2,'Intranet','https://intranet.sme.prefeitura.sp.gov.br/index.php/nova-senha/?token='
where not exists (select nome_sistema from sistema_recuperacao_senha where nome_sistema = 'Intranet');

INSERT INTO public.sistema_recuperacao_senha (codigo_sistema,nome_sistema,pagina_recuperacao_senha) 
select 3,'SIGPAE','https://sigpae.sme.prefeitura.sp.gov.br/redefinir-senha/'
where not exists (select nome_sistema from sistema_recuperacao_senha where nome_sistema = 'SIGPAE');

INSERT INTO public.sistema_recuperacao_senha (codigo_sistema,nome_sistema,pagina_recuperacao_senha) 
select 1006,'CDEP','https://cedep.sme.prefeitura.sp.gov.br/redefinir-senha/'
where not exists (select nome_sistema from sistema_recuperacao_senha where nome_sistema = 'CDEP');