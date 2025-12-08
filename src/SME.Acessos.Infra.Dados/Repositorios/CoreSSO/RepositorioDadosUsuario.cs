using Dapper;
using SME.Acessos.Infra.Dados.Constantes;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioDadosUsuario(IConexaoCoreSSO conexao) : RepositorioBaseCoreSSO<DadosUsuario>(conexao), IRepositorioDadosUsuario
    {
        public async Task<DadosUsuario> ObterMeusDados(string login)
        {
            var query = @"SELECT    
                                   p.pes_nome as nome,                            
                                   d.psd_numero as cpf,      
                                   u.usu_login as login,
                                   u.usu_email as email,   
                                   coalesce(c_celular.psc_contato, c_fixo.psc_contato) as telefone,
                                   endereco.end_logradouro as endereco,
                                   c_endereco.pse_numero as numero,
                                   c_endereco.pse_complemento as complemento,
                                   endereco.end_bairro as bairro,
                                   endereco.end_cep as cep,
                                   cidade.cid_nome as cidade,
                                   uf.unf_sigla as estado
                        FROM sys_usuario u    
                        join pes_pessoa p on u.pes_id = p.pes_id
                        join pes_pessoadocumento d on p.pes_id = d.pes_id and d.tdo_id = @tipoDocumentoCpf
                        left join pes_pessoacontato c_celular on c_celular.pes_id = p.pes_id and c_celular.tmc_id = @tipoMeioContatoTelefoneCelular
                        left join pes_pessoacontato c_fixo on c_fixo.pes_id = p.pes_id and c_fixo.tmc_id = @tipoMeioContatoTelefoneFixo
                        left join pes_pessoacontato c_email on c_email.pes_id = p.pes_id and c_email.tmc_id = @tipoMeioContatoEmail
                        left join pes_pessoaendereco c_endereco on c_endereco.pes_id = p.pes_id  
                        left join end_endereco endereco on endereco.end_id = c_endereco.end_id
                        left join end_cidade cidade on cidade.cid_id = endereco.cid_id
                        left join end_unidadefederativa uf on uf.unf_id = cidade.unf_id
                        where u.usu_login = @login ";
            
            var dadosUsuario = await conexao.Obter().QueryAsync<DadosUsuario>(query, new
            {
                login, 
                tipoDocumentoCpf = ConstantesDados.TIPO_DOCUMENTO_CPF,
                tipoMeioContatoTelefoneCelular = ConstantesDados.TIPO_MEIO_CONTATO_TELEFONE_CELULAR,
                tipoMeioContatoTelefoneFixo = ConstantesDados.TIPO_MEIO_CONTATO_TELEFONE_FIXO,
                tipoMeioContatoEmail = ConstantesDados.TIPO_MEIO_CONTATO_EMAIL
            });
            
            return dadosUsuario.Any() ? dadosUsuario.First() : default!;
        }
    }
}
