using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class Usuario : EntidadeBaseCoreSSO
    {
        public required string Login { get; set; }
        public string? Dominio { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public TipoCriptografia Criptografia { get; set; }
        public int Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Guid PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }
        public PessoaDocumento? Documento { get; set; }
        public int Integridade { get; set; }
        public int IntegracaoAD { get; set; }
        public int IntegracaoExterna { get; set; }
        public DateTime DataAlteracaoSenha { get; set; }
        public Guid EntidadeId { get; set; }

        public void AdicionarPessoa(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                Pessoa = pessoa;
                PessoaId = pessoa.Id;
            }
        }
        public void AdicionarPessoaDocumento(PessoaDocumento documento)
        {
            if (documento != null)
            {
                Documento = documento;
            }
        }
    }
}
