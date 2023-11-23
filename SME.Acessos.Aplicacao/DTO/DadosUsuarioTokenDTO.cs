namespace SME.Acessos.Aplicacao.DTO
{
    public class DadosUsuarioTokenDTO
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public int Sistema { get; set; }
        public Guid? Perfil { get; set; }
    }
}
