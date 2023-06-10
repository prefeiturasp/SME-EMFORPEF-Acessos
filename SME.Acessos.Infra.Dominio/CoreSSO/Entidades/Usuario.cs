namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class Usuario : EntidadeBaseCoreSSO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
