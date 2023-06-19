namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class Usuario : EntidadeBaseCoreSSO
    {
        public string Login { get; set; }
        public string Dominio { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Criptografia { get; set; }
        public int Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Guid PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
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
    }
}
