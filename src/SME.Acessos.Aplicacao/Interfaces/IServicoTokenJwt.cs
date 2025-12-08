using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoTokenJwt
    {
        string GerarToken(ClaimsTokenDto claimsTokenDto);
        DadosUsuarioTokenDTO ObterDadosToken(string token);
        DateTime ObterDataHoraCriacao();

        DateTime ObterDataHoraExpiracao();
    }
}