using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoTokenJwt
    {
        string GerarToken(string usuarioLogin, string usuarioNome, int sistemaId, Guid? guidPerfil, IEnumerable<long> permissionamentos, IEnumerable<UsuarioGrupoPessoa> perfisUsuario, IEnumerable<string> dres);
        DadosUsuarioTokenDTO ObterDadosToken(string token);
        DateTime ObterDataHoraCriacao();

        DateTime ObterDataHoraExpiracao();
    }
}