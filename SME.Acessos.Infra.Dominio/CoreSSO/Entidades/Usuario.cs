namespace SME.Acessos.Infra.Dominio.CoreSSO
{
    public class Usuario : EntidadeBaseCoreSSO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
