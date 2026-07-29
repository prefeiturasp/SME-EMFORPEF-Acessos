namespace SME.Acessos.Aplicacao.DTO
{
    public class RetornoAutenticacaoDTO
    {
        public required string Nome { get; set; }
        public string? NomeSocial { get; set; }
        public required string Login { get; set; }
        public required string Email { get; set; }
        public required string Cpf { get; set; }
    }
}
