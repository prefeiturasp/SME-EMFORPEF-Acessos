using System.ComponentModel.DataAnnotations;

namespace SME.Acessos.Aplicacao
{
    public class DadosUsuarioDto
    {
        [Required(ErrorMessage = "Login é necessário para o usuário")]
        [MinLength(1)]
        [MaxLength(500)]
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
