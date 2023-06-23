namespace SME.Acessos.Aplicacao.DTO
{
    public class AlterarUsuarioDTO
    {
        public string Login { get; set; }
        public string SenhaAtual { get; set; }
        public string SenhaNova { get; set; }
        public int SistemaId { get; set; }
    }
}
