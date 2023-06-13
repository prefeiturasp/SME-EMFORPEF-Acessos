
namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class UsuarioGrupoPessoa : EntidadeBaseCoreSSO
    {
        public Guid UsuarioId { get; set; }
        public string UsuarioEmail { get; set; }
        public string PessoaNome { get; set; }
        public Guid GrupoId { get; set; }
        public string GrupoNome { get; set; }
    }
}
