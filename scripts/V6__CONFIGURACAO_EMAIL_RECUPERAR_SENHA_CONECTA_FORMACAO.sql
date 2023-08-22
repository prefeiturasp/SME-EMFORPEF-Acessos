ALTER TABLE public.configuracao_email ALTER COLUMN usuario TYPE varchar(100);

--> Informações da configuração do e-mail
INSERT INTO public.configuracao_email (codigo_sistema,email,nome,smtp,usuario,senha,porta,tls) 
select 1007,'conectaformacao-nao_responder@sme.prefeitura.sp.gov.br','Conecta Formação - Não responder','smtp.office365.com','conectaformacao-nao_responder@sme.prefeitura.sp.gov.br','Yal16657',587,false 
where not exists (select email from configuracao_email where email = 'conectaformacao-nao_responder@sme.prefeitura.sp.gov.br');

--> Informações da recuperação da senha
INSERT INTO public.sistema_recuperacao_senha (codigo_sistema,nome_sistema,pagina_recuperacao_senha) 
select 1007,'Conecta Formação','https://conectaformacao.sme.prefeitura.sp.gov.br/redefinir-senha/'
where not exists (select nome_sistema from sistema_recuperacao_senha where nome_sistema = 'Conecta Formação');