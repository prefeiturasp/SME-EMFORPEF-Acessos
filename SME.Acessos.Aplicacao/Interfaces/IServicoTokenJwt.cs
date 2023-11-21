using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoTokenJwt
    {
        string GerarToken(string usuarioLogin, string usuarioNome, int sistemaId, Guid? guidPerfil, IEnumerable<long> permissionamentos);
        DadosUsuarioTokenDTO ObterDadosToken(string token);
        DateTime ObterDataHoraCriacao();

        DateTime ObterDataHoraExpiracao();
    }
}