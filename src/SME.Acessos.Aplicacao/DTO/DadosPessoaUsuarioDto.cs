namespace SME.Acessos.Aplicacao.DTO
{
    public class DadosPessoaUsuarioDto
    {
        public Guid PerfilId { get; set; }
        public required string Login { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public string? Email { get; set; }
    }
}
