using System.ComponentModel.DataAnnotations;

namespace SME.Acessos.Aplicacao.DTO
{
    public class LoginEmailDTO
    {
        [Required(ErrorMessage = "Login é necessário para o usuário")]
        [MinLength(1)]
        [MaxLength(500)]
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
