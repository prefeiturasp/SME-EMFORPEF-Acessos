namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class UsuarioBulkInsertDto
    {
        public Guid PessoaId { get; set; }
        public Guid UsuarioId { get; set; }
        public required string Nome { get; set; }
        public required string Login { get; set; }
        public required string Email { get; set; }
        public required string SenhaCriptografada { get; set; }
    }
}
