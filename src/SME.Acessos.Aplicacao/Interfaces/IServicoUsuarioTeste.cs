using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarioTeste
    {
        Task<IEnumerable<DadosPessoaUsuarioDto>> CadastrarUsuariosEmMassaAsync(int quantidade, Guid? perfilId);
        Task ExcluirUsuariosEmMassaAsync(IEnumerable<string> logins);
    }
}
