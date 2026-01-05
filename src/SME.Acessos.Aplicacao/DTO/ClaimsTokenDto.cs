using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Aplicacao.DTO
{
    public record ClaimsTokenDto(
        string UsuarioLogin,
        string UsuarioNome,
        int SistemaId,
        Guid? GuidPerfil,
        IEnumerable<long> Permissionamentos,
        IEnumerable<UsuarioGrupoPessoa> PerfisUsuario,
        IEnumerable<string> Dres)
    {
    }
}
